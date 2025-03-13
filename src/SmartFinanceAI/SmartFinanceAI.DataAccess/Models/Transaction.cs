using SmartFinanceAI.Domain.Enums;

namespace SmartFinanceAI.DataAccess.Models;

public class Transaction
{
    public DateTimeOffset Date { get; set; }
    public TransactionType TransactionType { get; set; }
    public required TransactionCategoryType TransactionCategoryType { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
}
