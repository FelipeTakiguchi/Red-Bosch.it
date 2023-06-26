using System.Linq.Expressions;
public interface IRepository<T>
{
    Task<List<T>> Filter(Expression<Func<T, bool>> exp);
    void Add(T obj);
    void Delete(T obj);
    void Update(T obj);
}