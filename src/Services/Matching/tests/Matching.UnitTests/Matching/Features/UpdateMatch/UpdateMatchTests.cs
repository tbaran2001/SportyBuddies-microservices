using NSubstitute.ReturnsExtensions;

namespace Matching.UnitTests.Matching.Features.UpdateMatch;

public class UpdateMatchTests
{
    private readonly UpdateMatchCommandHandler _handler;
    private readonly IMatchesRepository _matchesRepository = Substitute.For<IMatchesRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private Task<UpdateMatchResult> Act(UpdateMatchCommand command, CancellationToken cancellationToken) =>
        _handler.Handle(command, cancellationToken);

    public UpdateMatchTests()
    {
        _handler = new UpdateMatchCommandHandler(_matchesRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldUpdateMatch_WhenMatchExists()
    {
        // Arrange
        var (fakeMatch1, fakeMatch2) = FakeMatchCreatePair.Generate();
        var command = new UpdateMatchCommand(fakeMatch1.Id, Swipe.Right);

        _matchesRepository.GetMatchByIdAsync(fakeMatch1.Id).Returns(fakeMatch1);
        _matchesRepository.GetMatchByIdAsync(fakeMatch2.Id).Returns(fakeMatch2);

        // Act
        var result = await Act(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<UpdateMatchResult>();
        fakeMatch1.Swipe.Should().Be(Swipe.Right);

        await _matchesRepository.Received(1).GetMatchByIdAsync(fakeMatch1.Id);
        await _matchesRepository.Received(1).GetMatchByIdAsync(fakeMatch2.Id);
        await _unitOfWork.Received(1).CommitChangesAsync();
    }

    [Fact]
    public async Task Handle_ShouldThrowMatchNotFoundException_WhenMatchDoesNotExist()
    {
        // Arrange
        var command = new UpdateMatchCommand(Guid.NewGuid(), Swipe.Right);

        _matchesRepository.GetMatchByIdAsync(command.MatchId).ReturnsNull();

        // Act
        Func<Task> act = async () => { await Act(command, CancellationToken.None); };

        // Assert
        await act.Should().ThrowAsync<MatchNotFoundException>();

        await _matchesRepository.Received(1).GetMatchByIdAsync(command.MatchId);
        await _unitOfWork.DidNotReceive().CommitChangesAsync();
    }

    [Fact]
    public async Task Handle_ShouldThrowMatchNotFoundException_WhenOppositeMatchDoesNotExist()
    {
        // Arrange
        var (fakeMatch1, fakeMatch2) = FakeMatchCreatePair.Generate();
        var command = new UpdateMatchCommand(fakeMatch1.Id, Swipe.Right);

        _matchesRepository.GetMatchByIdAsync(fakeMatch1.Id).Returns(fakeMatch1);
        _matchesRepository.GetMatchByIdAsync(fakeMatch2.Id).ReturnsNull();

        // Act
        Func<Task> act = async () => { await Act(command, CancellationToken.None); };

        // Assert
        await act.Should().ThrowAsync<MatchNotFoundException>();

        await _matchesRepository.Received(1).GetMatchByIdAsync(fakeMatch1.Id);
        await _matchesRepository.Received(1).GetMatchByIdAsync(fakeMatch2.Id);
        await _unitOfWork.DidNotReceive().CommitChangesAsync();
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