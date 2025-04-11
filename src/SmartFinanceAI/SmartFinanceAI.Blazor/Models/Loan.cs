using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartFinanceAI.Blazor.Models;

public class Loan
{
    public int Id { get; set; }
    public int UserId { get; set; } = 1;

    [Required]
    public string? LoanAlias { get; set; }

    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal PrincipalAmount { get; set; }

    [Column(TypeName = "decimal(2, 2)")]
    public decimal InterestRate { get; set; }
    public int TermMonths { get; set; }
    public int OutstandingMonths { get; set; }

    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal MonthlyPayment { get; set; }

    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal OutstandingBalance { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }
    public int DueDay { get; set; }

    public AppUser? User { get; set; } // Navigation property
}
