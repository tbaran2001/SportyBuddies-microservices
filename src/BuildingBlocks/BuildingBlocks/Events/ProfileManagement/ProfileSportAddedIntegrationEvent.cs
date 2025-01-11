namespace BuildingBlocks.Events.ProfileManagement;

public record ProfileSportAddedIntegrationEvent : IntegrationEvent
{
    public Guid ProfileId { get; init; }
}