namespace BuildingBlocks.Messaging.Events;

public record ProfileSportAddedEvent : IntegrationEvent
{
    public Guid ProfileId { get; init; }
    public Guid SportId { get; init; }
}