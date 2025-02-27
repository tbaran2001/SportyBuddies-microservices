namespace ProfileManagement.IntegrationTests.Profiles.Features.Queries;

public class GetProfilesTests : ProfileServiceIntegrationTestBase
{
    public GetProfilesTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        DbContext.Profiles.RemoveRange(DbContext.Profiles);
        DbContext.SaveChanges();
    }

    [Fact]
    public async Task GetProfiles_ShouldReturnProfiles_WhenProfilesExist()
    {
        // Arrange
        var fakeProfiles = new FakeProfile().Generate(5);
        DbContext.Profiles.AddRange(fakeProfiles);
        await DbContext.SaveChangesAsync();

        var query = new GetProfilesQuery();

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Profiles.Should().NotBeEmpty();
        result.Profiles.Should().HaveCount(5);
    }

    [Fact]
    public async Task GetProfiles_ShouldReturnEmptyList_WhenProfilesDoNotExist()
    {
        // Arrange
        var query = new GetProfilesQuery();

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Profiles.Should().BeEmpty();
    }
}