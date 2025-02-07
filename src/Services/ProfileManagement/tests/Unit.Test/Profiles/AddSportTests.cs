using BuildingBlocks.Exceptions;
using FluentAssertions;
using ProfileManagement.API.Profiles.Features.Commands.AddProfileSport;
using ProfileManagement.API.Profiles.Models;
using ProfileManagement.API.Profiles.ValueObjects;

namespace Unit.Test.Profiles;

public class AddSportTests
{
    [Fact]
    public void AddSport_WhenUserDoesNotHaveSport_ShouldAddSport()
    {
        // Arrange
        var user = Profile.CreateSimple(Guid.NewGuid(),Name.Of("User"), Description.Of("Description"));

        // Act
        user.AddSport(Guid.NewGuid());

        // Assert
        user.ProfileSports.Should().ContainSingle();
    }

    [Fact]
    public void AddSport_WhenUserHasSport_ShouldThrowException()
    {
        // Arrange
        var user = Profile.CreateSimple(Guid.NewGuid(),Name.Of("User"), Description.Of("Description"));
        var sportId = Guid.NewGuid();
        user.AddSport(sportId);

        // Act
        var act = () => user.AddSport(sportId);

        // Assert
        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void AddSport_Should_RaiseSportAddedDomainEvent()
    {
        // Arrange
        var user = Profile.CreateSimple(Guid.NewGuid(),Name.Of("User"), Description.Of("Description"));
        var sportId = Guid.NewGuid();

        // Act
        user.AddSport(sportId);

        // Assert
        user.DomainEvents.Should().ContainSingle(e => e is ProfileSportAddedDomainEvent);
    }
}