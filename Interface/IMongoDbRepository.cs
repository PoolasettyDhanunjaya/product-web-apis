// File: Interface/IMongoDbRepository.cs

using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using web_apis.Models;

namespace web_apis.Interface
{
    public interface IMongoDbRepository<T> where T : class
    {
        Task<T?> GetAsyncOne(string collectionName, FilterDefinition<T> filter);
        Task<List<T>> GetAllAsync(string collectionName); 
        Task CreateAsync(T data, string collectionName);
        Task UpsertAsync(T data, string collectionName, FilterDefinition<T> filter);
        Task RemoveAsync(string collectionName, FilterDefinition<T> filter);
    }
}
