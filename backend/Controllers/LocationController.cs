using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using backend.DTO;
using backend.Model;
using backend.Repositories;

[ApiController]
[Route("location")]
[EnableCors("MainPolicy")]
public class LocationController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<LocationDTO>>> Get(
        [FromServices] IRepository<Location> repo,
        string search = ""
    )
    {
        var query = await repo.Filter(x => x.Nome.Contains(search));
        var locations = query
            .Select(l => new LocationDTO()
            {
                ImgPath = l.Photo?.ToString() ?? "",
                Name = l.Nome
            })
            .ToList();
        return Ok(locations);
    }

    [HttpPost]
    public async Task<ActionResult> Post(
        [FromBody] LocationDTO obj,
        [FromServices] IRepository<Location> repo
    )
    {
        Location newData = new Location();
        newData.Nome = obj.Name;
        newData.Photo = string.IsNullOrEmpty(obj.ImgPath) ?
            null : int.Parse(obj.ImgPath);
        
        await repo.Add(newData);
        return Ok();
    }
}
