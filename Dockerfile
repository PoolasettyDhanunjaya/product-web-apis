# Stage 1: Build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file and restore
COPY *.csproj ./
RUN dotnet restore

# Copy all source files and build
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

# Expose port (optional, Render maps automatically)
EXPOSE 80

# Run the app
ENTRYPOINT ["dotnet", "web-apis.dll"]
