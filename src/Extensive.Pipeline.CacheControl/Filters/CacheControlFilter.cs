using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Extensive.Pipeline.CacheControl.Attributes;
using Extensive.Pipeline.CacheControl.Features;
using Extensive.Pipeline.CacheControl.Features.FeatureHandler;
using Extensive.Pipeline.CacheControl.Providers;
using Extensive.Pipeline.CacheControl.Pure.Extensions;
using Extensive.Pipeline.CacheControl.Pure.Functors;
using Extensive.Pipeline.CacheControl.Stores;
using Extensive.Pipeline.CacheControl.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Extensive.Pipeline.CacheControl.Filters
{
    internal class CacheControlFilter: ActionFilterAttribute
    {
        private readonly CacheControl cacheControl;
        private readonly ICacheControlFeatureHandler featureHandler;
        private readonly IValidator validator;

        public CacheControlFilter(
            [DisallowNull] CacheControl cacheControl,
            [DisallowNull] ICacheControlFeatureHandler featureHandler,
            [DisallowNull] IValidator validator
            )
        {
            this.cacheControl = cacheControl ?? throw new ArgumentNullException(nameof(cacheControl));
            this.featureHandler = featureHandler ?? throw new ArgumentNullException(nameof(featureHandler));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var feature = await context.HttpContext.TryGetSupportedMethod(cacheControl.SupportedMethods)
                .SelectMany(_ => context.ActionDescriptor.TryGetAttribute<CacheControlAttribute>())
                .MatchResultAsync(
                    CacheControlFeature.CacheUnsupported(), 
                    async attr => await featureHandler.GetCacheControlFeature(attr));


            context.HttpContext.Features.Set<ICacheControlFeature>(feature);
            var cacheIsValid = feature.Validators == null ? Maybe<IHeaderDictionary>.None: validator.TryValidate(context.HttpContext.Request.Headers, feature.Validators);

            await cacheIsValid.MatchAsync(
                result =>
                {
                    context.Result = new StatusCodeResult(StatusCodes.Status304NotModified);
                    return context.Result.ExecuteResultAsync(context);
                }, () => base.OnActionExecutionAsync(context, next));
        }

        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var feature = context.HttpContext.Features.Get<ICacheControlFeature>();

            if (context.Result is ObjectResult objectResult)
            {
                if (objectResult.StatusCode.IsSuccessStatusCode() && feature.CacheControlSupported)
                {
                    context.HttpContext.Response.Headers.AddRange(feature.CacheControlResponseHeaders);

                    if (feature.ValidationSupported && feature.Validators != null)
                    {
                        var headers = feature.Validators.GetHeaders();
                        context.HttpContext.Response.Headers.AddRange(headers);
                    }
                }
            }

            return base.OnResultExecutionAsync(context, next);
        }
    }
}
