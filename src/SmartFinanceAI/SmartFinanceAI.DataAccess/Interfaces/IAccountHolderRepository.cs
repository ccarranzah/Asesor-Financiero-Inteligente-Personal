using SmartFinanceAI.Domain.Entities;

namespace SmartFinanceAI.DataAccess.Interfaces
{
    public interface IAccountHolderRepository : IRepository<AccountHolder>
    {
        Task<IEnumerable<AccountHolder>> GetAllAsync();
        Task<AccountHolder> GetByIdAsync(object id);
        Task AddAsync(AccountHolder entity);
        void Update(AccountHolder entity);
        void Delete(AccountHolder entity);
        Task SaveAsync();
    }
}
