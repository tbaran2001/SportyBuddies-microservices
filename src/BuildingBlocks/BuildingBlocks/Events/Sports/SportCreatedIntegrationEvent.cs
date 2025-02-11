namespace BuildingBlocks.Events.Sports;

public record SportCreatedIntegrationEvent : IntegrationEvent
{
    public Guid SportId { get; init; }
}