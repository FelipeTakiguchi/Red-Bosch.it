namespace backend.Repositories;
using backend.Model;

public interface IForumRepository : IRepository<Forum>
{
    Task<int> GetLastIndex();
}