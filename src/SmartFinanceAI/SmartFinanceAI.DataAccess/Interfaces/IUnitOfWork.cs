namespace SmartFinanceAI.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountHolderRepository AccountHolders { get; }
        Task<int> CompleteAsync();
    }
}
