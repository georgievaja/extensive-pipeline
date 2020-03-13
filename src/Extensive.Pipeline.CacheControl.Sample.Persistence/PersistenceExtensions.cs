using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Extensive.Pipeline.CacheControl.Sample.Persistence
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddStores(
            [NotNull] this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSingleton<ITeamsStore, TeamsDummyStore>();
            services.Decorate<ITeamsStore, TeamsVersioningDecorator>();

            return services;
        }
    }
}
