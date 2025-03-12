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

