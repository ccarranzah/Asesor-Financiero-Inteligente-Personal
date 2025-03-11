using SmartFinanceAI.DataAccess.Context;
using SmartFinanceAI.DataAccess.EFRepositories;
using SmartFinanceAI.DataAccess.Interfaces;
using SmartFinanceAI.DataAccess.JsonRepositories;

namespace SmartFinanceAI.DataAccess.DataLoaders
{
    public class DataLoaderFactory
    {
        public static IRepository<T> GetRepository<T>(string source, SmartFinanceContext? dbContext = null) where T : class
        {
            return source switch
            {
                "Database" when dbContext != null => new AccountHolderRepository(dbContext) as IRepository<T>,
                "JSON" => new JsonAccountHolderRepository() as IRepository<T>,
                _ => throw new ArgumentException("Invalid data source specified.")
            };
        }
    }
}
