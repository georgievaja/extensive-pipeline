using System;
using System.Linq;
using System.Net.Http;
using Extensive.Pipeline.CacheControl.Attributes;
using Extensive.Pipeline.CacheControl.Pure.Extensions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace Extensive.Pipeline.CacheControl.Filters
{
    /// <summary>
    /// Sets no-store for resources
    /// </summary>
    public class DisableCacheControlFilter : ActionFilterAttribute
    {
        private readonly CacheControl cacheControl;

        public DisableCacheControlFilter(
            [NotNull] CacheControl cacheControl)
        {
            this.cacheControl = cacheControl ?? throw new ArgumentNullException(nameof(cacheControl));
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.TryGetSupportedMethod(cacheControl.SupportedMethods)
                .SelectMany(_ => context.ActionDescriptor.TryGetAttribute<DisableCacheControlAttribute>())
                .Execute(just =>
            {
                context.HttpContext.Response.Headers.Add(
                    HeaderNames.CacheControl, new string[]
                        { "no-store" });

            });

            base.OnResultExecuting(context);
        }
    }
}
