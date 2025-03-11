namespace ProfileManagement.API.Profiles.EventHandlers.Integration;

public class UserRegisteredIntegrationEventHandler(ISender sender) : IConsumer<UserRegisteredIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
    {
        var command = new CreateProfileCommand(context.Message.UserId, context.Message.Name, "haha description");
        await sender.Send(command);
    }
}