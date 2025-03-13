using AutoMapper;
using SmartFinanceAI.DataAccess.Interfaces;
using SmartFinanceAI.DataAccess.Maps;
using System.Text.Json;

namespace SmartFinanceAI.DataAccess;

public class JsonDataAccess<TDomain, TDataAccess> : IDataAccess<TDomain>
    where TDomain : class
    where TDataAccess : class
{
    private readonly string _filePath;
    private List<TDataAccess> _data;
    private readonly IMapper _mapper;

    public JsonDataAccess(string filePath)
    {
        _filePath = filePath;
        _data = LoadData().Result;
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
    }

    private async Task<List<TDataAccess>> LoadData()
    {
        if (!File.Exists(_filePath)) return new List<TDataAccess>();

        string json = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<List<TDataAccess>>(json) ?? [];
    }

    private async Task SaveData()
    {
        string json = JsonSerializer.Serialize(_data, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_filePath, json);
    }

    public Task<IEnumerable<TDomain>> GetAllAsync()
    {
        var list = _data.ConvertAll(d => _mapper.Map<TDomain>(d)) ?? [];
        return Task.FromResult<IEnumerable<TDomain>>(list);
    }

    public Task<TDomain?> GetByIdAsync(params object?[]? keyValues)
    {
        if (keyValues == null || keyValues.Length == 0 || keyValues[0] == null)
        {
            return Task.FromResult<TDomain?>(null);
        }

        var entity = _data.Find(x => x.GetType().GetProperty("Id")?.GetValue(x)?.Equals(keyValues[0]) == true);
        return Task.FromResult(entity == null ? null : _mapper.Map<TDomain>(entity));
    }

    public async Task AddOrUpdateAsync(TDomain domainEntity)
    {
        var dataAccessEntity = _mapper.Map<TDataAccess>(domainEntity);

        var existingEntity = _data.Find(x => x.GetType().GetProperty("Id")?.GetValue(x)?.Equals(domainEntity.GetType().GetProperty("Id")?.GetValue(domainEntity)) == true);
        if (existingEntity == null)
        {
            _data.Add(dataAccessEntity);
        }
        else
        {
            int index = _data.IndexOf(existingEntity);
            _data[index] = dataAccessEntity;
        }

        await SaveData();
    }

    public async Task DeleteAsync(params object?[]? keyValues)
    {
        if (keyValues == null || keyValues.Length == 0 || keyValues[0] == null)
        {
            return;
        }

        _data.RemoveAll(x => x.GetType().GetProperty("Id")?.GetValue(x)?.Equals(keyValues[0]) == true);
        await SaveData();
    }
}
