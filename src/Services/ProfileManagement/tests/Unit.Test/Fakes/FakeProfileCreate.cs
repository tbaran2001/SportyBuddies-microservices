using ProfileManagement.API.Profiles.ValueObjects;

namespace Unit.Test.Fakes;

public static class FakeProfileCreate
{
    public static global::ProfileManagement.API.Profiles.Models.Profile Generate()
    {
        var command = new FakeCreateProfileCommand().Generate();

        return global::ProfileManagement.API.Profiles.Models.Profile.CreateSimple(command.ProfileId, Name.Of(command.Name), Description.Of(command.Description));
    }
}