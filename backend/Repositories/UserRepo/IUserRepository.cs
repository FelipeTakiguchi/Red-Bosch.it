namespace backend.Repositories;
using backend.Model;

public interface IUserRepository : IRepository<Usuario>
{
    Task<bool> userNameExists(string username);
    Task<bool> emailExists(string email);
}