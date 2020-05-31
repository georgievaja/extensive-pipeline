using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensive.Pipeline.CacheControl.Pure.Functors;
using Extensive.Pipeline.CacheControl.Sample.Persistence.Models;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Caching.Distributed;

namespace Extensive.Pipeline.CacheControl.Sample.Persistence
{
    //TODO: DUMMY persistence
    public class TeamsDummyStore : ITeamsStore
    {
        public IDictionary<Guid, Team> Teams;
        public TeamsDummyStore()
        {
            Teams = new Dictionary<Guid, Team>();
        }

        public Task<Team> CreateTeamAsync(Team team)
        {
            if (team == null) throw new ArgumentNullException(nameof(team));

            Teams.Add(team.Id, team);
            return Task.FromResult(team);
        }

        public Task<IEnumerable<Team>> GetTeamsAsync()
        {
            var teams = Teams.Values.AsEnumerable();

            return Task.FromResult(teams);
        }

        public Task<Maybe<Team>> TryGetTeamAsync(Guid id)
        {
            var teamFound = Teams.TryGetValue(id, out var team);

            return Task.FromResult(teamFound ? Maybe<Team>.Some(team) : Maybe<Team>.None);
        }

        public Task<Maybe<Team>> TryReplaceTeamAsync(Guid id, Team team)
        {
            if (team == null) throw new ArgumentNullException(nameof(team));

            var teamFound = Teams.ContainsKey(id);
            if (!teamFound) return Task.FromResult(Maybe<Team>.None);

            Teams[id] = team;
            return Task.FromResult(Maybe<Team>.Some(team));
        }
    }
}
