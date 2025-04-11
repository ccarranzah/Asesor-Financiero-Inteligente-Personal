namespace SmartFinanceAI.Blazor.Models;

public class FinancialAdvisor
{
    public AppUser AppUser { get; }
    public decimal BaseScore { get; }
    public decimal FinalScore { get; private set; }
    public List<string> AdviceList { get; } = new();

    public List<string> RulesApplied { get; set; } = new();

    public FinancialAdvisor(AppUser appUser, decimal baseScore = 100)
    {
        AppUser = appUser;
        BaseScore = baseScore;
        FinalScore = baseScore;
    }

    public void Penalize(decimal penalty) => FinalScore -= penalty;
    public void Reward(decimal boost) => FinalScore += boost;
    public void AddAdvice(string advice) => AdviceList.Add(advice);

    public decimal CalculateGeneralBalance()
    {
        var creditCardsBalance = AppUser.CreditCards?.Sum(c => c.CurrentBalance) ?? 0;
        var hasLoans = AppUser.Loans?.Sum(l => l.PrincipalAmount) ?? 0;
        var actives = AppUser.Accounts?.Sum(s => s.Balance) ?? 0;
        
        return actives - creditCardsBalance - hasLoans;
    }
}