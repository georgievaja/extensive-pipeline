using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace Extensive.Pipeline.CacheControl.Sample.Persistence
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddStores(
            [DisallowNull] this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSingleton<ITeamsStore, TeamsDummyStore>();
            services.Decorate<ITeamsStore, TeamsVersioningDecorator>();

            return services;
        }
    }
}
