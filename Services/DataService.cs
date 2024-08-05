using Microsoft.EntityFrameworkCore;
using RTS.EntityFramework;
using RTS.Models;
using RTS.Services.Interfaces;

namespace RTS.Services;

public class DataService<T> : IDataService<T> where T : BaseEntity
{
    private readonly RecruitmentDbContextFactory _recruitmentDbContextFactory;

    public DataService(RecruitmentDbContextFactory recruitmentDbContextFactory)
    {
        _recruitmentDbContextFactory = recruitmentDbContextFactory;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        using (RecruitmentDbContext context = _recruitmentDbContextFactory.CreateDbContext())
        {
            IEnumerable<T> entities = await context.Set<T>().ToListAsync();
            return entities;
        }
    }

    public async Task<T> GetById(int id)
    {
        using (RecruitmentDbContext context = _recruitmentDbContextFactory.CreateDbContext())
        {
            T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
            return entity;
        }
    }

    public async Task<T> Create(T entity)
    {
        using (RecruitmentDbContext context = _recruitmentDbContextFactory.CreateDbContext())
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }

    public async Task<T> Update(int id, T entity)
    {
        using (RecruitmentDbContext context = _recruitmentDbContextFactory.CreateDbContext())
        {
            entity.Id = id;
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }

    public async Task<bool> Delete(int id)
    {
        using (RecruitmentDbContext context = _recruitmentDbContextFactory.CreateDbContext())
        {
            T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }
    }
}