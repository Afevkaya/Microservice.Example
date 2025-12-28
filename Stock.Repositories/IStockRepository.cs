using MongoDB.Driver;

namespace Stock.Repositories;

public interface IStockRepository
{
    Task<bool> FindAsync();
    Task<Entities.Stock?> GetStockAsync(Guid productId);
    Task<bool> AnyAsync(Guid productId, int count);
    Task CreateStockAsync(Entities.Stock stock);
    Task UpdateStockAsync(Guid productId, Entities.Stock stock);
}