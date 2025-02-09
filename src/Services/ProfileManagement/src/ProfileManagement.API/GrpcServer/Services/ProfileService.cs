using Grpc.Core;

namespace ProfileManagement.API.GrpcServer.Services;

public class ProfileService(IProfilesRepository profilesRepository)
    : ProfileProtoService.ProfileProtoServiceBase
{
    public override async Task<GetPotentialMatchesResponse> GetPotentialMatches(GetPotentialMatchesRequest request,
        ServerCallContext context)
    {
        var profile = await profilesRepository.GetProfileByIdWithSportsAsync(new Guid(request.ProfileId));
        var potentialMatches =
            await profilesRepository.GetPotentialMatchesAsync(profile.Id,
                profile.ProfileSports.Select(x => x.SportId.Value).ToList());

        var response = new GetPotentialMatchesResponse
        {
            ProfileIds = { potentialMatches.Select(x => x.ToString()) }
        };

        return response;
    }
}