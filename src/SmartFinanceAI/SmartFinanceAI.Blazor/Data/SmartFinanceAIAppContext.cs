using Microsoft.EntityFrameworkCore;
using SmartFinanceAI.Blazor.Models;

namespace SmartFinanceAI.Blazor.Data;

public class SmartFinanceAIAppContext : DbContext
{
    public SmartFinanceAIAppContext(DbContextOptions<SmartFinanceAIAppContext> options)
        : base(options)
    {
    }

    public DbSet<AppUser> AppUser { get; set; } = default!;
    public DbSet<Account> Account { get; set; } = default!;
    public DbSet<CreditCard> CreditCard { get; set; } = default!;
    public DbSet<Loan> Loan { get; set; } = default!;
    public DbSet<Transaction> Transaction { get; set; } = default!;
    public DbSet<FinancialAdvisorRule> FinancialAdvisorRule { get; set; } = default!;
}