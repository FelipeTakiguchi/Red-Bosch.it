using System.Linq.Expressions;

namespace backend.Repositories;

public interface IRepository<T>
{
    Task<List<T>> Filter(Expression<Func<T, bool>> exp);
    Task Add(T obj);
    Task Delete(T obj);
    Task Update(T obj);
    Task Save();
}