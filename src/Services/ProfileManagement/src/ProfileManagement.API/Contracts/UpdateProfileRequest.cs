using ProfileManagement.Domain.Enums;

namespace ProfileManagement.API.Contracts;

public record UpdateProfileRequest(string Name, string Description, Gender Gender, DateOnly DateOfBirth);