namespace BuildingBlocks.Messaging.Events;

public record ProfileSportAddedIntegrationEvent : IntegrationEvent
{
    public Guid ProfileId { get; init; }
    public Guid SportId { get; init; }
}