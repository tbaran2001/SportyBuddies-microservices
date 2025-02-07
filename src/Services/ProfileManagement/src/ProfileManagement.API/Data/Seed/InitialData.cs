using ProfileManagement.API.Profiles.Enums;
using ProfileManagement.API.Profiles.Models;
using ProfileManagement.API.Profiles.ValueObjects;

namespace ProfileManagement.API.Data.Seed;

internal class InitialData
{
    public static IEnumerable<Sport> Sports => new List<Sport>
    {
        Sport.Create(new Guid("c256f0e3-be38-4502-89af-f26ac6553aeb")),
        Sport.Create(new Guid("8104248e-4c99-49f3-9ca3-4f15f6993ae6")),
        Sport.Create(new Guid("e1dfb3ff-b817-4322-9f91-6af7efd337cc")),
    };

    public static IEnumerable<Profile> ProfilesWithSports
    {
        get
        {
            var p1 = Profile.Create(
                new Guid("8d69a725-c1b7-45eb-8ace-982bdc21ca78"),
                Name.Of("John"),
                Description.Of("Description"),
                new DateOnly(1990, 1, 1),
                Gender.Male,
                Preferences.Default);

            p1.AddSport(new Guid("c256f0e3-be38-4502-89af-f26ac6553aeb"));
            p1.AddSport(new Guid("8104248e-4c99-49f3-9ca3-4f15f6993ae6"));

            var p2 = Profile.Create(
                new Guid("f0d08409-8f34-4f88-aba4-cc7e906f7d62"),
                Name.Of("Alice"),
                Description.Of("Description"),
                new DateOnly(1990, 1, 1),
                Gender.Female,
                Preferences.Default);

            p2.AddSport(new Guid("e1dfb3ff-b817-4322-9f91-6af7efd337cc"));
            p2.AddSport(new Guid("8104248e-4c99-49f3-9ca3-4f15f6993ae6"));

            return new List<Profile> { p1, p2 };
        }
    }
}