using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Extensive.Pipeline.CacheControl.Stores
{
    public class DefaultInMemoryCacheStore : ICacheStore
    {
        public Task<CacheControlResponse> GetCacheControlResponseAsync(CacheControlKey key)
        {
            //TODO: memory cache
            return Task.FromResult(new CacheControlResponse(DateTimeOffset.UtcNow, new NormalizedHeader("test"), DateTimeOffset.MinValue));
        }

        public Task SetCacheControlResponseAsync(CacheControlKey key, CacheControlResponse response)
        {
            //TODO: setting to mem cache
            return Task.CompletedTask;
        }
    }
}
