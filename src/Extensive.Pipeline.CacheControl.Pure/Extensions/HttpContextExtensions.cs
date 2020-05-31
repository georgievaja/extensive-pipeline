using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Extensive.Pipeline.CacheControl.Pure.Functors;
using System.Diagnostics.CodeAnalysis;

namespace Extensive.Pipeline.CacheControl.Pure.Extensions
{
    public static class HttpContextExtensions
    {
        public static Maybe<HttpMethod> TryGetSupportedMethod(
            [DisallowNull] this HttpContext context,
            [DisallowNull] HttpMethod[] methods)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (methods == null) throw new ArgumentNullException(nameof(methods));
            if (methods.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(methods));

            var supportedMethod = methods.SingleOrDefault(m => m.Method == context.Request.Method);

            return supportedMethod != null ? Maybe<HttpMethod>.Some(supportedMethod) : Maybe<HttpMethod>.None;
        }
    }
}
