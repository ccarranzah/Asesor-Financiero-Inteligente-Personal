namespace SmartFinanceAI.DataAccess.Models;

public class Loan
{
    public required string LoanAlias { get; set; }
    public decimal PrincipalAmount { get; set; }
    public decimal InterestRate { get; set; }
    public int TermMonths { get; set; }
    public int OutstandingMonths { get; set; }
    public decimal MonthlyPayment { get; set; }
    public decimal OutstandingBalance { get; set; }
    public DateTime StartDate { get; set; }
    public int DueDay { get; set; }
}
