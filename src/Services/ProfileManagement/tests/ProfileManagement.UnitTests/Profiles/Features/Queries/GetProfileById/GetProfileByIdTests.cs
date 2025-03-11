namespace ProfileManagement.UnitTests.Profiles.Features.Queries.GetProfileById;

public class GetProfileByIdTests
{
    private readonly GetProfileByIdQueryHandler _handler;
    private readonly IProfilesReadRepository _profilesRepository = Substitute.For<IProfilesReadRepository>();

    private Task<GetProfileByIdResult> Act(GetProfileByIdQuery query, CancellationToken cancellationToken) =>
        _handler.Handle(query, cancellationToken);

    public GetProfileByIdTests()
    {
        _handler = new GetProfileByIdQueryHandler(_profilesRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnProfile_WhenProfileExists()
    {
        // Arrange
        var query = new GetProfileByIdQuery(Guid.NewGuid());
        var fakeProfile = FakeProfileCreate.Generate();
        _profilesRepository.GetProfileByIdAsync(query.Id).Returns(fakeProfile.Adapt<ProfileReadModel>());

        // Act
        var result = await Act(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<GetProfileByIdResult>();
        result.Profile.Id.Should().Be(fakeProfile.Id);

        await _profilesRepository.Received(1).GetProfileByIdAsync(query.Id);
    }

    [Fact]
    public async Task Handle_ShouldThrowProfileNotFoundException_WhenProfileDoesNotExist()
    {
        // Arrange
        var query = new GetProfileByIdQuery(Guid.NewGuid());
        _profilesRepository.GetProfileByIdAsync(query.Id).ReturnsNull();

        // Act
        Func<Task> act = async () => { await Act(query, CancellationToken.None); };

        // Assert
        await act.Should().ThrowAsync<ProfileNotFoundException>();

        await _profilesRepository.Received(1).GetProfileByIdAsync(query.Id);
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