namespace backend.Repositories;
using backend.Model;

public interface ILocationRepository : IRepository<ImageDatum>
{
    Task<int> GetLastIndex();
}