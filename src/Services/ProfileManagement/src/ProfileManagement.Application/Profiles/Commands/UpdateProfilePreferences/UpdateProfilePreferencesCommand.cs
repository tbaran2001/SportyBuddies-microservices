namespace ProfileManagement.Application.Profiles.Commands.UpdateProfilePreferences;

public record UpdateProfilePreferencesCommand(int MinAge, int MaxAge, int MaxDistance, Gender PreferredGender)
    : ICommand;