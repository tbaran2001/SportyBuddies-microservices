namespace ProfileManagement.UnitTests.Profiles.Features.Queries.GetCurrentProfile;

public class GetCurrentProfileTests
{
    private readonly GetCurrentProfileQueryHandler _handler;
    private readonly IProfilesReadRepository _profileRepository = Substitute.For<IProfilesReadRepository>();
    private readonly ICurrentUserProvider _currentUserProvider = Substitute.For<ICurrentUserProvider>();

    private Task<GetCurrentProfileResult> Act(GetCurrentProfileQuery query, CancellationToken cancellationToken) =>
        _handler.Handle(query, cancellationToken);

    public GetCurrentProfileTests()
    {
        _handler = new GetCurrentProfileQueryHandler(_profileRepository, _currentUserProvider);
    }

    [Fact]
    public async Task Handle_ShouldReturnCurrentProfile_WhenProfileExists()
    {
        // Arrange
        var fakeProfile = FakeProfileCreate.Generate();
        _currentUserProvider.GetCurrentUserId().Returns(fakeProfile.Id);
        _profileRepository.GetProfileByIdAsync(fakeProfile.Id).Returns(fakeProfile.Adapt<ProfileReadModel>());

        var query = new GetCurrentProfileQuery();

        // Act
        var result = await Act(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<GetCurrentProfileResult>();
        result.Profile.Should().NotBeNull();
        result.Profile.Id.Should().Be(fakeProfile.Id);
    }

    [Fact]
    public async Task Handle_ShouldThrowProfileNotFoundException_WhenProfileDoesNotExist()
    {
        // Arrange
        var fakeProfile = FakeProfileCreate.Generate();
        _currentUserProvider.GetCurrentUserId().Returns(fakeProfile.Id);
        _profileRepository.GetProfileByIdAsync(fakeProfile.Id).ReturnsNull();

        var query = new GetCurrentProfileQuery();

        // Act
        Func<Task> act = async () => { await Act(query, CancellationToken.None); };

        // Assert
        await act.Should().ThrowAsync<ProfileNotFoundException>();
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