namespace backend.Repositories;
using backend.Model;

public interface IVoteRepository : IRepository<Vote>
{
    Task<int> GetLastIndex();
}