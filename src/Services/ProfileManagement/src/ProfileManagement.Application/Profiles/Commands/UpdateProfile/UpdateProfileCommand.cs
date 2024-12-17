namespace ProfileManagement.Application.Profiles.Commands.UpdateProfile;

public record UpdateProfileCommand(Guid Id,string Name, string Description, Gender Gender, DateOnly DateOfBirth)
    : ICommand<ProfileResponse>;