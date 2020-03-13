using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Extensive.Pipeline.CacheControl.Enums;
using Extensive.Pipeline.CacheControl.Sample.Mappers;
using Extensive.Pipeline.CacheControl.Sample.Persistence;
using Extensive.Pipeline.CacheControl.Sample.Resources;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Extensive.Pipeline.CacheControl.Sample.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/teams")]
    [CacheControl(CacheabilityType = CacheabilityType.Public)]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamsStore teamsStore;
        public TeamsController([NotNull] ITeamsStore teamsStore)
        {
            this.teamsStore = teamsStore ?? throw new ArgumentNullException(nameof(teamsStore));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TeamResourceV1>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await teamsStore.GetTeamsAsync();
            var resource = result.Select(TeamMapper.MapFromDto);

            return Ok(resource);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(TeamResourceV1), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(
            TeamRequestV1 payload,
            ApiVersion version)
        {
            var dto = payload.MapCreatedToDto(DateTimeOffset.UtcNow, Guid.NewGuid());
            var result = await teamsStore.CreateTeamAsync(dto);

            return CreatedAtRoute(
                nameof(GetOne),
                new
                {
                    version = $"{version}",
                    id = result.Id
                },result.MapFromDto());
        }

        [HttpGet("{id}", Name = nameof(GetOne))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TeamResourceV1), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOne(
            Guid id)
        {
            var result = await teamsStore.TryGetTeamAsync(id);

            return result.MatchResult<IActionResult>(
                NotFound(),
                just =>
                    Ok(just.MapFromDto()));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Replace(
            Guid id, 
            TeamResourceV1 resource)
        {
            var result = await teamsStore.TryReplaceTeamAsync(id, 
                resource.MapUpdatedToDto(DateTimeOffset.UtcNow));

            return result.MatchResult<IActionResult>(
                NotFound(),
                NoContent());
        }
    }
}
