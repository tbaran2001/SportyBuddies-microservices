using YarpApiGateway.Identity.Data;

namespace YarpApiGateway.Identity;

public class IdentityEventsHandler(IdentityApplicationDbContext context)
{
    public async Task OnUserCreatedAsync(ApplicationUser user)
    {
        await context.SaveChangesAsync();
    }
}