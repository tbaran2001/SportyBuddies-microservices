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
        var command = new UpdateMatchCommand(fakeMatch1.ProfileId, Swipe.Right);

        _matchesRepository.GetMatchByIdAsync(fakeMatch1.Id).Returns(fakeMatch1);
        
    }
}