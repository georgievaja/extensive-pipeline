using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace Extensive.Pipeline.CacheControl.Providers
{
    public sealed class DefaultCacheControlKeyProvider : ICacheControlKeyProvider
    {
        private readonly IHttpContextAccessor accessor;
        public DefaultCacheControlKeyProvider(
            [NotNull] IHttpContextAccessor accessor)
        {
            this.accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        public CacheControlKey GetCacheControlKey()
        {
            return new CacheControlKeyBuilder()
                .WithMethod(accessor.HttpContext.Request.Method)
                .WithScheme(accessor.HttpContext.Request.Scheme)
                .WithHost(accessor.HttpContext.Request.Host.Value)
                .WithPathBase(accessor.HttpContext.Request.PathBase.Value)
                .WithPath(accessor.HttpContext.Request.Path.Value)
                .WithQueryStrings(accessor.HttpContext.Request.Query)
                .Build();
        }

        public CacheControlKey GetCacheControlKey(string[] varyHeaders)
        {
            if (varyHeaders == null) throw new ArgumentNullException(nameof(varyHeaders));
            if (varyHeaders.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(varyHeaders));

            return new CacheControlKeyBuilder()
                .WithMethod(accessor.HttpContext.Request.Method)
                .WithScheme(accessor.HttpContext.Request.Scheme)
                .WithHost(accessor.HttpContext.Request.Host.Value)
                .WithPathBase(accessor.HttpContext.Request.PathBase.Value)
                .WithPath(accessor.HttpContext.Request.Path.Value)
                .WithQueryStrings(accessor.HttpContext.Request.Query)
                .WithVaryHeaders(varyHeaders)
                .Build();
        }
    }
}
