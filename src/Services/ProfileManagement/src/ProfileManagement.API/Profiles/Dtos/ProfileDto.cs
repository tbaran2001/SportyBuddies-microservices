namespace ProfileManagement.API.Profiles.Dtos;

public record ProfileDto(
    Guid Id,
    string Name,
    string Description,
    Gender Gender,
    DateTime BirthDate,
    Preferences Preferences,
    List<ProfileSportDto> ProfileSports);