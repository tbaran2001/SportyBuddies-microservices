namespace Sport.API.Data;

public class SportInitialData
{
    public static IEnumerable<Sports.Models.Sport> GetInitialSports() => new List<Sports.Models.Sport>
    {
        Sports.Models.Sport.CreateWithId(
            new Guid("c256f0e3-be38-4502-89af-f26ac6553aeb"), "Football", "Football description"),
        Sports.Models.Sport.CreateWithId(
            new Guid("8104248e-4c99-49f3-9ca3-4f15f6993ae6"), "Basketball", "Basketball description"),
        Sports.Models.Sport.CreateWithId(
            new Guid("e1dfb3ff-b817-4322-9f91-6af7efd337cc"), "Tennis", "Tennis description")
    };
}