using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileManagement.API.Contracts;
using ProfileManagement.Application.DTOs;
using ProfileManagement.Application.Profiles.Commands.AddSportToProfile;
using ProfileManagement.Application.Profiles.Commands.CreateProfile;
using ProfileManagement.Application.Profiles.Commands.RemoveSportFromProfile;
using ProfileManagement.Application.Profiles.Commands.UpdateProfile;
using ProfileManagement.Application.Profiles.Queries.GetProfile;
using ProfileManagement.Application.Profiles.Queries.GetProfiles;

namespace ProfileManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfilesController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProfileResponse>>> GetProfiles()
    {
        var query = new GetProfilesQuery();

        var profilesResult = await sender.Send(query);

        return Ok(profilesResult);
    }

    [HttpGet("{profileId:guid}")]
    public async Task<ActionResult<ProfileResponse>> GetProfile(Guid profileId)
    {
        var query = new GetProfileQuery(profileId);

        var profileResult = await sender.Send(query);

        return Ok(profileResult);
    }

    [HttpPost]
    public async Task<ActionResult<ProfileResponse>> CreateProfile(CreateProfileRequest request)
    {
        var command = request.Adapt<CreateProfileCommand>();

        var profileResult = await sender.Send(command);

        return Ok(profileResult);
    }

    [HttpPut("{profileId:guid}")]
    public async Task<ActionResult<ProfileResponse>> UpdateProfile(Guid profileId, UpdateProfileRequest request)
    {
        var command = request.Adapt<UpdateProfileCommand>() with { Id = profileId };

        var profileResult = await sender.Send(command);

        return Ok(profileResult);
    }

    [HttpPost("{profileId:guid}/sports/{sportId:guid}")]
    public async Task<ActionResult> AddSportToProfile(Guid profileId, Guid sportId)
    {
        var command = new AddSportToProfileCommand(profileId, sportId);

        await sender.Send(command);

        return NoContent();
    }

    [HttpDelete("{profileId:guid}/sports/{sportId:guid}")]
    public async Task<ActionResult> RemoveSportFromProfile(Guid profileId, Guid sportId)
    {
        var command = new RemoveSportFromProfileCommand(profileId, sportId);

        await sender.Send(command);

        return NoContent();
    }
}