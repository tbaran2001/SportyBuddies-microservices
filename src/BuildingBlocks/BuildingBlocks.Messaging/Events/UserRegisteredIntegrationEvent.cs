namespace BuildingBlocks.Messaging.Events;

public record UserRegisteredIntegrationEvent: IntegrationEvent
{
    public Guid UserId { get; init; }
    public string Name { get; init; }
}