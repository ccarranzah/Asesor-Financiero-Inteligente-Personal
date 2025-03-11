using NRules.Fluent.Dsl;

using SmartFinanceAI.Domain;

namespace SmartFinanceAI.Rules;

public class LowSavingsRule : Rule
{
    public override void Define()
    {
        FinancialPlan plan = default!;

        When()
            .Match(() => plan,
                p => p.User.SavingsAccount.Balance < 1000);

        Then()
            .Do(ctx => plan.AddAdvice(
                $"Savings below recommended threshold. Current balance: {plan.User.SavingsAccount.Balance:C}"))
            .Do(ctx => plan.Penalize(10));
    }
}

public class HighCreditUtilizationRule : Rule
{
    public override void Define()
    {
        FinancialPlan plan = default!;
        CreditCard creditCard = default!;

        When()
            .Match(() => plan)
            .Match(() => creditCard,
                c => plan.User.CreditCards.Contains(c),
                c => c.Limit > 0,
                c => (c.Balance / c.Limit) >= 0.70m);

        Then()
            .Do(ctx => plan.AddAdvice(
                $"High credit utilization detected. Balance: {creditCard.Balance:C}, Limit: {creditCard.Limit:C}"))
            .Do(ctx => plan.Penalize(5));
    }
}

public class HighRiskProfileRule : Rule
{
    public override void Define()
    {
        FinancialPlan plan = default!;

        When()
            .Match(() => plan,
                p => p.User.RiskProfile == RiskProfile.High);

        Then()
            .Do(ctx => plan.AddAdvice(
                $"User is high risk. Advise caution with additional leverage or margin."))
            .Do(ctx => plan.Penalize(5));
    }
}

public class MultipleLoansRule : Rule
{
    public override void Define()
    {
        FinancialPlan plan = default!;
        int loanCount = 0;

        When()
            .Match(() => plan)
            .Let(() => loanCount, () => plan.User.Loans.Count)
            .Having(() => loanCount > 1);

        Then()
            .Do(ctx => plan.AddAdvice(
                $"User has {loanCount} loans. Suggest consolidation or targeted payoff strategy."))
            .Do(ctx => plan.Penalize(3));
    }
}

public class HealthySavingsAndLowerRiskRule : Rule
{
    public override void Define()
    {
        FinancialPlan plan = default!;

        When()
            .Match(() => plan,
                p => p.User.SavingsAccount.Balance >= 5000,
                p => p.User.RiskProfile == RiskProfile.Low || p.User.RiskProfile == RiskProfile.Medium);

        Then()
            .Do(ctx => plan.AddAdvice(
                "Good savings balance. Consider diversifying investments."))
            .Do(ctx => plan.Reward(10));
    }
}


public class FiftyTwentyThirtyBudgetRule : Rule
{
    public override void Define()
    {
        FinancialPlan plan = default!;

        When()
            // Only apply if the user has a positive monthly income
            .Match(() => plan, p => p.User.MonthlyIncome > 0);

        Then()
            .Do(ctx => CheckBudget(plan));
    }

    private void CheckBudget(FinancialPlan plan)
    {
        var income = plan.User.MonthlyIncome;
        var needsRatio = plan.User.MonthlyNeeds / income;
        var wantsRatio = plan.User.MonthlyWants / income;
        var savingsRatio = plan.User.MonthlySavings / income;

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