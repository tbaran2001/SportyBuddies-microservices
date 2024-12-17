using Marten.Schema;

namespace Sport.API.Data;

public class SportInitialData:IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        var session = store.LightweightSession();

        if(await session.Query<Models.Sport>().AnyAsync(token: cancellation))
            return;

        session.Store<Models.Sport>(GetInitialSports());
        await session.SaveChangesAsync(cancellation);
    }

    private IEnumerable<Models.Sport> GetInitialSports()=> new List<Models.Sport>
    {
        new()
        {
            Id = new Guid("c256f0e3-be38-4502-89af-f26ac6553aeb"),
            Name = "Football",
            Description = "Football description"
        },
        new()
        {
            Id = new Guid("8104248e-4c99-49f3-9ca3-4f15f6993ae6"),
            Name = "Basketball",
            Description = "Basketball description"
        },
        new()
        {
            Id = new Guid("e1dfb3ff-b817-4322-9f91-6af7efd337cc"),
            Name = "Tennis",
            Description = "Tennis description"
        }
    };
}