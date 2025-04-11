using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using NRules;
using NRules.Fluent;
using NRules.Fluent.Dsl;
using NRules.RuleModel;
using SmartFinanceAI.Blazor.Data;
using SmartFinanceAI.Blazor.Models;
using System.Reflection;

namespace SmartFinanceAI.Blazor.Services;

public class CodeRulesDbInferenceService : IInferenceService
{
    private readonly ILogger<CodeRulesDbInferenceService> _logger;
    private readonly IDbContextFactory<SmartFinanceAIAppContext> _dbContextFactory;

    private ISessionFactory? _sessionFactory;

    public CodeRulesDbInferenceService(
        ILogger<CodeRulesDbInferenceService> logger,
        IDbContextFactory<SmartFinanceAIAppContext> dbContextFactory)
    {
        _logger = logger;
        _dbContextFactory = dbContextFactory;
    }

    public async Task RefreshRulesAsync()
    {
        _logger.LogInformation("Refreshing rules.");
        _sessionFactory = await LoadAndCompileAsync();
        _logger.LogInformation("Rules refreshed successfully.");
    }

    public async Task<NRules.ISession> CreateSessionAsync ()
    {
        _logger.LogInformation("Creating a new session for rule execution.");
        
        if (_sessionFactory == null)
        {
            _sessionFactory = await LoadAndCompileAsync();
        }
        
        var session = _sessionFactory.CreateSession();

        return session;
    }

    private async Task<ISessionFactory> LoadAndCompileAsync()
    {
         _logger.LogInformation("Loading rules from assembly.");
        var repository = new RuleRepository();
        var assembly = await CompileRuleAssemblyAsync();
        repository.Load(x => x.From(assembly));

        _logger.LogInformation("Compiling rules into a session factory.");
        var sessionFactory = repository.Compile();

        return sessionFactory;
    }

    public async Task<Assembly> CompileRuleAssemblyAsync()
    {
        using var context = _dbContextFactory.CreateDbContext();
        var rules = await context.FinancialAdvisorRule.ToListAsync();

        var syntaxTrees = new List<SyntaxTree>();

        foreach (var rule in rules)
        {
            if (string.IsNullOrEmpty(rule.Definition))
            {
                _logger.LogWarning($"Rule {rule.Name} has no definition.");
                continue;
            }
            var code = rule.Definition;
            var syntaxTree = CSharpSyntaxTree.ParseText(code);
            syntaxTrees.Add(syntaxTree);
        }

        // 2. Setup references: you need references for .NET assemblies + your NRules assemblies
        var references = new List<MetadataReference>
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            // So far, only mscorlib or System.Private.CoreLib...
        };

        // Here is where you need more references:
        var coreDir = Path.GetDirectoryName(typeof(object).Assembly.Location);

        if (coreDir == null)
        {
            throw new DirectoryNotFoundException("Core directory not found.");
        }
        
        references.Add(MetadataReference.CreateFromFile(Path.Combine(coreDir, "System.Runtime.dll")));
        references.Add(MetadataReference.CreateFromFile(Path.Combine(coreDir, "netstandard.dll")));
        references.Add(MetadataReference.CreateFromFile(Path.Combine(coreDir, "System.Core.dll")));
        references.Add(MetadataReference.CreateFromFile(Path.Combine(coreDir, "System.Collections.dll")));
        references.Add(MetadataReference.CreateFromFile(Path.Combine(coreDir, "System.Linq.Expressions.dll")));

        references.Add(MetadataReference.CreateFromFile(typeof(Rule).Assembly.Location));
        references.Add(MetadataReference.CreateFromFile(typeof(Context).Assembly.Location));

        references.Add(MetadataReference.CreateFromFile(typeof(FinancialAdvisor).Assembly.Location));


        // 3. Create the compilation
        var compilation = CSharpCompilation.Create(
            assemblyName: $"DynamicRules_{Guid.NewGuid()}",  // unique name
            syntaxTrees: syntaxTrees,
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );

        // 4. Compile into an in-memory assembly
        using var ms = new MemoryStream();
        var emitResult = compilation.Emit(ms);
        if (!emitResult.Success)
        {
            // Retrieve compilation errors
            var errors = string.Join(Environment.NewLine,
                emitResult.Diagnostics.Where(diag => diag.Severity == DiagnosticSeverity.Error));
            throw new Exception($"Failed to compile rule: {errors}");
        }

        ms.Seek(0, System.IO.SeekOrigin.Begin);
        var assembly = Assembly.Load(ms.ToArray());

        return assembly;
    }
}
