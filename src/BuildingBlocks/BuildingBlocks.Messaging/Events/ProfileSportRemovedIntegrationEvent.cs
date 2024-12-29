namespace BuildingBlocks.Messaging.Events;

public record ProfileSportRemovedIntegrationEvent : IntegrationEvent
{
    public Guid ProfileId { get; init; }
    public Guid SportId { get; init; }
}