namespace RTS.Services;

public interface IDataService<T>
{
   // event Action<T> EntityCreated;
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(int id, Func<IQueryable<T>, IQueryable<T>>? include = null);
    Task<T> Create(T entity);
    Task<T> Update(int id, T entity);
    Task<bool> Delete(int id);
}