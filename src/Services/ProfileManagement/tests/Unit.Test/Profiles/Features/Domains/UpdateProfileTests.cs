using FluentAssertions;
using Unit.Test.Common;
using Unit.Test.Fakes;

namespace Unit.Test.Profiles.Features.Domains;

[Collection(nameof(UnitTestFixture))]
public class UpdateProfileTests
{
    [Fact]
    public void can_update_valid_profile()
    {
        // Arrange
        var fakeProfile = FakeProfileCreate.Generate();

        // Act
        FakeProfileUpdate.Generate(fakeProfile);

        // Assert
        fakeProfile.Name.Should().Be(fakeProfile.Name);
    }
}