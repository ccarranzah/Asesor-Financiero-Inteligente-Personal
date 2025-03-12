using SmartFinanceAI.Domain.Enums;
namespace SmartFinanceAI.Domain.Entities;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Name { get; set; }
    public short Age { get; set; }
    public RiskProfile RiskProfile { get; set; } = RiskProfile.Conservative;
    public List<CreditCard> CreditCards { get; set; } = [];
    public List<Loan> Loans { get; set; } = [];
    public List<Account> Savings { get; set; } = [];
    public List<Account> Investments { get; set; } = [];
    public List<Transaction> Transactions { get; set; } = [];

    public IEnumerable<Transaction> GetTransactionsByPeriod(int year, int month)
    {
    
        return Transactions.Where(t => t.Date.Year == year && t.Date.Month == month).OrderBy(t => t.Date);
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

    public decimal GetTotalOutflowSavingsByPeriod(int year, int month)
    {
        return GetTransactionsByPeriod(year, month).Where(t => t.TransactionType == TransactionType.Expense && t.TransactionCategoryType == TransactionCategoryType.Savings).Sum(t => t.Amount);
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
        return Savings.Sum(s => s.Balance);
    }
}
