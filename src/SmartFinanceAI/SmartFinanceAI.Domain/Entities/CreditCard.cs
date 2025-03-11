namespace SmartFinanceAI.Domain.Entities;

public class CreditCard
{
    public required string CardNumber { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal CurrentBalance { get; set; }
    public decimal AvailableCredit => CreditLimit - CurrentBalance; // Available credit amount
    public decimal InterestRate { get; set; }
    public decimal MinimumPayment { get; set; }
    public DateTime NextDueDate { get; set; }
    public bool IsOverLimit => CurrentBalance > CreditLimit;
    public bool IsPaymentDue => DateTime.UtcNow > NextDueDate && CurrentBalance > 0;
}