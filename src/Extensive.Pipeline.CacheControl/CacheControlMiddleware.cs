using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Extensive.Pipeline.CacheControl.Handlers;
using Extensive.Pipeline.CacheControl.Providers;
using Extensive.Pipeline.CacheControl.Pure.Extensions;
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
        private readonly RequestDelegate next;
        private readonly CacheControl cacheControl;
        private readonly IControlHandler controlHandler;

        public CacheControlMiddleware(
            [NotNull] RequestDelegate next,
            [NotNull] IControlHandler controlHandler,
            [NotNull] CacheControl cacheControl)
        {
            this.controlHandler = controlHandler ?? throw new ArgumentNullException(nameof(controlHandler));
            this.cacheControl = cacheControl ?? throw new ArgumentNullException(nameof(cacheControl));
            this.next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public Task InvokeAsync(HttpContext context)
        {
            if (!cacheControl.SupportedMethods.Contains(new HttpMethod(context.Request.Method)))
                return next.Invoke(context);

            var cacheControlDirective = context.TryGetEndpointMetadataAttribute<CacheControlAttribute>();

            return cacheControlDirective
                .MatchResult(
                    next.Invoke(context), 
                    directive => ControlCache(context, directive));
        }

        private async Task ControlCache(HttpContext context, CacheControlAttribute directive)
        {
            //var controlHeaders = await controlHandler.ControlCache(directive);

            await next.Invoke(context);
        }
    }
}
