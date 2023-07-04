using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

using backend.Model;
using backend.Repositories;

using Microsoft.AspNetCore.Cors;
using backend.DTO;
using Security.Jwt;

[ApiController]
[EnableCors("MainPolicy")]
[Route("forums")]
public class ForumController : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<List<Forum>>> GetAll(
        [FromServices] IForumRepository ifr
    )
    {
        var query = await ifr.Filter(f => true);
        return query;
    }

    [HttpPost("/addForum")]
    public async Task<ActionResult> Register(
        [FromServices] IForumRepository forumRepository,
        [FromServices] IRepository<ImageDatum> repo,
        [FromServices] IImageRepository imgr,
        [FromServices] IJwtService jwtService)
    {
        var files = Request.Form.Files;

        if (files.Count > 0)
        {
            var file = Request.Form.Files[0];
            using MemoryStream ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var data = ms.GetBuffer();

            var img = new ImageDatum();
            img.Photo = data;
            await repo.Add(img);
        }
        Console.WriteLine(Request.Form["idUsuario"]);
        var result = jwtService.Validate<UserJwt>(Request.Form["idUsuario"]);

        Forum forum = new Forum()
        {
            Titulo = Request.Form["titulo"],
            Descricao = Request.Form["descricao"],
            IdUsuario = result.UserID,
        };

        forum.ImageId = await imgr.GetLastIndex();

        await forumRepository.Add(forum);

        return Ok();
    }
}
