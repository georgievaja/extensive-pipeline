using System;
using Extensive.Pipeline.CacheControl.Attributes;
using Extensive.Pipeline.CacheControl.Pure.Enums;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Extensive.Pipeline.CacheControl.Filters
{
    /// <inheritdoc />
    public class PublicCacheControlFilter : ActionFilterAttribute
    {
        private readonly CacheControl cacheControl;

        public PublicCacheControlFilter(
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
