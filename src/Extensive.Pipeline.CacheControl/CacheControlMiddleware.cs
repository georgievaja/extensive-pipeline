using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Extensive.Pipeline.CacheControl.Providers;
using Extensive.Pipeline.CacheControl.Stores;
using Extensive.Pipeline.CacheControl.Validators;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace Extensive.Pipeline.CacheControl
{
    public class CacheControlMiddleware
    {
        private readonly ICacheControlStore cacheStore;
        private readonly ICacheControlKeyProvider cacheControlKeyProvider;
        private readonly RequestDelegate next;
        private readonly CacheControl cacheControl;
        private readonly IValidator validator;

        public CacheControlMiddleware(
            [NotNull] RequestDelegate next,
            [NotNull] ICacheControlStore cacheStore,
            [NotNull] ICacheControlKeyProvider cacheControlKeyProvider,
            [NotNull] IValidator validator,
            [NotNull] CacheControl cacheControl)
        {
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
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
            //TODO: validate existence of cache control attr
            //var endpoint = context.Features.Get<IEndpointFeature>().Endpoint;
            //var cacheControlAttribute = endpoint?.Metadata?.GetMetadata<CacheControlAttribute>();

            //TODO: try validate and continue
            var baseKey = 
                cacheControlKeyProvider.GetCacheControlKey();
            var response = 
                await cacheStore.TryGetCacheControlResponseAsync(baseKey);

            var valid = response
                .Select(just =>
                    validator.TryValidate(context.Request.Headers, just));

            //TODO: set headers based on cache control and validation result, default vary headers, additional vary headers and query strings
            context.Response.OnStarting(async t =>
            {
                //context.Response.Headers.Add(HeaderNames.CacheControl, new StringValues(new[] { "public", "max-age=100" }));
            }, null);

            await next.Invoke(context);
        }
    }
}
