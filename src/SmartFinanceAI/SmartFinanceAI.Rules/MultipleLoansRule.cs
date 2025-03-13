using NRules.Fluent.Dsl;
using SmartFinanceAI.Domain;
using SmartFinanceAI.Domain.Enums;

namespace SmartFinanceAI.Rules;

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
