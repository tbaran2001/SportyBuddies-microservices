namespace ProfileManagement.IntegrationTests;

public class GetProfilesTests(IntegrationTestWebAppFactory factory) : ProfileServiceIntegrationTestBase(factory)
{
    [Fact]
    public async Task GetProfiles_ShouldReturnProfiles_WhenProfilesExist()
    {
        // Arrange
        var fakeProfile = FakeProfileCreate.Generate();
        DbContext.Profiles.Add(fakeProfile);
        await DbContext.SaveChangesAsync();

        var query = new GetProfilesQuery();

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Profiles.Should().NotBeEmpty();
        result.Profiles.Should().HaveCount(1);
        result.Profiles.First().Id.Should().Be(fakeProfile.Id);
    }
}