using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Extensive.Pipeline.CacheControl.Extensions;
using Extensive.Pipeline.CacheControl.Providers;
using Extensive.Pipeline.CacheControl.Pure.Functors;
using Extensive.Pipeline.CacheControl.Sample.Persistence.Models;
using Extensive.Pipeline.CacheControl.Stores;
using System.Diagnostics.CodeAnalysis;

namespace Extensive.Pipeline.CacheControl.Sample.Persistence
{
    public class TeamsVersioningDecorator : ITeamsStore
    {
        private readonly ITeamsStore inner;
        private readonly ICacheControlStore cacheControlStore;
        private readonly ICacheControlKeyProvider cacheControlKeyProvider;

        public TeamsVersioningDecorator(
            [DisallowNull] ITeamsStore inner,
            [DisallowNull] ICacheControlStore cacheControlStore,
            [DisallowNull] ICacheControlKeyProvider cacheControlKeyProvider)
        {
            this.inner = inner ?? throw new ArgumentNullException(nameof(inner));
            this.cacheControlStore = cacheControlStore ?? throw new ArgumentNullException(nameof(cacheControlStore));
            this.cacheControlKeyProvider = cacheControlKeyProvider ?? throw new ArgumentNullException(nameof(cacheControlKeyProvider));
        }

        public async Task<Team> CreateTeamAsync(Team team)
        {
            if (team == null) throw new ArgumentNullException(nameof(team));

            var newTeam = await inner.CreateTeamAsync(team);
            await SetCacheControlResponseAsync(newTeam);

            return newTeam;
        }

        public Task<IEnumerable<Team>> GetTeamsAsync()
        {
            return inner.GetTeamsAsync();
        }

        public Task<Maybe<Team>> TryGetTeamAsync(Guid id)
        {
            return inner.TryGetTeamAsync(id);
        }

        public async Task<Maybe<Team>> TryReplaceTeamAsync(
            Guid id, 
            Team team)
        {
            if (team == null) throw new ArgumentNullException(nameof(team));

            var replaced = await inner.TryReplaceTeamAsync(id, team);
            await replaced
                .MatchAsync(
                    SetCacheControlResponseAsync, 
                    () => { });

            return replaced;
        }

        private Task SetCacheControlResponseAsync([DisallowNull] Team team)
        {
            if (team == null) throw new ArgumentNullException(nameof(team));

            //TODO: this is not gonna work now as it is in post, put, have to add affected resources mappings
            var key = cacheControlKeyProvider.GetCacheControlKey();

            return cacheControlStore.SetCacheControlResponseAsync(
                key, new CacheControlResponse(
                    team.GetHashCode().ToString(),
                    team.Updated));
        }
    }
}
