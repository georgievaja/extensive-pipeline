using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Text;
using Extensive.Pipeline.CacheControl.Pure.Functors;

namespace Extensive.Pipeline.CacheControl.Pure.Extensions
{
    public static class HttpContextExtensions
    {
        public static Maybe<TAttribute> TryGetEndpointMetadataAttribute<TAttribute>(this HttpContext context) where TAttribute: Attribute
        {
            var endpoint = context.Features?.Get<IEndpointFeature>()?.Endpoint?.Metadata;
            var cacheControlAttribute = endpoint?.GetMetadata<TAttribute>();

            return cacheControlAttribute != null ? Maybe<TAttribute>.Some(cacheControlAttribute) : Maybe<TAttribute>.None;
        }
    }
}
