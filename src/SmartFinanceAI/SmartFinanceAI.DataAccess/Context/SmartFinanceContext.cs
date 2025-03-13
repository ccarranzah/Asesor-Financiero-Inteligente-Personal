using Microsoft.EntityFrameworkCore;
using SmartFinanceAI.Domain.Entities;

namespace SmartFinanceAI.DataAccess.Context
{
    public class SmartFinanceContext : DbContext
    {
        public SmartFinanceContext(DbContextOptions<SmartFinanceContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<CreditCard> CreditCardAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
