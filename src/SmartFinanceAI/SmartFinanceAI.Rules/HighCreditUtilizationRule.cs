﻿using NRules.Fluent.Dsl;
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
