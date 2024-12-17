using ProfileManagement.Domain.Enums;
using ProfileManagement.Domain.ValueObjects;

namespace ProfileManagement.Application.DTOs;

public record ProfileResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime CreatedOnUtc,
    Gender Gender,
    DateOnly DateOfBirth,
    Preferences Preferences,
    List<ProfileSportResponse> ProfileSports);