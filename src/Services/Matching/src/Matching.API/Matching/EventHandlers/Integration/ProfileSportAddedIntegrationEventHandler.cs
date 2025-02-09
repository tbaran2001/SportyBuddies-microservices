namespace Matching.API.Matching.EventHandlers.Integration;

public class ProfileSportAddedIntegrationEventHandler(
    ISender sender,
    ProfileProtoService.ProfileProtoServiceClient profileProtoService)
    : IConsumer<ProfileSportAddedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ProfileSportAddedIntegrationEvent> context)
    {
        var request = new GetPotentialMatchesRequest
        {
            ProfileId = context.Message.ProfileId.ToString()
        };

        var response = await profileProtoService.GetPotentialMatchesAsync(request);

        var potentialMatches = response.ProfileIds.Select(Guid.Parse).ToList();

        foreach (var match in potentialMatches)
        {
            await sender.Send(new CreateMatchesCommand(context.Message.ProfileId, match));
        }
    }
}