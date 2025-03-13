using SmartFinanceAI.Domain;
using SmartFinanceAI.Domain.Enums;

namespace SmartFinanceAI.Rules;

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
                $"{NotificationType.Investment} - Good savings balance. Consider diversifying investments."))
            .Do(ctx => plan.Reward(10));
    }
}
