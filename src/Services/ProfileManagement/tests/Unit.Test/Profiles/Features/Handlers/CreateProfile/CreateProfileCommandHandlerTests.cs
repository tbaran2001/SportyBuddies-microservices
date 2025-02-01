using BuildingBlocks.Core.Model;
using FluentAssertions;
using NSubstitute;
using ProfileManagement.API.Data.Repositories;
using ProfileManagement.API.Profiles.Features.Commands.CreateProfile;
using ProfileManagement.API.Profiles.Models;
using Unit.Test.Common;
using Unit.Test.Fakes;

namespace Unit.Test.Profiles.Features.Handlers.CreateProfile;

[Collection(nameof(UnitTestFixture))]
public class CreateProfileCommandHandlerTests
{
    private readonly UnitTestFixture _fixture;
    private readonly CreateProfileCommandHandler _handler;
    private readonly IProfilesRepository _profilesRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public Task<CreateProfileResult> Act(CreateProfileCommand command, CancellationToken cancellationToken) =>
        _handler.Handle(command, cancellationToken);

    public CreateProfileCommandHandlerTests(UnitTestFixture fixture)
    {
        _fixture = fixture;
        _profilesRepositoryMock = Substitute.For<IProfilesRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _handler = new CreateProfileCommandHandler(_profilesRepositoryMock, _unitOfWorkMock);
    }

    [Fact]
    public async Task handler_with_valid_command_should_create_new_profile_and_return_correct_profile_dto()
    {
        // Arrange
        var command = new FakeCreateProfileCommand().Generate();

        // Act
        var result = await Act(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Profile.Id.Should().Be(command.ProfileId);
        await _profilesRepositoryMock.Received(1).AddProfileAsync(Arg.Any<Profile>());
        await _unitOfWorkMock.Received(1).CommitChangesAsync();
    }
}