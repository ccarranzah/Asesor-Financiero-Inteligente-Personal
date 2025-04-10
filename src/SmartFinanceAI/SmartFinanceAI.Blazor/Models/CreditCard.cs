using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartFinanceAI.Blazor.Models;

public class CreditCard
{
    public int Id { get; set; }
    public int UserId { get; set; } = 1;

    [Required]
    public string? CardNumber { get; set; }

    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal CreditLimit { get; set; }

    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal CurrentBalance { get; set; }
    public decimal AvailableCredit => CreditLimit - CurrentBalance; // Available credit amount

    [Column(TypeName = "decimal(2, 2)")]
    public decimal InterestRate { get; set; }

    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal MinimumPayment { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime NextDueDate { get; set; }

    public AppUser? User { get; set; } // Navigation property
}
