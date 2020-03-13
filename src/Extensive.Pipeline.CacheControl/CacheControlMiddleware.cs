using System;
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

        public CacheControlMiddleware(
            [NotNull] RequestDelegate next,
            [NotNull] ICacheControlStore cacheStore,
            [NotNull] ICacheControlKeyProvider cacheControlKeyProvider)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.cacheStore = cacheStore ?? throw new ArgumentNullException(nameof(cacheStore));
            this.cacheControlKeyProvider = cacheControlKeyProvider ?? throw new ArgumentNullException(nameof(cacheControlKeyProvider));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //TODO: check request directives - max-age, max-stale, min-fresh, no-cache, no-store, no-transform, only-if-cached
            //TODO: check response directives from attribute - must-revalidate, no-cache, no-store, no-transform, public, private, proxy-revalidate, max-age, s-maxage
            
            var baseKey = cacheControlKeyProvider.GetCacheControlKey();
            var va = await cacheStore.TryGetCacheControlResponseAsync(baseKey);

            await next.Invoke(context);
        }

    }
}
