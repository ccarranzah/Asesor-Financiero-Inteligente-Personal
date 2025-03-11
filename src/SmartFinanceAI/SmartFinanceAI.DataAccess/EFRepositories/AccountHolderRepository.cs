using SmartFinanceAI.DataAccess.Context;
using SmartFinanceAI.DataAccess.Interfaces;
using SmartFinanceAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFinanceAI.DataAccess.EFRepositories
{
    public class AccountHolderRepository : IRepository<AccountHolder>
    {
        private readonly SmartFinanceContext _context;

        public AccountHolderRepository(SmartFinanceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AccountHolder>> GetAllAsync()
        {
            return await _context.AccountHolders.ToListAsync();
        }

        public async Task<AccountHolder> GetByIdAsync(object id)
        {
            return await _context.AccountHolders.FindAsync(id);
        }

        public async Task AddAsync(AccountHolder entity)
        {
            await _context.AccountHolders.AddAsync(entity);
        }

        public void Update(AccountHolder entity)
        {
            _context.AccountHolders.Update(entity);
        }

        public void Delete(AccountHolder entity)
        {
            _context.AccountHolders.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
