namespace SmartFinanceAI.Blazor.Models;

public class FinancialAdvisor
{
    public AppUser User { get; }
    public decimal BaseScore { get; }
    public decimal FinalScore { get; private set; }
    public List<string> AdviceList { get; } = new();

    public List<string> RulesApplied { get; set; } = new();

    public FinancialAdvisor(AppUser user, decimal baseScore = 100)
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
        var creditCardsBalance = User.CreditCards?.Sum(c => c.CurrentBalance) ?? 0;
        var hasLoans = User.Loans?.Sum(l => l.PrincipalAmount) ?? 0;
        var actives = User.Accounts?.Sum(s => s.Balance) ?? 0;
        
        return actives - creditCardsBalance - hasLoans;
    }
}