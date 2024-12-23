using BuildingBlocks.Messaging.Events;
using MassTransit;
using Matching.API.Matching.CreateMatches;

namespace Matching.API.EventHandlers;

public class ProfileSportAddedEventHandler(ISender sender):IConsumer<ProfileSportAddedEvent>
{
    public async Task Consume(ConsumeContext<ProfileSportAddedEvent> context)
    {
        var command = new CreateMatchesCommand(context.Message.ProfileId);
        await sender.Send(command);
    }
}