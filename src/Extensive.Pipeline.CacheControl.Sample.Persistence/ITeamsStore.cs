using Extensive.Pipeline.CacheControl.Pure.Functors;
using Extensive.Pipeline.CacheControl.Sample.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Extensive.Pipeline.CacheControl.Sample.Persistence
{
    public interface ITeamsStore
    {
        [NotNull]
        Task<Maybe<Team>> TryGetTeamAsync(Guid id);

        [NotNull]
        Task<IEnumerable<Team>> GetTeamsAsync();

        [NotNull]
        Task<Maybe<Team>> TryReplaceTeamAsync(Guid id, [NotNull] Team team);

        [NotNull]
        Task<Team> CreateTeamAsync([NotNull] Team team);
    }
}
