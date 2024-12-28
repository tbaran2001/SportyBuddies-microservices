using BuildingBlocks.Authentication;

namespace ProfileManagement.Application.Profiles.Queries.GetCurrentProfile;

public class GetCurrentProfileQueryHandler(IProfilesRepository profilesRepository, IUserContext userContext)
    : IQueryHandler<GetCurrentProfileQuery, ProfileResponse>
{
    public async Task<ProfileResponse> Handle(GetCurrentProfileQuery query, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        var profile = await profilesRepository.GetProfileByIdWithSportsAsync(currentUser.Id);
        if (profile == null)
            throw new ProfileNotFoundException(currentUser.Id);

        return profile.Adapt<ProfileResponse>();
    }
}