using System;
using System.Text.Json;
using System.Threading.Tasks;
using Extensive.Pipeline.CacheControl.Pure.Functors;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;

namespace Extensive.Pipeline.CacheControl.Stores
{
    public sealed class CacheControlStore : ICacheControlStore
    {
        private readonly IDistributedCache distributedCache;
        private readonly IHttpContextAccessor accessor;

        public CacheControlStore(
            [DisallowNull] IDistributedCache distributedCache,
            [DisallowNull] IHttpContextAccessor accessor)
        {
            this.accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
            this.distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        }

        public async Task<CacheContentValidators?> TryGetCacheControlResponseAsync(
            CacheControlKey key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            var result = await distributedCache.GetStringAsync(key.Key);
            
            return result == null ? 
                null : 
                JsonSerializer.Deserialize<CacheContentValidators>(result);
        }

        //TODO: this can probably be called from decorator on infrastructure layer
        public Task SetCacheControlResponseAsync(
            CacheControlKey key, 
            CacheContentValidators response)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (response == null) throw new ArgumentNullException(nameof(response));

            var str = JsonSerializer.Serialize(response);
            return distributedCache.SetStringAsync(key.Key, str);
        }
    }
}
