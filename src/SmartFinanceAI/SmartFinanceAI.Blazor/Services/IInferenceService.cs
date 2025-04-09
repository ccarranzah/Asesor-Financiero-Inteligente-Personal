using System;

namespace SmartFinanceAI.Blazor.Services;

public interface IInferenceService
{
    NRules.ISession CreateSession();
}
