namespace ProfileManagement.Application.Profiles.Commands.RemoveSportFromProfile;

public record RemoveSportFromProfileCommand(Guid SportId) : ICommand;