namespace SmartFinanceAI.Domain.Entities;

public class Account
{
    public required string AccountNumber { get; set; }
    public decimal Balance { get; set; }
}
