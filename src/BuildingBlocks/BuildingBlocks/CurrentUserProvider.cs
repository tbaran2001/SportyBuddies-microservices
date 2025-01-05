using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace BuildingBlocks;

public interface ICurrentUserProvider
{
    long? GetCurrentUserId();
}

public class CurrentUserProvider(IHttpContextAccessor httpContextAccessor) : ICurrentUserProvider
{
    public long? GetCurrentUserId()
    {
        var nameIdentifier = httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        long.TryParse(nameIdentifier, out var userId);

        return userId;
    }
}