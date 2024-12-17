using ProfileManagement.Domain.Enums;
using ProfileManagement.Domain.Models;
using ProfileManagement.Domain.ValueObjects;

namespace ProfileManagement.Infrastructure.Extensions;

internal class InitialData
{
    public static IEnumerable<Sport> Sports => new List<Sport>
    {
        Sport.Create(new Guid("c256f0e3-be38-4502-89af-f26ac6553aeb"), "Football"),
        Sport.Create(new Guid("8104248e-4c99-49f3-9ca3-4f15f6993ae6"), "Basketball"),
        Sport.Create(new Guid("e1dfb3ff-b817-4322-9f91-6af7efd337cc"), "Tennis"),
    };

    public static IEnumerable<Profile> ProfilesWithSports
    {
        get
        {
            var p1 = Profile.Create(
                new Guid("8d69a725-c1b7-45eb-8ace-982bdc21ca78"),
                ProfileName.Create("John"),
                ProfileDescription.Create("Description"),
                DateTime.Now,
                new DateOnly(1990, 1, 1),
                Gender.Male,
                Preferences.Default);

            p1.AddSport(new Guid("c256f0e3-be38-4502-89af-f26ac6553aeb"));
            p1.AddSport(new Guid("8104248e-4c99-49f3-9ca3-4f15f6993ae6"));

            var p2 = Profile.Create(
                new Guid("f0d08409-8f34-4f88-aba4-cc7e906f7d62"),
                ProfileName.Create("Alice"),
                ProfileDescription.Create("Description"),
                DateTime.Now,
                new DateOnly(1990, 1, 1),
                Gender.Female,
                Preferences.Default);

            p2.AddSport(new Guid("e1dfb3ff-b817-4322-9f91-6af7efd337cc"));
            p2.AddSport(new Guid("8104248e-4c99-49f3-9ca3-4f15f6993ae6"));

            return new List<Profile> { p1, p2 };
        }
    }
}