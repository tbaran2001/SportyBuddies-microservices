namespace ProfileManagement.UnitTests.Profiles.Features.Queries.GetProfiles;

public class GetProfilesTests
{
    private readonly GetProfilesQueryHandler _handler;
    private readonly IProfilesReadRepository _profileRepository = Substitute.For<IProfilesReadRepository>();

    private Task<GetProfilesResult> Act(GetProfilesQuery query, CancellationToken cancellationToken) =>
        _handler.Handle(query, cancellationToken);

    public GetProfilesTests()
    {
        _handler = new GetProfilesQueryHandler(_profileRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnProfiles_WhenProfilesExist()
    {
        // Arrange
        var fakeProfiles = new List<Profile>
        {
            FakeProfileCreate.Generate(),
            FakeProfileCreate.Generate(),
            FakeProfileCreate.Generate()
        };
        _profileRepository.GetProfilesAsync().Returns(fakeProfiles.Adapt<IEnumerable<ProfileReadModel>>());

        var query = new GetProfilesQuery();

        // Act
        var result = await Act(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<GetProfilesResult>();
        result.Profiles.Should().NotBeEmpty();
        result.Profiles.Should().HaveCount(3);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoProfilesExist()
    {
        // Arrange
        _profileRepository.GetProfilesAsync().Returns(new List<ProfileReadModel>());

        var query = new GetProfilesQuery();

        // Act
        var result = await Act(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Profiles.Should().BeEmpty();
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