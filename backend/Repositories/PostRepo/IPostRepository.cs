namespace backend.Repositories;
using backend.Model;

public interface IPostRepository : IRepository<Post>
{
    Task<int> GetLastIndex();
}