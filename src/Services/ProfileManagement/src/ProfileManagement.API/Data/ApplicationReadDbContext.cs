using BuildingBlocks.Mongo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ProfileManagement.API.Data;

public class ApplicationReadDbContext
{
    private readonly IMongoDatabase _database;
    
    public ApplicationReadDbContext(IOptions<MongoOptions> options)
    {
        var client = new MongoClient(options.Value.ConnectionString);
        _database = client.GetDatabase(options.Value.DatabaseName);
    }
}