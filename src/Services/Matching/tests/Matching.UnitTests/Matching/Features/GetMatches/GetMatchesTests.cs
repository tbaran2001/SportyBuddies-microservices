using Matching.API.Matching.Features.GetMatches;

namespace Matching.UnitTests.Matching.Features.GetMatches;

public class GetMatchesTests
{
    private readonly GetMatchesQueryHandler _handler;
    private readonly IMatchesRepository _matchesRepository = Substitute.For<IMatchesRepository>();

    private Task<GetMatchesResult> Act(GetMatchesQuery query, CancellationToken cancellationToken) =>
        _handler.Handle(query, cancellationToken);

    public GetMatchesTests()
    {
        _handler = new GetMatchesQueryHandler(_matchesRepository);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnMatches_WhenMatchesExist()
    {
        // Arrange
        var fakeMatches = FakeMatchCreatePair.Generate(2);
        _matchesRepository.GetMatchesAsync(Arg.Any<Guid?>(), Arg.Any<CancellationToken>()).Returns(fakeMatches);

        var query = new GetMatchesQuery(null);

        // Act
        var result = await Act(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<GetMatchesResult>();
        result.Matches.Should().NotBeEmpty();
        result.Matches.Should().HaveCount(4);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoMatchesExist()
    {
        // Arrange
        _matchesRepository.GetMatchesAsync(Arg.Any<Guid?>(), Arg.Any<CancellationToken>()).Returns(new List<Match>());

        var query = new GetMatchesQuery(null);

        // Act
        var result = await Act(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Matches.Should().BeEmpty();
    }
    
    [Fact]
    public async Task Handle_ShouldThrowArgumentNullException_WhenQueryIsNull()
    {
        // Act
        Func<Task> act = async () => { await Act(null, CancellationToken.None); };

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }
}