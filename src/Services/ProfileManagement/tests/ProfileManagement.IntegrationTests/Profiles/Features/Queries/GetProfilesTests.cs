using MongoDB.Driver;
using ProfileManagement.API.Profiles.Models.ReadModels;
using ProfileManagement.TestShared.Fakes.Profiles.Features;

namespace ProfileManagement.IntegrationTests.Profiles.Features.Queries;

public class GetProfilesTests : ProfileServiceIntegrationTestBase
{
    public GetProfilesTests(ProfileServiceIntegrationTestWebAppFactory factory) : base(factory)
    {
        DbContext.Profiles.RemoveRange(DbContext.Profiles);
        DbContext.SaveChanges();

        ReadDbContext.Profiles.DeleteMany(Builders<ProfileReadModel>.Filter.Empty);
    }

    [Fact]
    public async Task GetProfiles_ShouldReturnProfiles_WhenProfilesExist()
    {
        // Arrange
        var commands = new FakeCreateProfileCommand().Generate(5);
        foreach (var command in commands)
            await Sender.Send(command);

        var query = new GetProfilesQuery();

        // Act
        await Task.Delay(5000);
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