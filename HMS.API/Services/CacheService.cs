using HMS.API.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace HMS.API.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly JsonSerializerOptions _jsonOptions;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = false
            };
        }

        public async Task<T?> GetAsync<T>(string key) where T : class
        {
            try
            {
                var cachedValue = await _distributedCache.GetStringAsync(key);
                if (string.IsNullOrEmpty(cachedValue))
                    return null;

                return JsonSerializer.Deserialize<T>(cachedValue, _jsonOptions);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task SetAsync<T>(string key, T value) where T : class
        {
            try
            {
                var serializedValue = JsonSerializer.Serialize(value, _jsonOptions);
                await _distributedCache.SetStringAsync(key, serializedValue);
            }
            catch (Exception)
            {
            }
        }

        public async Task RemoveAsync(string key)
        {
            try
            {
                await _distributedCache.RemoveAsync(key);
            }
            catch (Exception)
            {
            }
        }
    }
}
