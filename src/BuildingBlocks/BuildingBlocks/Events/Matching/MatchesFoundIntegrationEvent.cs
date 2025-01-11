namespace BuildingBlocks.Events.Matching;

public record MatchesFoundIntegrationEvent : IntegrationEvent
{
    public Guid RequestId { get; init; }
    public Guid[] MatchedProfileIds { get; init; }
}