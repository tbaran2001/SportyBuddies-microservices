namespace BuildingBlocks.Events.Matching;

public record BothSwipedRightIntegrationEvent : IntegrationEvent
{
    public Guid RequestId { get; init; }
    public Guid ProfileId { get; init; }
    public Guid MatchedProfileId { get; init; }
}