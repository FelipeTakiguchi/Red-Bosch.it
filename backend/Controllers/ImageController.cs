using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using backend.Model;
using backend.Repositories;

[ApiController]
[Route("img")]
[EnableCors("MainPolicy")]
public class ImageController : ControllerBase
{
    [HttpGet("{code}")]
    public async Task<ActionResult> Get(
        string code,
        [FromServices]IRepository<ImageDatum> repo
    )
    {
        if (int.TryParse(code, out int id))
        {
            var query = await repo.Filter(im => im.Id == id);
            var img = query.FirstOrDefault();
            
            if (img is null)
                return NotFound();

            return File(img.Photo, "image/jpeg");
        }

        return BadRequest("code needs to be a integer.");
    }

    [HttpPost]
    [DisableRequestSizeLimit]
    public async Task<ActionResult<string>> Post(
        [FromServices]IRepository<ImageDatum> repo
    )
    {
        var files = Request.Form.Files;
        if (files is null || files.Count == 0)
            return BadRequest();
        var file = Request.Form.Files[0];

        if (file.Length < 1)
            return BadRequest();

        using MemoryStream ms = new MemoryStream();
        await file.CopyToAsync(ms);
        var data = ms.GetBuffer();

        var img = new ImageDatum();
        img.Photo = data;
        await repo.Add(img);

        var code = img.Id.ToString();
        return Ok(code);
    }
}
