namespace BuildingBlocks.Events.ProfileManagement;

public record ProfileSportRemovedIntegrationEvent : IntegrationEvent
{
    public Guid ProfileId { get; init; }
}