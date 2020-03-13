using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace Extensive.Pipeline.CacheControl.Providers
{
    public class DefaultCacheControlKeyProvider : ICacheControlKeyProvider
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
                .Build();
        }
    }
}
