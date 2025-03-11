namespace ProfileManagement.API.Profiles.Models.ReadModels;

public class ProfileSportReadModel
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public required Guid Id { get; init; }

    [BsonRepresentation(BsonType.String)] public required Guid ProfileSportId { get; init; }
    [BsonRepresentation(BsonType.String)] public required Guid ProfileId { get; init; }
    [BsonRepresentation(BsonType.String)] public required Guid SportId { get; init; }
}