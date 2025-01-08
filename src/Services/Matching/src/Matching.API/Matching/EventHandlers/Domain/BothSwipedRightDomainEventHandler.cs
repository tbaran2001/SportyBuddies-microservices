using Buddies.Grpc;
using Matching.API.Matching.Features.UpdateMatch;

namespace Matching.API.Matching.EventHandlers.Domain;

public class BothSwipedRightDomainEventHandler(BuddiesProtoService.BuddiesProtoServiceClient buddiesProtoService)
    : INotificationHandler<BothSwipedRightDomainEvent>
{
    public async Task Handle(BothSwipedRightDomainEvent notification, CancellationToken cancellationToken)
    {
        var request = new CreateBuddiesRequest
        {
            ProfileId = notification.ProfileId.ToString(),
            MatchedProfileId = notification.MatchedProfileId.ToString()
        };

        await buddiesProtoService.CreateBuddiesAsync(request, cancellationToken: cancellationToken);
    }
}