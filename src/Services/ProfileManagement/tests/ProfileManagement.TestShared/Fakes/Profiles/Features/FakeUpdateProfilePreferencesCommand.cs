namespace ProfileManagement.TestShared.Fakes.Profiles.Features;

public sealed class FakeUpdateProfilePreferencesCommand : AutoFaker<UpdateProfilePreferencesCommand>
{
    public FakeUpdateProfilePreferencesCommand()
    {
        RuleFor(x => x.ProfileId, x => x.Random.Guid());
        RuleFor(x => x.MinAge, x => x.Random.Int(18, 100));
        RuleFor(x => x.MaxAge, (f, cmd) => f.Random.Int(cmd.MinAge, 100));
        RuleFor(x => x.MaxDistance, x => x.Random.Int(1, 100));
        RuleFor(x => x.PreferredGender, x => x.PickRandom<Gender>());
    }
}