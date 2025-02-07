using Microsoft.EntityFrameworkCore;
using ProfileManagement.API.Data;
using ProfileManagement.API.Profiles.Models;
using ProfileManagement.API.Profiles.ValueObjects;

namespace Unit.Test.Common;

public static class DbContextFactory
{
    public static ProfileDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ProfileDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ProfileDbContext(options);

        ProfileDataSeeder(context);

        return context;
    }

    private static void ProfileDataSeeder(ProfileDbContext context)
    {
        var profiles = new List<Profile>
        {
            Profile.CreateSimple(Guid.NewGuid(), Name.Of("John Doe"), Description.Of("Software Engineer")),
            Profile.CreateSimple(Guid.NewGuid(), Name.Of("Jane Doe"), Description.Of("Software Engineer")),
        };

        context.Profiles.AddRange(profiles);
        context.SaveChanges();
    }

    public static void Destroy(ProfileDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}