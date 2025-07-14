using BillingMVC.Core.Contracts.Services;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace BillingMVC.Service.Utilities
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IDatabase _redis;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromHours(24);

        public MemoryCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _redis = connectionMultiplexer.GetDatabase();
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> createItem)
        {
            var cachedValue = await _redis.StringGetAsync(key);
            if (cachedValue.HasValue)
            {
                return JsonSerializer.Deserialize<T>(cachedValue);
            }

            T value = await createItem();

            var serializedValue = JsonSerializer.Serialize(value);
            await _redis.StringSetAsync(key, serializedValue, _cacheDuration);

            return value;
        }
    }
}
