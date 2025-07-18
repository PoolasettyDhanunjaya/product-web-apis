// File: Models/DatabaseSettings.cs

namespace web_apis.Models
{
    /// <summary>
    /// Represents the MongoDB connection settings used for initializing the database connection.
    /// These values are typically bound from appsettings.json using IOptions.
    /// </summary>
    public class DatabaseSettings
    {
        /// <summary>
        /// The MongoDB connection string (e.g., connection to Atlas or local MongoDB instance).
        /// </summary>
        public string ConnectionString { get; set; } = null!;

        /// <summary>
        /// The name of the MongoDB database to connect to.
        /// </summary>
        public string DatabaseName { get; set; } = null!;
    }
}
