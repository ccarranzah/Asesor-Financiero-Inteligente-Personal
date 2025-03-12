// 1. Prepare a RuleRepository, loading from the assembly with our rule classes.
using NRules;
using NRules.Fluent;
using SmartFinanceAI.Domain;
using SmartFinanceAI.Domain.Entities;
using SmartFinanceAI.Domain.Enums;
using SmartFinanceAI.Rules;

var repository = new RuleRepository();
repository.Load(x => x.From(typeof(FiftyTwentyThirtyBudgetRule).Assembly));
repository.Load(x => x.From(typeof(InvestmentRecommendationRule).Assembly));

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
    RiskProfile = RiskProfile.Conservative,

    // Accounts
    Savings = [ 
        new() { AccountNumber = "Paycheck", Balance = 500m } 
    ],
    Investments = [ 
        new() { AccountNumber = "CDP", Balance = 4000m } 
    ],

    // Financial instruments
    CreditCards = [
                    new() { CardNumber = "1212", CreditLimit = 1000m, CurrentBalance = 850m },
                    new() { CardNumber = "5252", CreditLimit = 3000m, CurrentBalance = 1000m },
    ],
    Loans = [
        new() { LoanAlias = "Car", OutstandingBalance = 5000m, InterestRate = 0.01m, OutstandingMonths = 12, DueDay = 5 },
        new() { LoanAlias = "House", OutstandingBalance = 12000m, InterestRate = 0.02m, OutstandingMonths = 36, DueDay = 10 }
    ],

    Transactions = [
        new() { Amount = 4000m, TransactionType = TransactionType.Income, TransactionCategoryType = TransactionCategoryType.Income, Date = DateTime.UtcNow },
        new() { Amount = 2200m, TransactionType = TransactionType.Expense, TransactionCategoryType = TransactionCategoryType.Needs, Date = DateTime.UtcNow },
        new() { Amount = 1200m, TransactionType = TransactionType.Expense, TransactionCategoryType = TransactionCategoryType.Wants, Date = DateTime.UtcNow },
        new() { Amount = 600m, TransactionType = TransactionType.Expense, TransactionCategoryType = TransactionCategoryType.Savings, Date = DateTime.UtcNow },
    ],    
};

// 6. Create a FinancialPlan with a base score of 100
var plan = new FinancialAdvisor(user, baseScore: 100);

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