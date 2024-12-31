namespace BuildingBlocks.Messaging.Events.ProfileManagement;

public record ProfileSportRemovedIntegrationEvent : IntegrationEvent
{
    public Guid ProfileId { get; init; }
    public Guid SportId { get; init; }
}