namespace ProfileManagement.UnitTests.Profiles.Features.Commands.CreateProfile;

public class CreateProfileTests
{
    private readonly CreateProfileCommandHandler _handler;
    private readonly IProfilesRepository _profilesRepository = Substitute.For<IProfilesRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private Task<CreateProfileResult> Act(CreateProfileCommand command, CancellationToken cancellationToken) =>
        _handler.Handle(command, cancellationToken);

    public CreateProfileTests()
    {
        _handler = new CreateProfileCommandHandler(_profilesRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldCreateProfile_WhenProfileDoesNotExist()
    {
        // Arrange
        var command = new FakeCreateProfileCommand().Generate();

        // Act
        var result = await Act(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Profile.Id.Should().Be(command.ProfileId);

        await _profilesRepository.Received(1).AddProfileAsync(Arg.Any<Profile>());
        await _unitOfWork.Received(1).CommitChangesAsync();
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