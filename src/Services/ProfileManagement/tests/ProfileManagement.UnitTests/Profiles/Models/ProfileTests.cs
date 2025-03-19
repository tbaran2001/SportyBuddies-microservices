namespace ProfileManagement.UnitTests.Profiles.Models;

public class ProfileTests : BaseDomainTest
{
    [Fact]
    public void CreateSimple_ShouldCreateProfile_WhenValidData()
    {
        // Arrange
        var fakeProfile = new FakeCreateProfileCommand().Generate();

        // Act
        var createdProfile = Profile.CreateSimple(
            ProfileId.Of(fakeProfile.ProfileId),
            Name.Of(fakeProfile.Name),
            Description.Of(fakeProfile.Description)
        );

        // Assert
        createdProfile.Should().NotBeNull();
        createdProfile.Id.Value.Should().Be(fakeProfile.ProfileId);
        createdProfile.Name.Value.Should().Be(fakeProfile.Name);
        createdProfile.Description.Value.Should().Be(fakeProfile.Description);
        createdProfile.BirthDate.Value.Should().Be(new DateTime(1990, 1, 1));
        createdProfile.Gender.Should().Be(Gender.Unknown);
        createdProfile.Preferences.Should().Be(Preferences.Default);
        createdProfile.ProfileSports.Should().BeEmpty();

        var domainEvent = AssertDomainEventWasPublished<ProfileCreatedDomainEvent>(createdProfile);
        domainEvent.ProfileId.Value.Should().Be(fakeProfile.ProfileId);
    }

    [Fact]
    public void Update_ShouldUpdateProfile_WhenValidData()
    {
        // Arrange
        var fakeProfile = FakeProfileCreate.Generate();

        // Act
        FakeProfileUpdate.Generate(fakeProfile);

        // Assert
        fakeProfile.Should().NotBeNull();
        fakeProfile.Name.Should().Be(fakeProfile.Name);
        fakeProfile.Description.Should().Be(fakeProfile.Description);
        fakeProfile.BirthDate.Should().Be(fakeProfile.BirthDate);
        fakeProfile.Gender.Should().Be(fakeProfile.Gender);

        var domainEvent = AssertDomainEventWasPublished<ProfileUpdatedDomainEvent>(fakeProfile);
        domainEvent.Id.Should().Be(fakeProfile.Id);
    }

    [Fact]
    public void AddSport_ShouldAddSport_WhenProfileDoesNotHaveSport()
    {
        // Arrange
        var fakeProfile = FakeProfileCreate.Generate();

        // Act
        FakeProfileAddSport.Generate(fakeProfile);

        // Assert
        fakeProfile.ProfileSports.Count.Should().Be(1);

        var domainEvent = AssertDomainEventWasPublished<ProfileSportAddedDomainEvent>(fakeProfile);
        domainEvent.ProfileId.Should().Be(fakeProfile.Id);
    }

    [Fact]
    public void AddSport_ShouldThrowDomainException_WhenProfileAlreadyHasSport()
    {
        // Arrange
        var fakeProfile = FakeProfileCreate.Generate();
        var fakeSportId = SportId.Of(Guid.NewGuid());
        FakeProfileAddSport.Generate(fakeProfile, fakeSportId);

        // Act
        Action act = () => FakeProfileAddSport.Generate(fakeProfile, fakeSportId);

        // Assert
        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void RemoveSport_ShouldRemoveSport_WhenProfileHasSport()
    {
        // Arrange
        var fakeProfile = FakeProfileCreate.Generate();
        var fakeSportId = SportId.Of(Guid.NewGuid());
        FakeProfileAddSport.Generate(fakeProfile, fakeSportId);

        // Act
        FakeProfileRemoveSport.Generate(fakeProfile, fakeSportId);

        // Assert
        fakeProfile.ProfileSports.Should().BeEmpty();

        var domainEvent = AssertDomainEventWasPublished<ProfileSportRemovedDomainEvent>(fakeProfile);
        domainEvent.ProfileId.Should().Be(fakeProfile.Id);
    }

    [Fact]
    public void RemoveSport_ShouldThrowDomainException_WhenProfileDoesNotHaveSport()
    {
        // Arrange
        var fakeProfile = FakeProfileCreate.Generate();
        var fakeSportId = SportId.Of(Guid.NewGuid());

        // Act
        Action act = () => FakeProfileRemoveSport.Generate(fakeProfile, fakeSportId);

        // Assert
        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void UpdatePreferences_ShouldUpdatePreferences_WhenValidData()
    {
        // Arrange
        var fakeProfile = FakeProfileCreate.Generate();

        // Act
        FakeProfileUpdatePreferences.Generate(fakeProfile);

        // Assert
        fakeProfile.Preferences.Should().NotBeNull();
        fakeProfile.Preferences.Should().Be(Preferences.Default);
    }
}