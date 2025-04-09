using NRules.Fluent.Dsl;
using SmartFinanceAI.Blazor.Models;
using System;

namespace SmartFinanceAI.Blazor.Rules;

public class InvestmentRecommendationRule : Rule
{
    public override void Define()
    {
        FinancialAdvisor plan = default!;
        var now = DateTime.Now;

        When()
            .Match(() => plan,
                p => p.User.GetSuperaavitByPeriod(now.Year, now.Month) / p.User.GetTotalInflowByPeriod(now.Year, now.Month) >= 0.2m);

        Then()
            .Do(ctx => CheckInvestment(plan));
    }

    private void CheckInvestment(FinancialAdvisor plan)
    {
        var riskProfile = plan.User.RiskProfile;
        var investmentAmount = plan.User.GetSuperaavitByPeriod(DateTime.UtcNow.Year, DateTime.UtcNow.Month) * 0.6m;

        switch (riskProfile)
        {
            case RiskProfile.Conservative:
                plan.AddAdvice($"Investment Opportunity - We have analyzed your finances and found that you have a surplus of 20% of your income." +
                    $"\r\nSince your risk profile is {riskProfile}, we recommend allocating part of this surplus, for example, ${investmentAmount:f}, to a Fixed-Term Certificate Deposit Term with a 6-month term." +
                    $"\r\nThis option guarantees stability with a fixed interest rate and no risks.");
                plan.Penalize(5);
                break;
            case RiskProfile.Moderate:
                plan.AddAdvice($"Investment Opportunity - Your financial analysis shows a 20% surplus of your income." +
                    $"\r\nSince your risk profile is {riskProfile}, we suggest investing in a Moderate-Risk Investment Fund,for example ${investmentAmount:f}" +
                    $"\r\nThis type of fund combines \"Bonds\" and \"Stocks\" to balance security and returns, " +
                    $"offering better earnings than a Certificate Deposit Term");
                plan.Penalize(5);
                break;
            case RiskProfile.Aggressive:
                plan.AddAdvice($"Investment Opportunity - {plan.User.Name}, congratulations! You have a 20% surplus of your income." +
                    $"\r\nSince your risk profile is {riskProfile}, we recommend exploring investments in high-volatility Mutual Funds or ETFs" +
                    $"\r\nThese options can offer higher returns, although with a higher level of risk.");
                plan.Penalize(5);
                break;
        }
    }
}
