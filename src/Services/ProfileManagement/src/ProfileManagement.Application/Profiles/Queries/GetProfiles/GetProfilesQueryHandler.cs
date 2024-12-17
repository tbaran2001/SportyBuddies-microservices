namespace ProfileManagement.Application.Profiles.Queries.GetProfiles;

public class GetProfilesQueryHandler(IProfilesRepository profilesRepository)
    : IQueryHandler<GetProfilesQuery, List<ProfileResponse>>
{
    public async Task<List<ProfileResponse>> Handle(GetProfilesQuery query, CancellationToken cancellationToken)
    {
        var profiles = await profilesRepository.GetAllProfilesAsync();

        return profiles.Adapt<List<ProfileResponse>>();
    }
}