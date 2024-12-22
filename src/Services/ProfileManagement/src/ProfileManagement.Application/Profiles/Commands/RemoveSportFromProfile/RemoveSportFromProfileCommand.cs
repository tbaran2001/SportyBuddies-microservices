namespace ProfileManagement.Application.Profiles.Commands.RemoveSportFromProfile;

public record RemoveSportFromProfileCommand(Guid ProfileId, Guid SportId) : ICommand;