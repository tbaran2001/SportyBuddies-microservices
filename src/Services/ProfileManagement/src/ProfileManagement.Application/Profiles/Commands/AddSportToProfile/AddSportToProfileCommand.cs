namespace ProfileManagement.Application.Profiles.Commands.AddSportToProfile;

public record AddSportToProfileCommand(Guid SportId) : ICommand;