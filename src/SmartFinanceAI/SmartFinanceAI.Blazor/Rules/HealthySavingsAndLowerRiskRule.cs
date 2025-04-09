using NRules.Fluent.Dsl;
using SmartFinanceAI.Blazor.Models;
using System;

namespace SmartFinanceAI.Blazor.Rules;

public class HealthySavingsAndLowerRiskRule : Rule
{
    public override void Define()
    {
        FinancialAdvisor plan = default!;

        When()
            .Match(() => plan,
                p => p.User.GetSavingsBalance() >= 5000,
                p => p.User.RiskProfile == RiskProfile.Conservative || p.User.RiskProfile == RiskProfile.Moderate);

        Then()
            .Do(ctx => plan.AddAdvice(
                $"Investment Opportunity - Good savings balance. Consider diversifying investments."))
            .Do(ctx => plan.Reward(10));
    }
}
