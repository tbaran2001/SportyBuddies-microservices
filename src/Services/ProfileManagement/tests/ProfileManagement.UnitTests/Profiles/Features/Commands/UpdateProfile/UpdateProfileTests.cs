namespace ProfileManagement.UnitTests.Profiles.Features.Commands.UpdateProfile;

public class UpdateProfileTests
{
    private readonly UpdateProfileCommandHandler _handler;
    private readonly IProfilesRepository _profilesRepository = Substitute.For<IProfilesRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private Task<UpdateProfileResult> Act(UpdateProfileCommand command, CancellationToken cancellationToken) =>
        _handler.Handle(command, cancellationToken);

    public UpdateProfileTests()
    {
        _handler = new UpdateProfileCommandHandler(_profilesRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldUpdateProfile_WhenProfileExists()
    {
        // Arrange
        var fakeProfile = FakeProfileCreate.Generate();
        var command = new UpdateProfileCommand(fakeProfile.Id, "Xd", fakeProfile.Description,
            fakeProfile.Gender, fakeProfile.BirthDate);
        _profilesRepository.GetProfileByIdAsync(command.ProfileId).Returns(fakeProfile);

        // Act
        var result = await Act(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(command.ProfileId);

        await _unitOfWork.Received(1).CommitChangesAsync();
    }

    [Fact]
    public async Task Handle_ShouldThrowProfileNotFoundException_WhenProfileDoesNotExist()
    {
        // Arrange
        var fakeProfile = FakeProfileCreate.Generate();
        var command = new UpdateProfileCommand(fakeProfile.Id, "Xd", fakeProfile.Description,
            fakeProfile.Gender, fakeProfile.BirthDate);
        _profilesRepository.GetProfileByIdAsync(command.ProfileId).ReturnsNull();

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