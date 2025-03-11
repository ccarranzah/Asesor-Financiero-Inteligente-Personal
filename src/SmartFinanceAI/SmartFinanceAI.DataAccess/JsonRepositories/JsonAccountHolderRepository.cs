using Newtonsoft.Json;
using SmartFinanceAI.DataAccess.Interfaces;


namespace SmartFinanceAI.DataAccess.JsonRepositories
{
    public class JsonAccountHolderRepository : IRepository<AccountHolder>
    {
        private readonly string _filePath = "data/account_holders.json";

        public async Task<IEnumerable<AccountHolder>> GetAllAsync()
        {
            if (!File.Exists(_filePath)) return new List<AccountHolder>();
            var jsonData = await File.ReadAllTextAsync(_filePath);
            return JsonConvert.DeserializeObject<List<AccountHolder>>(jsonData);
        }

        public async Task<AccountHolder> GetByIdAsync(object id)
        {
            var accountHolders = await GetAllAsync();
            return accountHolders.FirstOrDefault(u => u.ID == id.ToString());
        }

        public async Task AddAsync(AccountHolder entity)
        {
            var accountHolders = await GetAllAsync();
            var updatedList = accountHolders.Append(entity).ToList();
            await File.WriteAllTextAsync(_filePath, JsonConvert.SerializeObject(updatedList, Formatting.Indented));
        }

        public void Update(AccountHolder entity)
        {
            throw new NotImplementedException("Updating JSON records not implemented.");
        }

        public void Delete(AccountHolder entity)
        {
            throw new NotImplementedException("Deleting JSON records not implemented.");
        }

        public async Task SaveAsync()
        {
            await Task.CompletedTask;
        }
    }
}
