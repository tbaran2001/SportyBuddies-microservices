namespace ProfileManagement.UnitTests.Profiles.Features.Commands.UpdateProfilePreferences;

public class UpdateProfilePreferencesTests
{
    private readonly UpdateProfilePreferencesCommandHandler _handler;
    private readonly IProfilesRepository _profileRepository = Substitute.For<IProfilesRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private Task<Unit> Act(UpdateProfilePreferencesCommand command, CancellationToken cancellationToken) =>
        _handler.Handle(command, cancellationToken);

    public UpdateProfilePreferencesTests()
    {
        _handler = new UpdateProfilePreferencesCommandHandler(_profileRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldUpdateProfilePreferences_WhenValidData()
    {
        // Arrange
        var fakeProfile = FakeProfileCreate.Generate();
        var command = new FakeUpdateProfilePreferencesCommand().Generate();
        _profileRepository.GetProfileByIdAsync(command.ProfileId).Returns(fakeProfile);

        // Act
        var result = await Act(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
        fakeProfile.Preferences.MinAge.Should().Be(command.MinAge);
        fakeProfile.Preferences.MaxAge.Should().Be(command.MaxAge);
        fakeProfile.Preferences.MaxDistance.Should().Be(command.MaxDistance);
        fakeProfile.Preferences.PreferredGender.Should().Be(command.PreferredGender);

        await _unitOfWork.Received(1).CommitChangesAsync();
    }

    [Fact]
    public async Task Handle_ShouldThrowProfileNotFoundException_WhenProfileDoesNotExist()
    {
        // Arrange
        var command = new FakeUpdateProfilePreferencesCommand().Generate();
        _profileRepository.GetProfileByIdAsync(command.ProfileId).ReturnsNull();

        // Act
        Func<Task> act = async () => { await Act(command, CancellationToken.None); };

        // Assert
        await act.Should().ThrowAsync<ProfileNotFoundException>();
    }

    [Fact]
    public async Task Handle_ShouldThrowArgumentNullException_WhenCommandIsNull()
    {
        // Act
        Func<Task> act = async () => { await Act(null, CancellationToken.None); };

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }
}