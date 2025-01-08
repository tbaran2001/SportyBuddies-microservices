using ProfileManagement.API.Profiles.Enums;
using ProfileManagement.API.Profiles.ValueObjects;

namespace ProfileManagement.API.Profiles.Dtos;

public record ProfileDto(
    Guid Id,
    string Name,
    string Description,
    DateTime CreatedOnUtc,
    Gender Gender,
    DateOnly DateOfBirth,
    Preferences Preferences,
    List<ProfileSportDto> ProfileSports);