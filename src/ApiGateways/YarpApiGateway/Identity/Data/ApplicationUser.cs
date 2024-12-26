using Microsoft.AspNetCore.Identity;

namespace YarpApiGateway.Identity.Data;

public class ApplicationUser : IdentityUser<Guid>
{
    public string Name { get; set; }
    public DateOnly DateOfBirth { get; set; }
}