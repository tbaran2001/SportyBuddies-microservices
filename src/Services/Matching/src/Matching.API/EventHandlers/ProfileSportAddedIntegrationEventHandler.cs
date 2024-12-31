using BuildingBlocks.Messaging.Events;
using BuildingBlocks.Messaging.Events.ProfileManagement;
using MassTransit;
using Matching.API.Matching.CreateMatches;

namespace Matching.API.EventHandlers;

public class ProfileSportAddedIntegrationEventHandler(ISender sender) : IConsumer<ProfileSportAddedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ProfileSportAddedIntegrationEvent> context)
    {
        var command = new CreateMatchesCommand(context.Message.ProfileId);
        await sender.Send(command);
    }
}