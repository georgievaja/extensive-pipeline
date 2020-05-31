using Extensive.Pipeline.CacheControl.Pure.Functors;
using Extensive.Pipeline.CacheControl.Sample.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Extensive.Pipeline.CacheControl.Sample.Persistence
{
    public interface ITeamsStore
    {
        [return: NotNull]
        Task<Maybe<Team>> TryGetTeamAsync(Guid id);

        [return: NotNull]
        Task<IEnumerable<Team>> GetTeamsAsync();

        [return: NotNull]
        Task<Maybe<Team>> TryReplaceTeamAsync(Guid id, [DisallowNull] Team team);

        [return: NotNull]
        Task<Team> CreateTeamAsync([DisallowNull] Team team);
    }
}
