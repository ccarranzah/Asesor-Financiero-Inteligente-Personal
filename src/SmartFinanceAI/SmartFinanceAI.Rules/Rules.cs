using NRules.Fluent.Dsl;

using SmartFinanceAI.Domain;
using SmartFinanceAI.Domain.Entities;
using SmartFinanceAI.Domain.Enums;

namespace SmartFinanceAI.Rules;

public class HighCreditUtilizationRule : Rule
{
    public override void Define()
    {
        FinancialAdvisor plan = default!;
        CreditCard creditCard = default!;

        When()
            .Match(() => plan)
            .Match(() => creditCard,
                c => plan.User.CreditCards.Contains(c),
                c => c.CreditLimit > 0,
                c => (c.CurrentBalance / c.CreditLimit) >= 0.70m);

        Then()
            .Do(ctx => plan.AddAdvice(
                $"{NotificationType.General} - High credit utilization detected. Balance: {creditCard.CurrentBalance:C}, Limit: {creditCard.CreditLimit:C}"))
            .Do(ctx => plan.Penalize(5));
    }
}

public class MultipleLoansRule : Rule
{
    public override void Define()
    {
        FinancialAdvisor plan = default!;
        int loanCount = 0;

        When()
            .Match(() => plan)
            .Let(() => loanCount, () => plan.User.Loans.Count)
            .Having(() => loanCount > 1);

        Then()
            .Do(ctx => plan.AddAdvice(
                $"{NotificationType.General} - User has {loanCount} loans. Suggest consolidation or targeted payoff strategy."))
            .Do(ctx => plan.Penalize(3));
    }
}

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


public class FiftyTwentyThirtyBudgetRule : Rule
{
    public override void Define()
    {
        FinancialAdvisor plan = default!;
        var now = DateTime.UtcNow;

        When()
            // Only apply if the user has a positive monthly income
            .Match(() => plan, p => p.User.GetTotalInflowByPeriod(now.Year, now.Month) > 0);

        Then()
            .Do(ctx => CheckBudget(plan));
    }

    private void CheckBudget(FinancialAdvisor plan)
    {
        var income = plan.User.GetTotalInflowByPeriod(DateTime.UtcNow.Year, DateTime.UtcNow.Month);
        var needsRatio = plan.User.GetTotalOutflowNeedByPeriod(DateTime.UtcNow.Year, DateTime.UtcNow.Month) / income;
        var wantsRatio = plan.User.GetTotalOutflowWantsByPeriod(DateTime.UtcNow.Year, DateTime.UtcNow.Month) / income;
        var savingsRatio = plan.User.GetTotalOutflowSavingsByPeriod(DateTime.UtcNow.Year, DateTime.UtcNow.Month) / income;

        // Check "Needs" (should be <= 50%)
        if (needsRatio > 0.50m)
        {
            plan.AddAdvice($"Needs exceed 50% of income. Current: {needsRatio:P1}");
            plan.Penalize(5);
        }

        // Check "Savings" (should be >= 20%)
        if (savingsRatio < 0.20m)
        {
            plan.AddAdvice($"Savings below 20% of income. Current: {savingsRatio:P1}");
            plan.Penalize(5);
        }

        // Check "Wants" (should be <= 30%)
        if (wantsRatio > 0.30m)
        {
            plan.AddAdvice($"Wants exceed 30% of income. Current: {wantsRatio:P1}");
            plan.Penalize(5);
        }
    }
}