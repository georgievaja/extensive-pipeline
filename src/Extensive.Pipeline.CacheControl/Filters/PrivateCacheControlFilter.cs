using System;
using Extensive.Pipeline.CacheControl.Attributes;
using Extensive.Pipeline.CacheControl.Pure.Enums;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Extensive.Pipeline.CacheControl.Filters
{
    /// <summary>
    /// Sets the response is for a single user and must not be stored by a shared cache.
    /// A private cache (like the user's browser cache) may store the response.
    /// </summary>
    public class PrivateCacheControlFilter : ActionFilterAttribute
    {
        private readonly CacheControl cacheControl;

        public PrivateCacheControlFilter(
            [NotNull] CacheControl cacheControl)
        {
            this.cacheControl = cacheControl ?? throw new ArgumentNullException(nameof(cacheControl));
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //TODO: validate etags
            base.OnActionExecuting(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            //TODO: generate etags and set headers
            base.OnResultExecuting(context);
        }
    }
}
