using NRules;
using NRules.Fluent;
using NRules.RuleModel;
using SmartFinanceAI.Blazor.Models;
using System;

namespace SmartFinanceAI.Blazor.Services;

public class CodeRulesInferenceService : IInferenceService
{
    private readonly ILogger<CodeRulesInferenceService> _logger;
    private readonly IRuleRepository _ruleRepository;
    private readonly ISessionFactory _sessionFactory;

    public CodeRulesInferenceService(ILogger<CodeRulesInferenceService> logger)
    {
        _logger = logger;
        _ruleRepository = LoadRules();
        _sessionFactory = Compile();
    }

    public NRules.ISession CreateSession()
    {
        _logger.LogInformation("Creating a new session for rule execution.");
        var session = _sessionFactory.CreateSession();
        
        return session;
    }

    private IRuleRepository LoadRules()
    {
        _logger.LogInformation("Loading rules from assembly.");
        var repository = new RuleRepository();
        repository.Load(x => x.From(typeof(FinancialAdvisor).Assembly));

        return repository;
    }

    private ISessionFactory Compile()
    {
        _logger.LogInformation("Compiling rules into a session factory.");
        var sessionFactory = _ruleRepository.Compile();

        return sessionFactory;
    }

}
