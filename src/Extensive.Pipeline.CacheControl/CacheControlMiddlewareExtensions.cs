using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using Extensive.Pipeline.CacheControl.Providers;
using Extensive.Pipeline.CacheControl.Stores;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Extensive.Pipeline.CacheControl
{
    public static class CacheControlMiddlewareExtensions
    {
        /// <summary>
        /// Adds cache control services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddCacheControl(
            [DisallowNull] this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.TryAdd(ServiceDescriptor.Transient<IETagValidationProvider, DefaultETagValidationProvider>());
            services.TryAdd(ServiceDescriptor.Transient<ILastModifiedValidationProvider, DefaultLastModifiedValidationProvider>());
            services.TryAdd(ServiceDescriptor.Transient<ICacheControlStore, CacheControlStore>());
            services.TryAdd(ServiceDescriptor.Transient<ICacheControlKeyProvider, DefaultCacheControlKeyProvider>());

            return services;
        }

        /// <summary>
        /// Adds the cache control middleware <see cref="CacheControlMiddleware"/> to the request pipeline
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
        public static IApplicationBuilder UseCacheControl(
            [DisallowNull] this IApplicationBuilder app)
        {
            if (app is null) throw new ArgumentNullException(nameof(app));
            var cacheControlBuilder = new CacheControlBuilder();

            return app.UseMiddleware<CacheControlMiddleware>(cacheControlBuilder.Build());
        }

        /// <summary>
        /// Adds the cache control middleware <see cref="CacheControlMiddleware"/> to the request pipeline
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
        /// <param name="configureCacheControl">A delegate which can use a cache control builder to build a cache control.</param>
        public static IApplicationBuilder UseCacheControl(
            [DisallowNull] this IApplicationBuilder app,
            [DisallowNull] Action<CacheControlBuilder> configureCacheControl)
        {
            if (app is null) throw new ArgumentNullException(nameof(app));
            if (configureCacheControl == null) throw new ArgumentNullException(nameof(configureCacheControl));
            var cacheControlBuilder = new CacheControlBuilder();
            configureCacheControl(cacheControlBuilder);

            return app.UseMiddleware<CacheControlMiddleware>(cacheControlBuilder.Build());
        }
    }
}
