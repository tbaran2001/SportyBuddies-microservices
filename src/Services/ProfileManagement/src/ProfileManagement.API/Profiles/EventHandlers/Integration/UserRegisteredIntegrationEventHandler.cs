﻿using BuildingBlocks.Events.Identity;
using MassTransit;
using MediatR;
using ProfileManagement.API.Profiles.Features.Commands.CreateProfile;

namespace ProfileManagement.API.Profiles.EventHandlers.Integration;

public class UserRegisteredIntegrationEventHandler(ISender sender) : IConsumer<UserRegisteredIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
    {
        var command = new CreateProfileCommand(context.Message.UserId, context.Message.Name, "haha description");
        await sender.Send(command);
    }
}