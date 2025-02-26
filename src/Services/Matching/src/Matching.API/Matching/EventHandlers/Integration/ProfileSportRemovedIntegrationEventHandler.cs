﻿namespace Matching.API.Matching.EventHandlers.Integration;

public class ProfileSportRemovedIntegrationEventHandler(
    IMatchesRepository matchesRepository,
    IUnitOfWork unitOfWork,
    ProfileProtoService.ProfileProtoServiceClient profileProtoService)
    : IConsumer<ProfileSportRemovedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ProfileSportRemovedIntegrationEvent> context)
    {
        var request = new GetPotentialMatchesRequest
        {
            ProfileId = context.Message.ProfileId.ToString()
        };

        var response = await profileProtoService.GetPotentialMatchesAsync(request);

        var potentialMatches = response.ProfileIds.Select(Guid.Parse).ToList();

        await matchesRepository.RemoveMatchesAsync(Guid.Parse(request.ProfileId), potentialMatches);
        await unitOfWork.CommitChangesAsync();
    }
}