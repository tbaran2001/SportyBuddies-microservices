namespace ProfileManagement.IntegrationTests.Profiles.Features.Commands;

public class AddProfileSportTests(IntegrationTestWebAppFactory factory) : ProfileServiceIntegrationTestBase(factory)
{
    [Fact]
    public async Task AddProfileSport_ShouldAddProfileSport_WhenProfileExists()
    {
        // Arrange
        var fakeProfile = new FakeProfile().Generate();
        DbContext.Profiles.Add(fakeProfile);
        await DbContext.SaveChangesAsync();

        var fakeSport = new FakeSport().Generate();
        DbContext.Sports.Add(fakeSport);
        await DbContext.SaveChangesAsync();

        var command = new AddProfileSportCommand(fakeProfile.Id.Value, fakeSport.Id.Value);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.Should().Be(Unit.Value);
        fakeProfile.ProfileSports.Single().SportId.Value.Should().Be(fakeSport.Id);
    }

    [Fact]
    public async Task AddProfileSport_ShouldThrowProfileNotFoundException_WhenProfileDoesNotExist()
    {
        // Arrange
        var fakeSport = new FakeSport().Generate();
        DbContext.Sports.Add(fakeSport);
        await DbContext.SaveChangesAsync();

        var command = new AddProfileSportCommand(ProfileId.Of(Guid.NewGuid()), fakeSport.Id.Value);

        // Act
        Func<Task> act = async () => { await Sender.Send(command); };

        // Assert
        await act.Should().ThrowAsync<ProfileNotFoundException>();
    }

    [Fact]
    public async Task AddProfileSport_ShouldThrowSportNotFoundException_WhenSportDoesNotExist()
    {
        // Arrange
        var fakeProfile = new FakeProfile().Generate();
        DbContext.Profiles.Add(fakeProfile);
        await DbContext.SaveChangesAsync();

        var command = new AddProfileSportCommand(fakeProfile.Id.Value, SportId.Of(Guid.NewGuid()));

        // Act
        Func<Task> act = async () => { await Sender.Send(command); };

        // Assert
        await act.Should().ThrowAsync<SportNotFoundException>();
    }

    [Fact]
    public async Task AddProfileSport_ShouldThrowArgumentException_WhenCommandIsNull()
    {
        // Arrange
        AddProfileSportCommand command = null;

        // Act
        Func<Task> act = async () => { await Sender.Send(command); };

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }
}