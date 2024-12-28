using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Authentication;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser GetCurrentUser()
    {
        var user = httpContextAccessor.HttpContext?.User;
        if (user == null)
            throw new InvalidOperationException("User Context is not available");

        if (user.Identity == null || !user.Identity.IsAuthenticated)
            throw new Exception("User is not authenticated");

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var roles = user.FindAll(ClaimTypes.Role).Select(c => c.Value);

        return new CurrentUser(Guid.Parse(userId),roles);
    }
}