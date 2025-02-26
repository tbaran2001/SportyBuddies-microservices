using FluentAssertions;
using ProfileManagement.API.Profiles.Features.Queries.GetProfiles;
using ProfileManagement.API.Profiles.Models;
using ProfileManagement.API.Profiles.ValueObjects;

namespace ProfileManagement.IntegrationTests;

public class GetProfilesTests(IntegrationTestWebAppFactory factory) : ProfileServiceIntegrationTestBase(factory)
{
    [Fact]
    public async Task GetProfiles_ShouldReturnProfiles_WhenProfilesExist()
    {
        // Arrange
        var profile = Profile.CreateSimple(ProfileId.Of(Guid.NewGuid()), Name.Of("xd"), Description.Of("xd"));
        DbContext.Profiles.Add(profile);
        await DbContext.SaveChangesAsync();

        var query = new GetProfilesQuery();

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Profiles.Should().NotBeEmpty();
        result.Profiles.Should().HaveCount(1);
        result.Profiles.First().Id.Should().Be(profile.Id);
    }
}