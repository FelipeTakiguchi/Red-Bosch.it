namespace backend.Repositories;
using backend.Model;

public interface IUsuarioCargoRepository : IRepository<UsuarioCargo>
{
    Task<int> GetLastIndex();
}