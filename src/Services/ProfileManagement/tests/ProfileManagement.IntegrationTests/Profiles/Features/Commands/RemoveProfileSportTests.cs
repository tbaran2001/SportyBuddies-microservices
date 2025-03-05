namespace ProfileManagement.IntegrationTests.Profiles.Features.Commands;

public class RemoveProfileSportTests(IntegrationTestWebAppFactory factory) : ProfileServiceIntegrationTestBase(factory)
{
    [Fact]
    public async Task RemoveProfileSport_ShouldRemoveProfileSport_WhenProfileHasSport()
    {
        // Arrange
        var fakeProfile = new FakeProfile().Generate();
        var fakeSport = new FakeSport().Generate();

        fakeProfile.AddSport(fakeSport.Id);

        DbContext.Sports.Add(fakeSport);
        DbContext.Profiles.Add(fakeProfile);
        await DbContext.SaveChangesAsync();

        var command = new RemoveProfileSportCommand(fakeProfile.Id.Value, fakeSport.Id.Value);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.Should().Be(Unit.Value);
        fakeProfile.ProfileSports.Should().BeEmpty();
    }

    [Fact]
    public async Task RemoveProfileSport_ShouldThrowDomainException_WhenProfileDoesNotHaveSport()
    {
        // Arrange
        var fakeProfile = new FakeProfile().Generate();
        var fakeSport = new FakeSport().Generate();

        DbContext.Sports.Add(fakeSport);
        DbContext.Profiles.Add(fakeProfile);
        await DbContext.SaveChangesAsync();

        var command = new RemoveProfileSportCommand(fakeProfile.Id.Value, fakeSport.Id.Value);

        // Act
        Func<Task> act = async () => { await Sender.Send(command); };

        // Assert
        await act.Should().ThrowAsync<DomainException>();
    }
}