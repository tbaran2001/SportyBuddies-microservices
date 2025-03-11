namespace ProfileManagement.UnitTests.Profiles.Features.Commands.RemoveProfileSport;

public class RemoveProfileSportTests
{
    private readonly RemoveProfileSportCommandHandler _handler;
    private readonly IProfilesRepository _profilesRepository = Substitute.For<IProfilesRepository>();
    private readonly ISportsRepository _sportsRepository = Substitute.For<ISportsRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private Task<Unit> Act(RemoveProfileSportCommand command, CancellationToken cancellationToken) =>
        _handler.Handle(command, cancellationToken);

    public RemoveProfileSportTests()
    {
        _handler = new RemoveProfileSportCommandHandler(
            _profilesRepository,
            _sportsRepository,
            _unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldRemoveProfileSport_WhenValidData()
    {
        // Arrange
        var profile = FakeProfileCreate.Generate();
        var command = new FakeRemoveProfileSportCommand().Generate();
        profile.AddSport(SportId.Of(command.SportId));
        _profilesRepository.GetProfileByIdAsync(command.ProfileId).Returns(profile);
        _sportsRepository.SportExistsAsync(command.SportId).Returns(true);

        // Act
        var result = await Act(command, CancellationToken.None);

        // Assert
        profile.ProfileSports.Should().BeEmpty();
        result.Should().Be(Unit.Value);

        await _unitOfWork.Received(1).CommitChangesAsync();
    }

    [Fact]
    public async Task Handle_ShouldThrowProfileNotFoundException_WhenProfileDoesNotExist()
    {
        // Arrange
        var command = new FakeRemoveProfileSportCommand().Generate();
        _profilesRepository.GetProfileByIdAsync(command.ProfileId).ReturnsNull();

        // Act
        Func<Task> act = async () => { await Act(command, CancellationToken.None); };

        // Assert
        await act.Should().ThrowAsync<ProfileNotFoundException>();
    }

    [Fact]
    public async Task Handle_ShouldThrowSportNotFoundException_WhenSportDoesNotExist()
    {
        // Arrange
        var profile = FakeProfileCreate.Generate();
        var command = new FakeRemoveProfileSportCommand().Generate();
        _profilesRepository.GetProfileByIdAsync(command.ProfileId).Returns(profile);
        _sportsRepository.SportExistsAsync(command.SportId).Returns(false);

        // Act
        Func<Task> act = async () => { await Act(command, CancellationToken.None); };

        // Assert
        await act.Should().ThrowAsync<SportNotFoundException>();
    }

    [Fact]
    public async Task Handle_ShouldThrowArgumentException_WhenCommandIsNull()
    {
        // Act
        Func<Task> act = async () => { await Act(null, CancellationToken.None); };

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }
}