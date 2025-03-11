// 1. Prepare a RuleRepository, loading from the assembly with our rule classes.
using NRules;
using NRules.Fluent;
using SmartFinanceAI.Domain;
using SmartFinanceAI.Rules;

var repository = new RuleRepository();
repository.Load(x => x.From(typeof(LowSavingsRule).Assembly));

// 2. Compile the rules into a SessionFactory
var factory = repository.Compile();

// 3. Create a rules session
var session = factory.CreateSession();

// 4. (Optional) Listen to which rules get fired
session.Events.RuleFiredEvent += (_, args) =>
    Console.WriteLine($"Fired rule: {args.Rule.Name}");

// 5. Construct an example user
var user = new User
{
    Name = "John Doe",
    RiskProfile = RiskProfile.High,

    // Accounts
    PaycheckAccount = new Account { Balance = 2500m },
    SavingsAccount = new Account { Balance = 500m },
    InvestmentAccount = new Account { Balance = 4000m },

    // Financial instruments
    CreditCards = new List<CreditCard>
                {
                    new CreditCard { Limit = 1000m, Balance = 850m, MonthlyInterestRate = 0.02m, CutDay = 10, DueDay = 25 },
                    new CreditCard { Limit = 3000m, Balance = 1000m, MonthlyInterestRate = 0.015m, CutDay = 15, DueDay = 30 },
                },
    Loans = new List<Loan>
                {
                    new Loan { Balance = 5000m, MonthlyInterestRate = 0.01m, MonthsRemaining = 12, DueDay = 5 },
                    new Loan { Balance = 12000m, MonthlyInterestRate = 0.02m, MonthsRemaining = 36, DueDay = 10 }
                },

    // New monthly budgeting details
    MonthlyIncome = 4000m,
    MonthlyNeeds = 2200m,   // > 50% => 55% of income
    MonthlyWants = 1200m,  // 30% => OK
    MonthlySavings = 600m  // 15% => below recommended 20%
};

// 6. Create a FinancialPlan with a base score of 100
var plan = new FinancialPlan(user, baseScore: 100);

// 7. Insert the plan into the rules engine
session.Insert(plan);

// 8. Fire the rules
session.Fire();

// 9. Print results
Console.WriteLine("\n=== Final Output ===");
Console.WriteLine($"User: {user.Name}");
Console.WriteLine($"Base Score: {plan.BaseScore}");
Console.WriteLine($"Final Score: {plan.FinalScore}");
Console.WriteLine("Advice & Alerts:");
foreach (var tip in plan.AdviceList)
{
    Console.WriteLine($" - {tip}");
}

/*
 * Example output:
 * Fired rule: SmartFinanceAI.Rules.LowSavingsRule
 * Fired rule: SmartFinanceAI.Rules.HighCreditUtilizationRule
 * Fired rule: SmartFinanceAI.Rules.HighRiskProfileRule
 * Fired rule: SmartFinanceAI.Rules.MultipleLoansRule
 * Fired rule: SmartFinanceAI.Rules.FiftyTwentyThirtyBudgetRule
 *
 * === Final Output ===
 * User: Alice Johnson
 * Base Score: 100
 * Final Score: 72
 * Advice & Alerts:
 *  - Savings below recommended threshold. Current balance: $500.00
 *  - High credit utilization detected. Balance: $850.00, Limit: $1,000.00
 *  - User is high risk. Advise caution with additional leverage or margin.
 *  - User has 2 loans. Suggest consolidation or targeted payoff strategy.
 *  - Needs exceed 50% of income. Current: 55.0%
 *  - Savings below 20% of income. Current: 15.0%
 */