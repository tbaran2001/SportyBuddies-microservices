
using BuildingBlocks.Web;

namespace ProfileManagement.Application.Profiles.Queries.GetCurrentProfile;

public class GetCurrentProfileQueryHandler(IProfilesRepository profilesRepository, ICurrentUserProvider currentUserProvider)
    : IQueryHandler<GetCurrentProfileQuery, ProfileResponse>
{
    public async Task<ProfileResponse> Handle(GetCurrentProfileQuery query, CancellationToken cancellationToken)
    {
        var currentUserId = currentUserProvider.GetCurrentUserId();

        var profile = await profilesRepository.GetProfileByIdWithSportsAsync(currentUserId);
        if (profile == null)
            throw new ProfileNotFoundException(currentUserId);

        return profile.Adapt<ProfileResponse>();
    }
}