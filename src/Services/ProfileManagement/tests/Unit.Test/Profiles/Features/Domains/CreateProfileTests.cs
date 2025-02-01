using FluentAssertions;
using Unit.Test.Common;
using Unit.Test.Fakes;

namespace Unit.Test.Profiles.Features.Domains;

[Collection(nameof(UnitTestFixture))]
public class CreateProfileTests
{
    [Fact]
    public void can_create_valid_profile()
    {
        // Arrange + Act
        var fakeProfile = FakeProfileCreate.Generate();

        // Assert
        fakeProfile.Should().NotBeNull();
    }
}