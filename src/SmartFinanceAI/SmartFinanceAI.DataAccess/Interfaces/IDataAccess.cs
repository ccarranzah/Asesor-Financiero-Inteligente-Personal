namespace SmartFinanceAI.DataAccess.Interfaces;

public interface IDataAccess<TDomain> where TDomain : class
{
    Task<IEnumerable<TDomain>> GetAllAsync();
    Task<TDomain> GetByIdAsync(params object?[]? keyValues);
    Task AddOrUpdateAsync(TDomain entity);
    Task DeleteAsync(params object?[]? keyValues);
}
