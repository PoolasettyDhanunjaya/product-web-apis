// File: Services/ProductService.cs

using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_apis.Interface;
using web_apis.Models;

namespace web_apis.Services
{
    /// <summary>
    /// Service layer to manage product-related operations by communicating with the MongoDB repository.
    /// </summary>
    public class ProductService
    {
        // Repository instance for accessing MongoDB
        private readonly IMongoDbRepository<Product> _mongoDbRepository;

        /// <summary>
        /// Constructor that injects the generic MongoDB repository for Product.
        /// </summary>
        /// <param name="mongoDbRepository">The injected MongoDB repository instance.</param>
        public ProductService(IMongoDbRepository<Product> mongoDbRepository)
        {
            _mongoDbRepository = mongoDbRepository;
        }

        /// <summary>
        /// Creates a new product document in the specified collection.
        /// </summary>
        /// <param name="data">The product data to be inserted.</param>
        /// <param name="collectionName">The name of the MongoDB collection.</param>
        public async Task CreateAsync(Product data, string collectionName)
        {
            await _mongoDbRepository.CreateAsync(data, collectionName);
        }

        /// <summary>
        /// Updates an existing product or inserts it if it doesn't exist (upsert).
        /// </summary>
        /// <param name="data">The product data to be upserted.</param>
        /// <param name="collectionName">The name of the MongoDB collection.</param>
        public async Task UpdateAsync(Product data, string collectionName)
        {
            var fieldDefinition = Builders<Product>.Filter.Eq(s => s.id, data.id);
            await _mongoDbRepository.UpsertAsync(data, collectionName, fieldDefinition);
        }

        /// <summary>
        /// Deletes a product document from the specified collection by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete.</param>
        /// <param name="collectionName">The name of the MongoDB collection.</param>
        public async Task DeleteAsync(string id, string collectionName)
        {
            var fieldDefinition = Builders<Product>.Filter.Eq(s => s.id, id);
            await _mongoDbRepository.RemoveAsync(collectionName, fieldDefinition);
        }

        /// <summary>
        /// Retrieves a single product by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the product to retrieve.</param>
        /// <returns>The product document, or null if not found.</returns>
        public async Task<Product> GetProductAsync(string id)
        {
            var fieldDefinition = Builders<Product>.Filter.Eq(s => s.id, id);
            return await _mongoDbRepository.GetAsyncOne(typeof(Product).ToString(), fieldDefinition);
        }

        /// <summary>
        /// Retrieves all product documents from the "Product" collection.
        /// </summary>
        /// <returns>A list of all product documents.</returns>
        public async Task<List<Product>> GetProductListAsync()
        {
            return await _mongoDbRepository.GetAllAsync(typeof(Product).ToString());
        }
    }
}
