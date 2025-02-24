namespace ProfileManagement.UnitTests.Profiles.Models;

public class ProfileSportTests:BaseDomainTest
{
    [Fact]
    public void Create_ShouldCreateProfileSport_WhenValidData()
    {
        // Arrange
        var fakeProfileSport = FakeProfileSportCreate.Generate();

        // Act
        var createdProfileSport = ProfileSport.Create(
            ProfileId.Of(fakeProfileSport.ProfileId),
            SportId.Of(fakeProfileSport.SportId)
        );

        // Assert
        createdProfileSport.Should().NotBeNull();
        createdProfileSport.Id.Value.Should().NotBeEmpty();
        createdProfileSport.ProfileId.Value.Should().Be(fakeProfileSport.ProfileId);
        createdProfileSport.SportId.Value.Should().Be(fakeProfileSport.SportId);
    }
}