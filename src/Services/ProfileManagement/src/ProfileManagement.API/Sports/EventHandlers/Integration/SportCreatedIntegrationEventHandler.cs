using BuildingBlocks.Events.Sports;
using ProfileManagement.API.Sports.Features.Commands;

namespace ProfileManagement.API.Sports.EventHandlers.Integration;

public class SportCreatedIntegrationEventHandler(ISender sender) : IConsumer<SportCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<SportCreatedIntegrationEvent> context)
    {
        var command = new CreateSportCommand(context.Message.SportId);
        await sender.Send(command);
    }
}