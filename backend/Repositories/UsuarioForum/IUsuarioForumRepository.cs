namespace backend.Repositories;
using backend.Model;

public interface IUsuarioForumRepository : IRepository<UsuarioForum>
{
    Task<int> GetLastIndex();
}