using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Stock.Repositories;

public class MongoDbServices
{
    public IMongoDatabase Database { get; }
    public MongoDbServices(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("MongoDbConnection"));
        Database = client.GetDatabase("StockDb");
    }
}