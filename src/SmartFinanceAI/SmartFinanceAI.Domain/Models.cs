using System;

namespace SmartFinanceAI.Domain;
public enum RiskProfile
{
    Low,
    Medium,
    High
}

/// <summary>
/// Represents a system user with accounts, financial instruments, income, and budgeting data.
/// </summary>
public class User
{
    public required string Name { get; set; }
    public RiskProfile RiskProfile { get; set; } = RiskProfile.Medium;

    // Basic Accounts
    public Account PaycheckAccount { get; set; } = new();
    public Account SavingsAccount { get; set; } = new();
    public Account InvestmentAccount { get; set; } = new();

    // Credit Cards and Loans
    public List<CreditCard> CreditCards { get; set; } = new();
    public List<Loan> Loans { get; set; } = new();
    public decimal MonthlyIncome { get; set; }

    /// <summary>
    /// The portion of monthly expenses needed to cover essential costs (housing, utilities, groceries, etc.).
    /// </summary>
    public decimal MonthlyNeeds { get; set; }

    /// <summary>
    /// The portion of monthly expenses that are considered "wants" or discretionary (dining out, entertainment, etc.).
    /// </summary>
    public decimal MonthlyWants { get; set; }

    /// <summary>
    /// The amount the user actually saves each month (could be deposit to SavingsAccount, etc.).
    /// </summary>
    public decimal MonthlySavings { get; set; }
}

/// <summary>
/// A basic bank account.
/// </summary>
public class Account
{
    public decimal Balance { get; set; } = 0;
}

public class CreditCard
{
    public decimal Limit { get; set; }
    public decimal Balance { get; set; }
    public decimal MonthlyInterestRate { get; set; }
    public int CutDay { get; set; }
    public int DueDay { get; set; }
}

public class Loan
{
    public decimal Balance { get; set; }
    public decimal MonthlyInterestRate { get; set; }
    public int MonthsRemaining { get; set; }
    public int DueDay { get; set; }
}

/// <summary>
/// Similar to an insurance quote model, but for financial health scoring and advice.
/// </summary>
public class FinancialPlan
{
    public User User { get; }
    public decimal BaseScore { get; }
    public decimal FinalScore { get; private set; }

    // Collection of textual advice, warnings, or recommended actions
    public List<string> AdviceList { get; } = new();

    public FinancialPlan(User user, decimal baseScore = 100)
    {
        User = user;
        BaseScore = baseScore;
        FinalScore = baseScore;
    }

    public void Penalize(decimal penalty) => FinalScore -= penalty;
    public void Reward(decimal boost) => FinalScore += boost;
    public void AddAdvice(string advice) => AdviceList.Add(advice);
}