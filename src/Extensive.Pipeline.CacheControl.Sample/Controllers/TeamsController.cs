using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Extensive.Pipeline.CacheControl.Attributes;
using Extensive.Pipeline.CacheControl.Pure.Enums;
using Extensive.Pipeline.CacheControl.Sample.Mappers;
using Extensive.Pipeline.CacheControl.Sample.Persistence;
using Extensive.Pipeline.CacheControl.Sample.Resources;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace Extensive.Pipeline.CacheControl.Sample.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/teams")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamsStore teamsStore;
        public TeamsController([DisallowNull] ITeamsStore teamsStore)
        {
            this.teamsStore = teamsStore ?? throw new ArgumentNullException(nameof(teamsStore));
        }

        [HttpGet]
        [PrivateCacheControl(MaxAge = 10, AdditionalVaryHeaders = new[] { "personal-number" })]
        [ProducesResponseType(typeof(IEnumerable<TeamResourceV1>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(
            [FromHeader(Name = "personal-number")]string personalNumber)
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
        [DisableCacheControl]
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
