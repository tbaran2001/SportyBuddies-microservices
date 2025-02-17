namespace ProfileManagement.API.Profiles.Models;

public record ProfileSport : Entity<ProfileSportId>
{
    public ProfileId ProfileId { get; private set; }
    public SportId SportId { get; private set; }

    private ProfileSport(ProfileId profileId, SportId sportId)
    {
        ProfileId = profileId;
        SportId = sportId;
    }

    public static ProfileSport Create(ProfileId profileId, SportId sportId)
    {
        return new ProfileSport(profileId, sportId);
    }
}