using ProfileManagement.TestShared.Fakes.Sports.Features;

namespace ProfileManagement.UnitTests.Sports.Features.Commands.CreateSport;

public class CreateSportTests
{
    private readonly CreateSportCommandHandler _handler;
    private readonly ISportsRepository _sportsRepository = Substitute.For<ISportsRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private Task<CreateSportResult> Act(CreateSportCommand command, CancellationToken cancellationToken) =>
        _handler.Handle(command, cancellationToken);

    public CreateSportTests()
    {
        _handler = new CreateSportCommandHandler(_sportsRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldCreateSport_WhenSportDoesNotExist()
    {
        // Arrange
        var command = new FakeCreateSportCommand().Generate();

        // Act
        var result = await Act(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Sport.Id.Should().Be(command.SportId);
        result.Should().BeOfType<CreateSportResult>();

        await _sportsRepository.Received(1).AddSportAsync(Arg.Any<Sport>());
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

    [Fact]
    public async Task Handle_ShouldThrowSportAlreadyExistException_WhenSportExists()
    {
        // Arrange
        var command = new FakeCreateSportCommand().Generate();
        _sportsRepository.SportExistsAsync(command.SportId).Returns(true);

        // Act
        Func<Task> act = async () => { await Act(command, CancellationToken.None); };

        // Assert
        await act.Should().ThrowAsync<SportAlreadyExistException>();
    }
}