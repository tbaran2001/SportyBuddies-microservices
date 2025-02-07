using BuildingBlocks.Exceptions;
using FluentAssertions;
using ProfileManagement.API.Profiles.Features.Commands.RemoveProfileSport;
using ProfileManagement.API.Profiles.Models;
using ProfileManagement.API.Profiles.ValueObjects;

namespace Unit.Test.Profiles;

public class RemoveSportTests
{
    [Fact]
    public void RemoveSport_WhenUserHasSport_ShouldRemoveSport()
    {
        // Arrange
        var user = Profile.CreateSimple(Guid.NewGuid(), Name.Of("User"), Description.Of("Description"));
        var sportId = Guid.NewGuid();
        user.AddSport(sportId);

        // Act
        user.RemoveSport(sportId);

        // Assert
        user.ProfileSports.Should().BeEmpty();
    }

    [Fact]
    public void RemoveSport_WhenUserDoesNotHaveSport_ShouldThrowException()
    {
        // Arrange
        var user = Profile.CreateSimple(Guid.NewGuid(), Name.Of("User"), Description.Of("Description"));
        var sportId = Guid.NewGuid();

        // Act
        var act = () => user.RemoveSport(sportId);

        // Assert
        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void RemoveSport_Should_RaiseSportRemovedDomainEvent()
    {
        // Arrange
        var user = Profile.CreateSimple(Guid.NewGuid(), Name.Of("User"), Description.Of("Description"));
        var sportId = Guid.NewGuid();
        user.AddSport(sportId);

        // Act
        user.RemoveSport(sportId);

        // Assert
        user.DomainEvents.Should().ContainSingle(e => e is ProfileSportRemovedDomainEvent);
    }
}