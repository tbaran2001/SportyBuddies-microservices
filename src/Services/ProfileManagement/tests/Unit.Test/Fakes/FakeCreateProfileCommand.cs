using AutoBogus;
using ProfileManagement.API.Profiles.Features.Commands.CreateProfile;

namespace Unit.Test.Fakes;

public sealed class FakeCreateProfileCommand:AutoFaker<CreateProfileCommand>
{
    public FakeCreateProfileCommand()
    {
        RuleFor(r => r.ProfileId, _ => Guid.NewGuid());
        RuleFor(r=>r.Name,r=>r.Person.FullName);
        RuleFor(r=>r.Description,r=>r.Lorem.Sentence());
    }
}