// File: Services/MongoDbRepository.cs

using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using web_apis.Models;
using web_apis.Interface;

namespace web_apis.Services
{
    /// <summary>
    /// Generic repository class to perform CRUD operations on MongoDB collections.
    /// </summary>
    /// <typeparam name="T">The type of document stored in the collection.</typeparam>
    public class MongoDbRepository<T> : IMongoDbRepository<T> where T : class
    {
        private readonly IMongoDatabase _mongoDatabase;

        /// <summary>
        /// Initializes the MongoDB client and connects to the specified database.
        /// </summary>
        /// <param name="databaseSettings">MongoDB connection and database name from configuration.</param>

        public MongoDbRepository(IOptions<DatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _mongoDatabase = client.GetDatabase(settings.Value.DatabaseName);
        }

        /// <summary>
        /// Retrieves a single document from the specified collection that matches the given filter.
        /// </summary>
        public async Task<T?> GetAsyncOne(string collectionName, FilterDefinition<T> filter)
        {
            return await _mongoDatabase
                         .GetCollection<T>(collectionName)
                         .Find(filter)
                         .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Inserts a new document into the specified collection.
        /// </summary>
        public async Task CreateAsync(T data, string collectionName)
        {
            await _mongoDatabase
                  .GetCollection<T>(collectionName)
                  .InsertOneAsync(data);
        }

        /// <summary>
        /// Replaces a document if it exists, or inserts it if it does not (upsert).
        /// </summary>
        public async Task UpsertAsync(T data, string collectionName, FilterDefinition<T> filter)
        {
            await _mongoDatabase
                  .GetCollection<T>(collectionName)
                  .ReplaceOneAsync(filter, data, new ReplaceOptions { IsUpsert = true });
        }

        /// <summary>
        /// Deletes a document from the collection that matches the given filter.
        /// </summary>
        public async Task RemoveAsync(string collectionName, FilterDefinition<T> filter)
        {
            await _mongoDatabase
                  .GetCollection<T>(collectionName)
                  .DeleteOneAsync(filter);
        }

        /// <summary>
        /// Retrieves All document from the specified collection.
        /// </summary>
        public async Task<List<T>> GetAllAsync(string collectionName)
        {
            return await _mongoDatabase
                         .GetCollection<T>(collectionName)
                         .Find(Builders<T>.Filter.Empty)
                         .ToListAsync();
        }

    }
}
