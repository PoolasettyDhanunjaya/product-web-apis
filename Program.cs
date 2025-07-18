// File: Program.cs

using web_apis.Interface;
using web_apis.Models;
using web_apis.Services;

var builder = WebApplication.CreateBuilder(args);

// ? Configure strongly typed settings object from appsettings.json
// This binds the "DatabaseSettings" section in appsettings.json to the DatabaseSettings class
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));

// ? Register services for dependency injection
builder.Services.AddSingleton<ProductService>(); // Business logic layer
builder.Services.AddSingleton(typeof(IMongoDbRepository<>), typeof(MongoDbRepository<>)); // Generic MongoDB repository

// ? Add controllers to the service collection
builder.Services.AddControllers();

// ? Add Swagger/OpenAPI support for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ? Enable CORS to allow requests from frontend apps (like Angular)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()   // Allow requests from any origin
              .AllowAnyHeader()   // Allow any HTTP headers
              .AllowAnyMethod();  // Allow any HTTP methods (GET, POST, etc.)
    });
});

var app = builder.Build();

// ? Enable Swagger UI in development mode only
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ? Enable CORS before routing to controllers
app.UseCors();

// ? Middleware for authorization (if used later)
app.UseAuthorization();

// ? Map attribute-routed controllers (e.g., [Route("api/[controller]")])
app.MapControllers();

// ? Run the application
app.Run();
