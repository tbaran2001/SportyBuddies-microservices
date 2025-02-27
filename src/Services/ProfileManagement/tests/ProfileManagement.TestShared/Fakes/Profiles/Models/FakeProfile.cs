namespace ProfileManagement.TestShared.Fakes.Profiles.Models;

public sealed class FakeProfile : AutoFaker<Profile>
{
    public FakeProfile()
    {
        RuleFor(x => x.Id, x => ProfileId.Of(x.Random.Guid()));
        RuleFor(x => x.Name, x => Name.Of(x.Person.FullName));
        RuleFor(x => x.Description, x => Description.Of(x.Lorem.Sentence()));
        RuleFor(x => x.Gender, x => x.PickRandom<Gender>());
        RuleFor(x => x.BirthDate, x => BirthDate.Of(x.Person.DateOfBirth));
    }
}