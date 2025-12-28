using MongoDB.Driver;

namespace Stock.Repositories;

public class StockRepository : IStockRepository
{
    private readonly MongoDbServices _mongoDbServices;

    public StockRepository(MongoDbServices mongoDbServices)
    {
        _mongoDbServices = mongoDbServices;
    }

    private Task<IMongoCollection<T>> GetCollectionAsync<T>()
    {
        return Task.FromResult(_mongoDbServices.Database.GetCollection<T>(typeof(T).Name.ToLowerInvariant()));
    }
    public async Task<bool> FindAsync()
    {
        var collection = await GetCollectionAsync<Entities.Stock>();
        var cursor = await collection.FindAsync(s => true);
        return await cursor.AnyAsync();
    }
    public async Task<Entities.Stock?> GetStockAsync(Guid productId)
    {
        var collection = await GetCollectionAsync<Entities.Stock>();
        var cursor = await collection.FindAsync(s => s.ProductId == productId);
        return await cursor.FirstOrDefaultAsync();
    }
    public async Task<bool> AnyAsync(Guid productId, int count)
    {
        var collection = await GetCollectionAsync<Entities.Stock>();
        var cursor = await collection.FindAsync(s => s.ProductId == productId && s.Count >= count);
        return await cursor.AnyAsync();
    }
    public async Task CreateStockAsync(Entities.Stock stock)
    {
        var collection = await GetCollectionAsync<Entities.Stock>();
        await collection.InsertOneAsync(stock);
    }
    
    public async Task UpdateStockAsync(Guid productId, Entities.Stock stock)
    {
        var collection = await GetCollectionAsync<Entities.Stock>();
        await collection.ReplaceOneAsync(s => s.ProductId == productId, stock);
    }
}