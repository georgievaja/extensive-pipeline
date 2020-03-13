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

        //TODO: this can probably be decorating infrastructure layer, where the key provider is injected
        public Task SetCacheControlResponseAsync(CacheControlKey key, CacheControlResponse response)
        {
            return Task.CompletedTask;
        }
    }
}
