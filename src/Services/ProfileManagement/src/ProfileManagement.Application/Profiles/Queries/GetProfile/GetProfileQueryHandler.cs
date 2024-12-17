namespace ProfileManagement.Application.Profiles.Queries.GetProfile;

public class GetProfileQueryHandler(
    IProfilesRepository profilesRepository)
    : IQueryHandler<GetProfileQuery, ProfileResponse>
{
    public async Task<ProfileResponse> Handle(GetProfileQuery query, CancellationToken cancellationToken)
    {
        var profile = await profilesRepository.GetProfileByIdWithSportsAsync(query.Id);
        if (profile == null)
            throw new ProfileNotFoundException(query.Id);

        return profile.Adapt<ProfileResponse>();
    }
}