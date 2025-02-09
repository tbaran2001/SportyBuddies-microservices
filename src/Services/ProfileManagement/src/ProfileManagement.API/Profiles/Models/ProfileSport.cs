namespace ProfileManagement.API.Profiles.Models;

public record ProfileSport : Entity<ProfileSportId>
{
    public ProfileId ProfileId { get; private set; }
    public SportId SportId { get; private set; }

    internal ProfileSport(ProfileId profileId, SportId sportId)
    {
        ProfileId = profileId;
        SportId = sportId;
    }
}