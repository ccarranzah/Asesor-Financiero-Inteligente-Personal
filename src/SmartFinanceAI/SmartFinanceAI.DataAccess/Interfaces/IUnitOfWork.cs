using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFinanceAI.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountHolderRepository AccountHolders { get; }
        Task<int> CompleteAsync();
    }
}
