using NRules.Fluent.Dsl;
using SmartFinanceAI.Domain;
using SmartFinanceAI.Domain.Enums;

namespace SmartFinanceAI.Rules;

/// <summary>
/// Esta regla se basa en un balance general que contempla todo lo posible del usuario
/// como: balance de transacciones, total de préstamos, balance de tarjetas de crédito y
// balance de ahorros. De acuerdo a este balance se le da un consejo al usuario tomando en
// cuenta su perfil de riesgo.
/// </summary>
public class InvestmentRule : Rule
{
    public override void Define()
    {
        FinancialAdvisor plan = default!;

        When()
        // Para invertir la regla es no deber nada y tener ahorros.
            .Match(() => plan, p => p.CalculateGeneralBalance() > 0);

        Then()
            .Do(ctx => ManageInvestmentAdvices(plan));
    }

    private void ManageInvestmentAdvices(FinancialAdvisor plan)
    {
        var translation = new TranslationService();
        var riskProfile = plan.User.RiskProfile;
        var generalBalance = plan.CalculateGeneralBalance();

        if (generalBalance < 300000)
        {
            plan.AddAdvice(translation.Translate("InvestmentRule.LowBalance", "es"));
            plan.Penalize(5);
            return;
        }

        if (riskProfile == RiskProfile.Conservative)
        {
            plan.AddAdvice(translation.Translate("InvestmentRule.Conservative", "es"));
            plan.Reward(5);
        }
        else if (riskProfile == RiskProfile.Moderate)
        {
            plan.AddAdvice(translation.Translate("InvestmentRule.Moderate", "es"));
            plan.Reward(10);
        }
        else
        {
            plan.AddAdvice(translation.Translate("InvestmentRule.Aggressive", "es"));
            plan.Reward(15);
        }
    }
}