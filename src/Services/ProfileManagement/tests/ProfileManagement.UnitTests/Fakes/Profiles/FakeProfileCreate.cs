namespace ProfileManagement.UnitTests.Fakes.Profiles;

public static class FakeProfileCreate
{
    public static Profile Generate()
    {
        var command = new FakeCreateProfileCommand().Generate();

        return Profile.CreateSimple(ProfileId.Of(command.ProfileId), Name.Of(command.Name),
            Description.Of(command.Description));
    }
}