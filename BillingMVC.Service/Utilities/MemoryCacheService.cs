using BillingMVC.Core.Contracts.Services;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace BillingMVC.Service.Utilities
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromHours(24);

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> createItem)
        {
            if (!_memoryCache.TryGetValue(key, out T cachedItem))
            {
                cachedItem = await createItem();
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = _cacheDuration
                };

                _memoryCache.Set(key, cachedItem);
            }

            return cachedItem;
        }
    }
}
