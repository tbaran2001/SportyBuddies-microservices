namespace ProfileManagement.TestShared.Fakes.Profiles.Models;

public static class FakeProfileSportCreate
{
    public static ProfileSport Generate()
    {
        return ProfileSport.Create(ProfileId.Of(Guid.NewGuid()), SportId.Of(Guid.NewGuid()));
    }
}