using SmartFinanceAI.Domain.Entities;

namespace SmartFinanceAI.Domain;

/// <summary>
/// Similar to an insurance quote model, but for financial health scoring and advice.
/// </summary>
public class FinancialAdvisor
{
    public User User { get; }
    public decimal BaseScore { get; }
    public decimal FinalScore { get; private set; }
    public List<string> AdviceList { get; } = new();

    public FinancialAdvisor(User user, decimal baseScore = 100)
    {
        User = user;
        BaseScore = baseScore;
        FinalScore = baseScore;
    }

    public void Penalize(decimal penalty) => FinalScore -= penalty;
    public void Reward(decimal boost) => FinalScore += boost;
    public void AddAdvice(string advice) => AdviceList.Add(advice);

    public decimal CalculateGeneralBalance()
    {
        var creditCardsBalance = User.CreditCards.Sum(c => c.CurrentBalance);
        var hasLoans = User.Loans.Sum(l => l.PrincipalAmount);
        var totalSavings = User.Savings.Sum(s => s.Balance);
        var flowBalance = User.GetSuperaavitLastPeriod();
        return totalSavings + flowBalance - creditCardsBalance - hasLoans;
    }
}