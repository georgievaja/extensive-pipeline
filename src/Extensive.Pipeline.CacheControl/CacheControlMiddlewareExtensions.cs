using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Extensive.Pipeline.CacheControl
{
    public static class CacheControlMiddlewareExtensions
    {
        /// <summary>
        /// Adds the cache control middleware <see cref="CacheControlMiddleware"/> to the request pipeline
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
        public static IApplicationBuilder UseCacheControl(
            [NotNull]this IApplicationBuilder app)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<CacheControlMiddleware>();
        }
    }
}
