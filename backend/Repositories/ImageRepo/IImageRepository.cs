namespace backend.Repositories;
using backend.Model;

public interface IImageRepository : IRepository<ImageDatum>
{
    Task<int> GetLastIndex();
}