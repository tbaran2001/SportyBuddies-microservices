using ProfileManagement.TestShared.Fakes.Sports.Features;

namespace ProfileManagement.UnitTests.Sports.Models;

public class SportTests : BaseDomainTest
{
    [Fact]
    public void Create_ShouldCreateSport_WhenValidData()
    {
        // Arrange
        var fakeSport = new FakeCreateSportCommand().Generate();

        // Act
        var createdSport = Sport.Create(
            SportId.Of(fakeSport.SportId)
        );

        // Assert
        createdSport.Should().NotBeNull();
        createdSport.Id.Value.Should().Be(fakeSport.SportId);
    }
}