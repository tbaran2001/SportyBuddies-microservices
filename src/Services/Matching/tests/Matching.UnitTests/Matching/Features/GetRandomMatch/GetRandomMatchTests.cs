using BuildingBlocks.Web;
using Mapster;
using Matching.API.Matching.Dtos;
using Matching.API.Matching.Features.GetRandomMatch;
using NSubstitute.ReturnsExtensions;

namespace Matching.UnitTests.Matching.Features.GetRandomMatch;

public class GetRandomMatchTests
{
    private readonly GetRandomMatchQueryHandler _handler;
    private readonly IMatchesRepository _matchesRepository = Substitute.For<IMatchesRepository>();
    private readonly ICurrentUserProvider _currentUserProvider = Substitute.For<ICurrentUserProvider>();

    private Task<GetRandomMatchResult> Act(GetRandomMatchQuery query, CancellationToken cancellationToken) =>
        _handler.Handle(query, cancellationToken);

    public GetRandomMatchTests()
    {
        _handler = new GetRandomMatchQueryHandler(_matchesRepository, _currentUserProvider);
    }

    [Fact]
    public async Task Handle_ShouldReturnMatch_WhenMatchExists()
    {
        // Arrange
        var (fakeMatch1, _) = FakeMatchCreatePair.Generate();
        _currentUserProvider.GetCurrentUserId().Returns(fakeMatch1.ProfileId.Value);
        _matchesRepository.GetRandomMatchAsync(fakeMatch1.ProfileId, Arg.Any<CancellationToken>()).Returns(fakeMatch1);

        var query = new GetRandomMatchQuery();

        // Act
        var result = await Act(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<GetRandomMatchResult>();
        result.Match.Should().NotBeNull();
    }

    [Fact]
    public async Task Handle_ShouldThrowRandomMatchNotFoundException_WhenMatchDoesNotExist()
    {
        // Arrange
        var (fakeMatch1, _) = FakeMatchCreatePair.Generate();
        _currentUserProvider.GetCurrentUserId().Returns(fakeMatch1.ProfileId.Value);
        _matchesRepository.GetRandomMatchAsync(fakeMatch1.ProfileId, Arg.Any<CancellationToken>()).ReturnsNull();

        var query = new GetRandomMatchQuery();

        // Act
        Func<Task> act = async () => { await Act(query, CancellationToken.None); };

        // Assert
        await act.Should().ThrowAsync<RandomMatchNotFoundException>();
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