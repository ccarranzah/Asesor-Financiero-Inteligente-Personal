// 1. Prepare a RuleRepository, loading from the assembly with our rule classes.
using NRules;
using NRules.Fluent;
using SmartFinanceAI.DataAccess;
using SmartFinanceAI.Domain;
using SmartFinanceAI.Domain.Entities;
using SmartFinanceAI.Rules;

var repository = new RuleRepository();
repository.Load(x => x.From(typeof(FiftyTwentyThirtyBudgetRule).Assembly));

// 2. Compile the rules into a SessionFactory
var factory = repository.Compile();

// 3. Create a rules session
var session = factory.CreateSession();

// 4. (Optional) Listen to which rules get fired
session.Events.RuleFiredEvent += (_, args) =>
    Console.WriteLine($"Fired rule: {args.Rule.Name}");

// 5. Construct an example user
string basePath = AppDomain.CurrentDomain.BaseDirectory;
string jsonFilePath = Path.Combine(basePath, "data", "fact.json");
var dataAccess = new JsonDataAccess<User, SmartFinanceAI.DataAccess.Models.User>(jsonFilePath);

// Ask if the user wants to use a specific example user
Console.Write("\r\nWould you like to use a specific sample user (if the first sample user is not used)? (y/n, default n): ");
string useExampleUserInput = Console.ReadLine() ?? "n";

User? user;
if (useExampleUserInput?.Trim().ToLower() == "y")
{
    // Use a specific example user
    Console.Write("Enter the user ID: ");
    string? userIdInput = Console.ReadLine();
    user = await dataAccess.GetByIdAsync(userIdInput);
    if (user == null)
    {
        Console.WriteLine($"User with ID '{userIdInput}' not found.");
        return;
    }
}
else
{
    // Use a default example user
    var users = await dataAccess.GetAllAsync() ?? [];
    user = users.FirstOrDefault();
    if (user == null)
    {
        Console.WriteLine("No users found in the data source.");
        return;
    }
    Console.WriteLine("Using the first example user from the data source.");
}

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