namespace RTS.Services.Interfaces;

public interface IDataService<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(int id, Func<IQueryable<T>, IQueryable<T>> include = null);
    Task<T> Create(T entity);
    Task<T> Update(int id, T entity);
    Task<bool> Delete(int id);
}