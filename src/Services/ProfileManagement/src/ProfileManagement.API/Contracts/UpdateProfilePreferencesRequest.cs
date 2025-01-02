using ProfileManagement.Domain.Enums;

namespace ProfileManagement.API.Contracts;

public record UpdateProfilePreferencesRequest(int MinAge, int MaxAge, int MaxDistance, Gender PreferredGender);