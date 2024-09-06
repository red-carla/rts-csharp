namespace RTS.Services;

public interface IDataService<T>
{
    // event Action<T> EntityCreated;
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> GetAll(string includeProperties);
    Task<T?> GetById(int id, Func<IQueryable<T>, IQueryable<T>>? include = null);
    Task<T> GetByIdWProps(int id, string includeProperties);
    Task<T> Create(T entity);
    Task<T> Update(int id, T entity);
    Task<bool> Delete(int id);
    Task<T?> GetByEmail(string email);
    /*Task<IEnumerable<T>> GetByCandidateId(int candidateId);*/
    Task<IEnumerable<T>> GetByCondition(Func<T, bool> condition);
}