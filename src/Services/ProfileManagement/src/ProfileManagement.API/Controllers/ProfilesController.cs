using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileManagement.API.Contracts;
using ProfileManagement.Application.DTOs;
using ProfileManagement.Application.Profiles.Commands.AddSportToProfile;
using ProfileManagement.Application.Profiles.Commands.CreateProfile;
using ProfileManagement.Application.Profiles.Commands.RemoveSportFromProfile;
using ProfileManagement.Application.Profiles.Commands.UpdateProfile;
using ProfileManagement.Application.Profiles.Commands.UpdateProfilePreferences;
using ProfileManagement.Application.Profiles.Queries.GetCurrentProfile;
using ProfileManagement.Application.Profiles.Queries.GetProfile;
using ProfileManagement.Application.Profiles.Queries.GetProfiles;

namespace ProfileManagement.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProfilesController(ISender sender) : ControllerBase
{
    [HttpGet("me")]
    public async Task<ActionResult<ProfileResponse>> GetCurrentProfile()
    {
        var query = new GetCurrentProfileQuery();

        var profileResult = await sender.Send(query);

        return Ok(profileResult);
    }

    [HttpGet("{profileId:guid}")]
    public async Task<ActionResult<ProfileResponse>> GetProfile(Guid profileId)
    {
        var query = new GetProfileQuery(profileId);

        var profileResult = await sender.Send(query);

        return Ok(profileResult);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProfileResponse>>> GetProfiles()
    {
        var query = new GetProfilesQuery();

        var profilesResult = await sender.Send(query);

        return Ok(profilesResult);
    }

    [HttpPut("me")]
    public async Task<ActionResult<ProfileResponse>> UpdateProfile(UpdateProfileRequest request)
    {
        var command = request.Adapt<UpdateProfileCommand>();

        var profileResult = await sender.Send(command);

        return Ok(profileResult);
    }

    [HttpPost("sports/{sportId:guid}")]
    public async Task<ActionResult> AddSportToProfile(Guid sportId)
    {
        var command = new AddSportToProfileCommand(sportId);

        await sender.Send(command);

        return NoContent();
    }

    [HttpDelete("sports/{sportId:guid}")]
    public async Task<ActionResult> RemoveSportFromProfile(Guid sportId)
    {
        var command = new RemoveSportFromProfileCommand(sportId);

        await sender.Send(command);

        return NoContent();
    }

    [HttpPut("preferences")]
    public async Task<ActionResult> UpdateProfilePreferences(UpdateProfilePreferencesRequest request)
    {
        var command = request.Adapt<UpdateProfilePreferencesCommand>();

        await sender.Send(command);

        return NoContent();
    }
}