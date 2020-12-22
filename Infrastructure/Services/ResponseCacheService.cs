using System;
using System.Threading.Tasks;
using Core.Interfaces;
using StackExchange.Redis;
using Newtonsoft.Json;
using System.IO;

namespace Infrastructure.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDatabase _database;
        public ResponseCacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            if (response == null)
            {
                return;
            }

            //var options = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            //};

            await _database.StringSetAsync(cacheKey, JsonConvert.SerializeObject(response), timeToLive);

        }

        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            var cachedResponse = await _database.StringGetAsync(cacheKey);

            if (cachedResponse.IsNullOrEmpty)
            {
                return null;
            }

            return cachedResponse;
        }
    }
}