using ProfileManagement.TestShared.Fakes.Profiles.Features;

namespace ProfileManagement.TestShared.Fakes.Profiles.Models;

public static class FakeProfileCreate
{
    public static Profile Generate()
    {
        var command = new FakeCreateProfileCommand().Generate();

        return Profile.CreateSimple(ProfileId.Of(command.ProfileId), Name.Of(command.Name),
            Description.Of(command.Description));
    }

    public static List<Profile> Generate(int count)
    {
        var profiles = new List<Profile>();

        for (var i = 0; i < count; i++)
        {
            profiles.Add(Generate());
        }

        return profiles;
    }
}