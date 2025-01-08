using BuildingBlocks.Messaging.Events.ProfileManagement;
using MassTransit;
using Matching.API.Matching.Features.CreateMatches;

namespace Matching.API.Matching.EventHandlers.Integration;

public class ProfileSportAddedIntegrationEventHandler(ISender sender) : IConsumer<ProfileSportAddedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ProfileSportAddedIntegrationEvent> context)
    {
        var potentialMatches= context.Message.PotentialMatches;

        foreach (var match in potentialMatches)
        {
            await sender.Send(new CreateMatchesCommand(context.Message.ProfileId, match));
        }
    }
}