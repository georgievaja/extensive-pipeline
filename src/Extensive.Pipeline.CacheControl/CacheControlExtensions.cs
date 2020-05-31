using System.Diagnostics.CodeAnalysis;
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
using Extensive.Pipeline.CacheControl.Features.FeatureHandler;

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
            [DisallowNull] this IServiceCollection services,
            [DisallowNull] Action<CacheControlBuilder> configureCacheControl)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configureCacheControl == null) throw new ArgumentNullException(nameof(configureCacheControl));
            var cacheControlBuilder = new CacheControlBuilder();
            configureCacheControl(cacheControlBuilder);

            services.AddTransient<IfNoneMatchValidator>();
            services.AddTransient<IfModifiedSinceValidator>();

            services.AddTransient<DisableCacheControlFeatureHandler>();
            services.AddTransient<PrivateCacheControlFeatureHandler>();
            services.AddTransient<PublicCacheControlFeatureHandler>();

            services.AddTransient<ICacheControlStore, CacheControlStore>();
            services.AddTransient<ICacheControlKeyProvider, MockCacheControlKeyProvider>();
            services.AddTransient<IValidatorsProvider, DefaultValidatorsProvider>();
            services.AddTransient<IValidator>(sp =>
            {
                var etagValidator = sp.GetRequiredService<IfNoneMatchValidator>();
                var modifiedSinceValidator = sp.GetRequiredService<IfModifiedSinceValidator>();

                etagValidator
                .SetNext(modifiedSinceValidator);

                return etagValidator;
            });

            services.AddTransient<ICacheControlFeatureHandler>(sp =>
            {
                var diabled = sp.GetRequiredService<DisableCacheControlFeatureHandler>();
                var privateh = sp.GetRequiredService<PrivateCacheControlFeatureHandler>();
                var publich = sp.GetRequiredService<PublicCacheControlFeatureHandler>();

                diabled
                    .SetNext(privateh)
                    .SetNext(publich);

                return diabled;
            });

            services.AddScoped<CacheControl>(p => cacheControlBuilder.Build());
            services.AddScoped<CacheControlFilter>();

            return services;
        }

        public static void AddCacheControlFilters(
            [DisallowNull] this FilterCollection filters)
        {
            if (filters == null) throw new ArgumentNullException(nameof(filters));
            if (filters.Count == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(filters));

            filters.Add<CacheControlFilter>();
        }
    }
}
