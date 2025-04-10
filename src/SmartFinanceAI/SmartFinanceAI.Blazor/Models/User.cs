namespace SmartFinanceAI.Blazor.Models;

public class AppUser
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public short Age { get; set; }
    public RiskProfile RiskProfile { get; set; } = RiskProfile.Conservative;
    public List<CreditCard>? CreditCards { get; set; } // Navigation property
    public List<Loan>? Loans { get; set; } // Navigation property
    public List<Account>? Accounts { get; set; } // Navigation property
    public List<Transaction>? Transactions { get; set; } // Navigation property

    // Helper Methods, these should not be in the model
    public IEnumerable<Transaction> GetTransactionsByPeriod(int year, int month)
    {

        return Transactions?.Where(t => t.Date.Year == year && t.Date.Month == month).OrderBy(t => t.Date) ?? Enumerable.Empty<Transaction>();
    }

    public decimal GetTotalInflowByPeriod(int year, int month)
    {
        return GetTransactionsByPeriod(year, month).Where(t => t.TransactionType == TransactionType.Income).Sum(t => t.Amount);
    }

    public decimal GetTotalOutflowByPeriod(int year, int month)
    {
        return GetTransactionsByPeriod(year, month).Where(t => t.TransactionType == TransactionType.Expense).Sum(t => t.Amount);
    }

    public decimal GetTotalOutflowNeedByPeriod(int year, int month)
    {
        return GetTransactionsByPeriod(year, month).Where(t => t.TransactionType == TransactionType.Expense && t.TransactionCategoryType == TransactionCategoryType.Needs).Sum(t => t.Amount);
    }

    public decimal GetTotalOutflowWantsByPeriod(int year, int month)
    {
        return GetTransactionsByPeriod(year, month).Where(t => t.TransactionType == TransactionType.Expense && t.TransactionCategoryType == TransactionCategoryType.Wants).Sum(t => t.Amount);
    }

    public decimal GetSuperaavitByPeriod(int year, int month)
    {
        return GetTotalInflowByPeriod(year, month) - GetTotalOutflowByPeriod(year, month);
    }

    public decimal GetSuperaavitLastPeriod()
    {
        var now = DateTime.UtcNow;

        return GetTotalInflowByPeriod(now.Year, now.Month) - GetTotalOutflowByPeriod(now.Year, now.Month);
    }

    public decimal GetSavingsBalance()
    {
        return Accounts?.Where(a => a.AccountType == AccountType.Savings).Sum(s => s.Balance) ?? 0;
    }
}
