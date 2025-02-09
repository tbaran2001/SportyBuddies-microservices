namespace Sport.API.Data;

public static class SportInitialData
{
    public static IEnumerable<Sports.Models.Sport> GetInitialSports() => new List<Sports.Models.Sport>
    {
        Sports.Models.Sport.CreateWithId(
            SportId.Of(new Guid("c256f0e3-be38-4502-89af-f26ac6553aeb")), Name.Of("Football"),
            Description.Of("Football description")),

        Sports.Models.Sport.CreateWithId(
            SportId.Of(new Guid("8104248e-4c99-49f3-9ca3-4f15f6993ae6")), Name.Of("Basketball"),
            Description.Of("Basketball description")),

        Sports.Models.Sport.CreateWithId(
            SportId.Of(new Guid("e1dfb3ff-b817-4322-9f91-6af7efd337cc")), Name.Of("Tennis"),
            Description.Of("Tennis description")),

        Sports.Models.Sport.CreateWithId(
            SportId.Of(new Guid("f1b3f3b3-3b3b-3b3b-3b3b-3b3b3b3b3b3b")), Name.Of("Volleyball"),
            Description.Of("Volleyball description")),
    };
}