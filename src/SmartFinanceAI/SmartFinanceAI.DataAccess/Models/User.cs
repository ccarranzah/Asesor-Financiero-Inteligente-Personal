using SmartFinanceAI.Domain.Enums;

namespace SmartFinanceAI.DataAccess.Models;

public class User
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public short Age { get; set; }
    public RiskProfile RiskProfile { get; set; }
    public List<CreditCard> CreditCards { get; set; } = [];
    public List<Loan> Loans { get; set; } = [];
    public List<Account> Savings { get; set; } = [];
    public List<Account> Investments { get; set; } = [];
    public List<Transaction> Transactions { get; set; } = [];
}
