namespace BuildingBlocks.Events.Identity;

public record UserRegisteredIntegrationEvent: IntegrationEvent
{
    public Guid UserId { get; init; }
    public string Name { get; init; }
}