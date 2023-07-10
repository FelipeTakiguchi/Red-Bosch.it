namespace backend.Repositories;
using backend.Model;

public interface ICommentRepository : IRepository<Comentario>
{
    Task<int> GetLastIndex();
}