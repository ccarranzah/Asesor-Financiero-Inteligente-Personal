using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartFinanceAI.Blazor.Models;

public class  Account
{
    public int Id { get; set; }
    public int UserId { get; set; } = 1;
    public AccountType AccountType { get; set; } = AccountType.Savings;
    public string? AccountAlias { get; set; }
    public string? AccountNumber { get; set; }

    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Balance { get; set; }

    public AppUser? User { get; set; } // Navigation property
    public List<Transaction>? Transactions { get; set; } // Navigation property
    
}

public enum AccountType
{
    Savings,
    Investment
}


