using Microsoft.EntityFrameworkCore;
using RTS.EntityFramework;
using RTS.Models;

namespace RTS.Services;

public class DataService<T> : IDataService<T> where T : BaseEntity
{
    // public event Action<T> EntityCreated;

    private readonly RecruitmentDbContextFactory _recruitmentDbContextFactory;

    public DataService(RecruitmentDbContextFactory recruitmentDbContextFactory)
    {
        _recruitmentDbContextFactory = recruitmentDbContextFactory;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        await using var context = _recruitmentDbContextFactory.CreateDbContext();
        IEnumerable<T> entities = await context.Set<T>().ToListAsync();
        return entities;
    }

    public async Task<IEnumerable<T>> GetAll(string includeProperties)
    {
        await using var context = _recruitmentDbContextFactory.CreateDbContext();
        IQueryable<T> query = context.Set<T>();

        foreach (var includeProperty in includeProperties.Split(new[] { ',' },
                     StringSplitOptions.RemoveEmptyEntries))
            query = query.Include(includeProperty.Trim());

        return await query.ToListAsync();
    }

    public async Task<T?> GetById(int id, Func<IQueryable<T>, IQueryable<T>>? include = null)
    {
        await using var context = _recruitmentDbContextFactory.CreateDbContext();
        IQueryable<T?> query = context.Set<T>();

        if (include != null) query = include(query!);

        return await query.FirstOrDefaultAsync(e => e != null && EF.Property<int>(e, "Id") == id);
    }

    public async Task<T> GetByIdWProps(int id, string includeProperties = "")
    {
        await using var context = _recruitmentDbContextFactory.CreateDbContext();
        IQueryable<T> query = context.Set<T>();

        foreach (var includeProperty in includeProperties.Split(new[] { ',' },
                     StringSplitOptions.RemoveEmptyEntries))
            query = query.Include(includeProperty.Trim());

        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<T> Create(T entity)
    {
        await using var context = _recruitmentDbContextFactory.CreateDbContext();
        await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();

        // EntityCreated?.Invoke(entity);
        return entity;
    }

    public async Task<T> Update(int id, T entity)
    {
        await using var context = _recruitmentDbContextFactory.CreateDbContext();
        entity.Id = id;
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> Delete(int id)
    {
        await using var context = _recruitmentDbContextFactory.CreateDbContext();
        var entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        if (entity != null) context.Set<T>().Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}