using BuildingBlocks.Core.Model;
using Matching.API.Data.Repositories;
using Matching.API.Matching.Features.CreateMatches;
using MediatR;
using NSubstitute;

namespace Matching.UnitTests.Matching.Features.CreateMatches;

public class CreateMatchesTests
{
    private readonly CreateMatchesCommandHandler _handler;
    private readonly IMatchesRepository _matchesRepository = Substitute.For<IMatchesRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private Task<Unit> Act(CreateMatchesCommand command, CancellationToken cancellationToken) =>
        _handler.Handle(command, cancellationToken);

    public CreateMatchesTests()
    {
        _handler=new CreateMatchesCommandHandler(_matchesRepository,_unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldCreateMatches_WhenMatchesDoesNotExist()
    {
        // Arrange
        var profileId = Guid.NewGuid();
        var matchedProfileId = Guid.NewGuid();
        var command = new CreateMatchesCommand(profileId, matchedProfileId);
        
        _matchesRepository.CheckIfMatchExistsAsync(profileId, matchedProfileId, Arg.Any<CancellationToken>())
            .Returns(false);

        // Act
        var result = await Act(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Unit>();

        await _matchesRepository.Received(1).CheckIfMatchExistsAsync(command.ProfileId, command.MatchedProfileId, Arg.Any<CancellationToken>());
        await _matchesRepository.Received(1).AddMatchesAsync(Arg.Any<List<Match>>(), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(1).CommitChangesAsync();
    }
    
    [Fact]
    public async Task Handle_ShouldNotCreateMatches_WhenMatchesExist()
    {
        // Arrange
        var profileId = Guid.NewGuid();
        var matchedProfileId = Guid.NewGuid();
        var command = new CreateMatchesCommand(profileId, matchedProfileId);
        
        _matchesRepository.CheckIfMatchExistsAsync(profileId, matchedProfileId, Arg.Any<CancellationToken>())
            .Returns(true);

        // Act
        var result = await Act(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Unit>();

        await _matchesRepository.Received(1).CheckIfMatchExistsAsync(command.ProfileId, command.MatchedProfileId, Arg.Any<CancellationToken>());
        await _matchesRepository.DidNotReceive().AddMatchesAsync(Arg.Any<List<Match>>(), Arg.Any<CancellationToken>());
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