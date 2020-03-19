using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Extensive.Pipeline.CacheControl.Providers;
using Extensive.Pipeline.CacheControl.Stores;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace Extensive.Pipeline.CacheControl
{
    public class CacheControlMiddleware
    {
        private readonly ICacheControlStore cacheStore;
        private readonly ICacheControlKeyProvider cacheControlKeyProvider;
        private readonly RequestDelegate next;
        private readonly CacheControl cacheControl;

        public CacheControlMiddleware(
            [NotNull] RequestDelegate next,
            [NotNull] ICacheControlStore cacheStore,
            [NotNull] ICacheControlKeyProvider cacheControlKeyProvider,
            [NotNull] CacheControl cacheControl)
        {
            this.cacheControl = cacheControl ?? throw new ArgumentNullException(nameof(cacheControl));
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.cacheStore = cacheStore ?? throw new ArgumentNullException(nameof(cacheStore));
            this.cacheControlKeyProvider = cacheControlKeyProvider ?? throw new ArgumentNullException(nameof(cacheControlKeyProvider));
        }

        public Task InvokeAsync(HttpContext context)
        {
            return !cacheControl.SupportedMethods.Contains(new HttpMethod(context.Request.Method)) ? 
                next.Invoke(context) : 
                ControlCache(context);
        }

        private async Task ControlCache(HttpContext context)
        {
            // Check validation headers:
            /*
               | If-Match            | http     | standard | P 1 |
               | If-Modified-Since   | http     | standard | P 4 |
               | If-None-Match       | http     | standard | P 3 |
               | If-Unmodified-Since | http     | standard | P 2 |
               | ETag
               | Last-Modified  
             */

            // NOT SUPPORTED IN PHASE 1: Check request directives in Cache-control headers (not supported yet, no-cache, no-store, no-transform, only-if-cached, max-age, max-stale, min-fresh)
            
            // Add response directives from attribute - must-revalidate, no-cache, no-store, no-transform, public, private, proxy-revalidate, max-age, s-maxage
            // Add validation directives if they exist - etag, last-modified
            // Add vary headers
            var baseKey = cacheControlKeyProvider.GetCacheControlKey();
            var va = await cacheStore.TryGetCacheControlResponseAsync(baseKey);

            await next.Invoke(context);
        }

    }
}
