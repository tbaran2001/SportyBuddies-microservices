namespace BuildingBlocks.Messaging.Events.ProfileManagement;

public record ProfileSportRemovedIntegrationEvent : IntegrationEvent
{
    public Guid ProfileId { get; init; }
    public IEnumerable<Guid> PotentialMatches { get; init; }
}