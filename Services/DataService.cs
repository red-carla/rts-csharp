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
        await using RecruitmentDbContext context = _recruitmentDbContextFactory.CreateDbContext();
        IEnumerable<T> entities = await context.Set<T>().ToListAsync();
        return entities;
    }

    public async Task<T?> GetById(int id, Func<IQueryable<T>, IQueryable<T>>? include = null)
    {
        await using RecruitmentDbContext context = _recruitmentDbContextFactory.CreateDbContext();
        IQueryable<T?> query = context.Set<T>();

        if (include != null)
        {
            query = include(query!);
        }

        return await query.FirstOrDefaultAsync(e => e != null && EF.Property<int>(e, "Id") == id);
    }

    public async Task<T> Create(T entity)
    {
        await using RecruitmentDbContext context = _recruitmentDbContextFactory.CreateDbContext();
        await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();

        // EntityCreated?.Invoke(entity);
        return entity;
    }

    public async Task<T> Update(int id, T entity)
    {
        await using RecruitmentDbContext context = _recruitmentDbContextFactory.CreateDbContext();
        entity.Id = id;
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> Delete(int id)
    {
        await using RecruitmentDbContext context = _recruitmentDbContextFactory.CreateDbContext();
        T? entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
        if (entity != null) context.Set<T>().Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}