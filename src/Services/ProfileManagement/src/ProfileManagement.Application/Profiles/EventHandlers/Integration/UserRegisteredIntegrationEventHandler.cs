using BuildingBlocks.Messaging.Events;
using BuildingBlocks.Messaging.Events.Identity;
using MassTransit;
using ProfileManagement.Application.Profiles.Commands.CreateProfile;

namespace ProfileManagement.Application.Profiles.EventHandlers.Integration;

public class UserRegisteredIntegrationEventHandler(ISender sender) : IConsumer<UserRegisteredIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
    {
        var command = new CreateProfileCommand(context.Message.UserId, context.Message.Name, "haha description");
        await sender.Send(command);
    }
}