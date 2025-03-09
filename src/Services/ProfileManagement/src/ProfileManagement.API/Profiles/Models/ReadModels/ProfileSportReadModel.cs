namespace ProfileManagement.API.Profiles.Models.ReadModels;

public class ProfileSportReadModel
{
    public required Guid Id { get; init; }
    public required Guid ProfileSportId { get; init; }
    public required Guid ProfileId { get; init; }
    public required Guid SportId { get; init; }
}