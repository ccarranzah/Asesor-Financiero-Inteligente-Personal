using NRules.Fluent.Dsl;
using SmartFinanceAI.Domain;

namespace SmartFinanceAI.Rules.RulesDefinitions
{
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
}
