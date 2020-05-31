using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Extensive.Pipeline.CacheControl.Attributes;
using Extensive.Pipeline.CacheControl.Providers;
using Extensive.Pipeline.CacheControl.Pure.Enums;
using Extensive.Pipeline.CacheControl.Pure.Extensions;
using Extensive.Pipeline.CacheControl.Stores;
using Extensive.Pipeline.CacheControl.Validators;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Net.Http.Headers;

namespace Extensive.Pipeline.CacheControl.Filters
{
    /// <summary>
    /// Sets the response is for a single user and must not be stored by a shared cache.
    /// A private cache (like the user's browser cache) may store the response.
    /// </summary>
    internal class PrivateCacheControlFilter : CacheControlFilter
    {
        private readonly CacheControl cacheControl;
        private readonly IValidator validator;
        private readonly IValidatorsProvider validatorProvider;

        public PrivateCacheControlFilter(
            [NotNull] CacheControl cacheControl,
            [NotNull] ICacheControlKeyProvider keyProvider,
            [NotNull] ICacheControlStore cacheControlStore,
            IValidator validator)
            : base(cacheControl, keyProvider, cacheControlStore)
        {
            this.cacheControl = cacheControl ?? throw new ArgumentNullException(nameof(cacheControl));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controlIsPresent =
                 await context.HttpContext.TryGetSupportedMethod(cacheControl.SupportedMethods)
                            .SelectMany(_ => 
                                context.ActionDescriptor.TryGetAttribute<PrivateCacheControlAttribute>())
                            .SelectManyAsync(directive =>
                                TryGetCacheControlValidators(directive.AdditionalVaryHeaders));

            var cacheIsValid =
                controlIsPresent.SelectMany(validators =>
                        validator.TryValidate(context.HttpContext.Request.Headers, validators)
            );

            await cacheIsValid.MatchAsync(
                result =>
                {
                    context.Result = new StatusCodeResult(StatusCodes.Status304NotModified);
                    return context.Result.ExecuteResultAsync(context);
                },() => base.OnActionExecutionAsync(context, next));
        }

        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var directiveIsPresent = context.HttpContext.TryGetSupportedMethod(cacheControl.SupportedMethods)
                .SelectMany(_ => context.ActionDescriptor.TryGetAttribute<PrivateCacheControlAttribute>());

            directiveIsPresent
                .Execute(directive =>
                    {
                        context.HttpContext.Response.Headers.Add(
                            HeaderNames.CacheControl, new[]
                                {"private", $"max-age={directive.MaxAge}"});
                        context.HttpContext.Response.Headers.Add(
                            HeaderNames.Vary, GetVaryHeaders(directive.AdditionalVaryHeaders));
                    }
                );

            var validatorsArePresent = await directiveIsPresent
                .SelectManyAsync(directive => 
                    TryGetCacheControlValidators(directive.AdditionalVaryHeaders));
            
            validatorsArePresent 
                .Execute(validators =>
                {
                    var headers = validators.GetHeaders();
                    foreach (var header in headers)
                    {
                        context.HttpContext.Response.Headers.Add(header);
                    }
                });

            await base.OnResultExecutionAsync(context, next);
        }
    }
}
