using BuildingBlocks.Messaging.Events.ProfileManagement;
using MassTransit;
using Matching.API.Data.Repositories;

namespace Matching.API.Matching.EventHandlers.Integration;

public class ProfileSportRemovedIntegrationEventHandler(ISender sender, IMatchesRepository matchesRepository)
    : IConsumer<ProfileSportRemovedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ProfileSportRemovedIntegrationEvent> context)
    {
        var potentialMatches = context.Message.PotentialMatches;

        await matchesRepository.RemoveMatchesAsync(context.Message.ProfileId, potentialMatches);
    }
}