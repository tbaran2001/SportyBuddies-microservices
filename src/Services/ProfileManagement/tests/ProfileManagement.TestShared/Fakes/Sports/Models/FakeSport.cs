using ProfileManagement.API.Sports.Models;

namespace ProfileManagement.TestShared.Fakes.Sports.Models;

public class FakeSport : AutoFaker<Sport>
{
    public FakeSport()
    {
        RuleFor(x => x.Id, x => SportId.Of(x.Random.Guid()));
    }
}