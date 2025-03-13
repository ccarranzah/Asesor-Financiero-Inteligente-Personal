using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartFinanceAI.DataAccess.Context;
using SmartFinanceAI.DataAccess.Interfaces;
using SmartFinanceAI.DataAccess.Maps;

namespace SmartFinanceAI.DataAccess;

public class EFCoreDataAccess<TDomain, TDataAccess> : IDataAccess<TDomain>
    where TDomain : class
    where TDataAccess : class
{
    private readonly SmartFinanceContext _context;
    private readonly DbSet<TDataAccess> _dbSet;
    private readonly IMapper _mapper;

    public EFCoreDataAccess(SmartFinanceContext context)
    {
        _context = context;
        _dbSet = _context.Set<TDataAccess>();
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
    }

    public async Task<IEnumerable<TDomain>> GetAllAsync()
    {
        return await _dbSet
            .Select(item => _mapper.Map<TDomain>(item))
            .ToListAsync();
    }

    public async Task<TDomain> GetByIdAsync(params object?[]? keyValues)
    {
        var entity = await _dbSet.FindAsync(keyValues);
        return entity == null ? null! : _mapper.Map<TDomain>(entity);
    }

    public async Task AddOrUpdateAsync(TDomain domainEntity)
    {
        var dataAccessEntity = _mapper.Map<TDataAccess>(domainEntity);

        var primaryKey = _context.Model.FindEntityType(typeof(TDataAccess))
                              ?.FindPrimaryKey()?.Properties
                              ?.Select(p => p.PropertyInfo?.GetValue(domainEntity))
                              .ToArray();

        if (primaryKey == null || primaryKey.Any(pk => pk == null))
            throw new InvalidOperationException("No se pudo determinar la clave primaria.");

        var existingEntity = await _dbSet.FindAsync(primaryKey);
        if (existingEntity == null)
        {
            await _dbSet.AddAsync(dataAccessEntity);
        }
        else
        {
            _context.Entry(existingEntity).CurrentValues.SetValues(dataAccessEntity);
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(params object?[]? keyValues)
    {
        var entity = await _dbSet.FindAsync(keyValues);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
