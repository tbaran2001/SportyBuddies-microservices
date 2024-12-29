using BuildingBlocks.Messaging.Events;
using MassTransit;
using Matching.API.Matching.CreateMatches;

namespace Matching.API.EventHandlers;

public class ProfileSportRemovedIntegrationEventHandler(ISender sender) : IConsumer<ProfileSportRemovedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ProfileSportRemovedIntegrationEvent> context)
    {
        var command = new CreateMatchesCommand(context.Message.ProfileId);
        await sender.Send(command);
    }
}