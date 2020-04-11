using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using Extensive.Pipeline.CacheControl.Attributes;
using Extensive.Pipeline.CacheControl.Filters;
using Extensive.Pipeline.CacheControl.Providers;
using Extensive.Pipeline.CacheControl.Stores;
using Extensive.Pipeline.CacheControl.Validators;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Extensive.Pipeline.CacheControl
{
    public static class CacheControlExtensions
    {
        /// <summary>
        /// Adds cache control services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="configureCacheControl"></param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddCacheControl(
            [NotNull] this IServiceCollection services,
            [NotNull] Action<CacheControlBuilder> configureCacheControl)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configureCacheControl == null) throw new ArgumentNullException(nameof(configureCacheControl));
            var cacheControlBuilder = new CacheControlBuilder();
            configureCacheControl(cacheControlBuilder);

            services.AddTransient<IfNoneMatchValidator>();
            services.AddTransient<IfModifiedSinceValidator>();

            services.TryAdd(ServiceDescriptor.Transient<ICacheControlStore, CacheControlStore>());
            services.TryAdd(ServiceDescriptor.Transient<ICacheControlKeyProvider, MockCacheControlKeyProvider>());
            services.TryAdd(ServiceDescriptor.Transient<IValidator>(sp =>
                sp.GetRequiredService<IfNoneMatchValidator>()
                    .SetNext(sp.GetRequiredService<IfModifiedSinceValidator>()))
                );

            services.AddScoped<CacheControl>(p => cacheControlBuilder.Build());
            services.AddScoped<DisableCacheControlFilter>();
            services.AddScoped<PrivateCacheControlFilter>();
            services.AddScoped<PublicCacheControlFilter>();

            return services;
        }

        public static void AddCacheControlFilters(
            [NotNull] this FilterCollection filters)
        {
            if (filters == null) throw new ArgumentNullException(nameof(filters));
            if (filters.Count == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(filters));

            filters.Add<DisableCacheControlFilter>();
            filters.Add<PrivateCacheControlFilter>();
            filters.Add<PublicCacheControlFilter>();
        }
    }
}
