using System;

namespace SmartFinanceAI.Blazor.Services;

public interface IInferenceService
{
    Task RefreshRulesAsync();
    Task<NRules.ISession> CreateSessionAsync();
}
