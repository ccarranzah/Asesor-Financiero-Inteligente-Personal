using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartFinanceAI.Blazor.Models;

public class Transaction
{
    public int Id { get; set; }
    public int UserId { get; set; } = 1;
    public int AccountId { get; set; }
    
    [DataType(DataType.Date)]
    public DateTimeOffset Date { get; set; }
    public TransactionType TransactionType { get; set; }
    public TransactionCategoryType TransactionCategoryType { get; set; }
    public string? Description { get; set; }

    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }

    public AppUser? User { get; set; } // Navigation property
    public Account? Account { get; set; } // Navigation property
}
