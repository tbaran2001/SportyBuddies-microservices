namespace ProfileManagement.UnitTests.Fakes.Profiles.Features;

public sealed class FakeUpdateProfileCommand : AutoFaker<UpdateProfileCommand>
{
    public FakeUpdateProfileCommand(ProfileId profileId)
    {
        RuleFor(x => x.ProfileId, x => profileId);
        RuleFor(x => x.Name, x => x.Person.FullName);
        RuleFor(x => x.Description, x => x.Lorem.Sentence());
        RuleFor(x => x.Gender, x => x.PickRandom<Gender>());
        RuleFor(x => x.BirthDate, x => x.Person.DateOfBirth);
    }
}