namespace ProfileManagement.Application.Profiles.Commands.AddSportToProfile;

public record AddSportToProfileCommand(Guid ProfileId, Guid SportId) : ICommand;