namespace ProfileManagement.API.Data;

public class ApplicationReadDbContext
{
    private readonly IMongoDatabase _database;
    public IMongoCollection<ProfileReadModel> Profiles => _database.GetCollection<ProfileReadModel>("Profiles");

    public ApplicationReadDbContext(IOptions<MongoOptions> options)
    {
        var client = new MongoClient(options.Value.ConnectionString);
        _database = client.GetDatabase(options.Value.DatabaseName);
    }
}