using System;
using System.Threading.Tasks;
using Extensive.Pipeline.CacheControl.Stores;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace Extensive.Pipeline.CacheControl
{
    public class CacheControlMiddleware
    {
        private readonly ICacheControlStore cacheStore;
        private readonly RequestDelegate next;

        public CacheControlMiddleware(
            [NotNull] RequestDelegate next,
            [NotNull] ICacheControlStore cacheStore)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.cacheStore = cacheStore ?? throw new ArgumentNullException(nameof(cacheStore));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //TODO: check request directives - max-age, max-stale, min-fresh, no-cache, no-store, no-transform, only-if-cached
            //TODO: check response directives from attribute - must-revalidate, no-cache, no-store, no-transform, public, private, proxy-revalidate, max-age, s-maxage
            
            // if cache control validation needed, create cache control key
            var baseKey = new CacheControlKeyBuilder()
                .WithMethod(context.Request.Method)
                .WithScheme(context.Request.Scheme)
                .WithHost(context.Request.Host.Value)
                .WithPathBase(context.Request.PathBase.Value)
                .WithPath(context.Request.Path.Value)
                .Build();

            // try get cache control response
            var va = await cacheStore.TryGetCacheControlResponseAsync(baseKey);

            await next.Invoke(context);
        }

    }
}
