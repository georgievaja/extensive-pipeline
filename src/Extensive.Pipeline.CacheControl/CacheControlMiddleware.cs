using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Extensive.Pipeline.CacheControl.Providers;
using Extensive.Pipeline.CacheControl.Stores;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace Extensive.Pipeline.CacheControl
{
    public class CacheControlMiddleware
    {
        private readonly ICacheStore cacheStore;
        private readonly RequestDelegate next;

        public CacheControlMiddleware(
            [NotNull] RequestDelegate next,
            [NotNull] ICacheStore cacheStore)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.cacheStore = cacheStore ?? throw new ArgumentNullException(nameof(cacheStore));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var key = new CacheControlKeyBuilder()
                .WithMethod(context.Request.Method)
                .WithScheme(context.Request.Scheme)
                .Build();

            var va = await cacheStore.GetCacheControlResponseAsync(key);

            await next.Invoke(context);
        }

    }
}
