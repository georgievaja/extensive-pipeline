using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Extensive.Pipeline.CacheControl.Sample.Persistence.Models;
using Extensive.Pipeline.CacheControl.Sample.Resources;
using System.Diagnostics.CodeAnalysis;

namespace Extensive.Pipeline.CacheControl.Sample.Mappers
{
    public static class TeamMapper
    {
        [return: NotNull]
        public static Team MapUpdatedToDto(
            [DisallowNull] this TeamResourceV1 resource, 
            DateTimeOffset lastModified)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));

            return new Team
            {
                Id = resource.Id,
                Updated = lastModified,
                Name = resource.Name
            };
        }

        [return: NotNull]
        public static Team MapCreatedToDto(
            [DisallowNull] this TeamRequestV1 request,
            DateTimeOffset created,
            Guid id)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return new Team
            {
                Id = id,
                Updated = created,
                Name = request.Name
            };
        }


        [return: NotNull]
        public static TeamResourceV1 MapFromDto(
            [DisallowNull] this Team team)
        {
            if (team == null) throw new ArgumentNullException(nameof(team));

            return new TeamResourceV1
            {
                Name = team.Name,
                Id = team.Id
            };
        }
    }
}
