using Buddies.API.Buddies.Models;
using Buddies.API.Data;
using Buddies.Grpc;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Buddies.API.GrpcServer.Services;

public class BuddiesService(BuddyDbContext dbContext) : BuddiesProtoService.BuddiesProtoServiceBase
{
    public override async Task<GetProfileBuddiesResponse> GetProfileBuddies(GetProfileBuddiesRequest request,
        ServerCallContext context)
    {
        var profileId = Guid.Parse(request.ProfileId);

        var buddies = await dbContext.Buddies
            .Where(b => b.ProfileId == profileId)
            .ToListAsync();

        var buddyResponses = buddies.Adapt<List<BuddyResponse>>();

        var response = new GetProfileBuddiesResponse
        {
            Buddies = { buddyResponses }
        };

        return response;
    }

    public override async Task<CreateBuddiesResponse> CreateBuddies(CreateBuddiesRequest request,
        ServerCallContext context)
    {
        var (buddy1, buddy2) = Buddy.CreatePair(Guid.Parse(request.ProfileId), Guid.Parse(request.MatchedProfileId),
            DateTime.UtcNow);
        if (buddy1 is null || buddy2 is null)
            throw new RpcException(new Status(StatusCode.Internal, "Failed to create buddies"));

        await dbContext.Buddies.AddRangeAsync(buddy1, buddy2);
        await dbContext.SaveChangesAsync();

        var response = new CreateBuddiesResponse
        {
            Buddy1 = buddy1.Adapt<BuddyResponse>(),
            Buddy2 = buddy2.Adapt<BuddyResponse>()
        };

        return response;
    }
}