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
        [FromServices] IUsuarioForumRepository usuarioForumRepository,
        [FromServices] IJwtService jwtService)
    {
        var files = Request.Form.Files;

        if (files.Count > 0)
        {
            var file = Request.Form.Files[0];
            using MemoryStream ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var data = ms.GetBuffer();

            var img = new ImageDatum
            {
                Photo = data
            };
            await repo.Add(img);
        }

        var result = jwtService.Validate<UserJwt>(Request.Form["idUsuario"]);

        Forum forum = new()
        {
            Titulo = Request.Form["titulo"],
            Descricao = Request.Form["descricao"],
            IdUsuario = result.UserID,
            Inscritos = 1,
        };

        if (files.Count > 0)
            forum.ImageId = await imgr.GetLastIndex();

        await forumRepository.Add(forum);

        UsuarioForum usuarioForum = new()
        {
            IdUsuario = result.UserID,
            IdForum = await forumRepository.GetLastIndex(),
        };

        await usuarioForumRepository.Add(usuarioForum);

        return Ok();
    }

    [HttpPost("/enterForum")]
    public async Task<ActionResult> RegisterUserInForum(
            [FromServices] IForumRepository forumRepository,
            [FromServices] IUsuarioForumRepository usuarioForumRepository,
            [FromServices] IJwtService jwtService)
    {
        var query = await forumRepository.Filter(f => f.Id == Convert.ToInt16(Request.Form["idForum"]));

        query[0].Inscritos += 1;

        await forumRepository.Update(query[0]);

        var result = jwtService.Validate<UserJwt>(Request.Form["idUsuario"]);

        UsuarioForum usuarioForum = new()
        {
            IdUsuario = result.UserID,
            IdForum = Convert.ToInt16(Request.Form["idForum"]),
        };

        await usuarioForumRepository.Add(usuarioForum);

        return Ok();
    }

    [HttpGet("/getSomeForums")]
    public async Task<ActionResult<List<Forum>>> GetSomeForums(
                [FromServices] IForumRepository forumRepository,
                [FromServices] IJwtService jwtService,
                [FromServices] IUsuarioForumRepository usuarioForumRepository)

    {
        try
        {
            var jwt = Request.Headers["id"];
            var result = jwtService.Validate<UserJwt>(jwt);

            var query1 = await usuarioForumRepository.Filter(u => true);
            var query2 = await forumRepository.Filter(f => true);

            List<ForumDTO> forumDTO = new();

            foreach (var item in query2)
            {
                ForumDTO forinho = new()
                {
                    Id = item.Id,
                    Titulo = item.Titulo,
                    Descricao = item.Descricao,
                    Inscritos = item.Inscritos,
                    ImageId = item.ImageId,
                    IdUsuario = item.IdUsuario
                };
                forumDTO.Add(forinho);
            }

            for (int i = 0; i < query1.Count; i++)
            {
                if (query1.Count > 0 && query2.Count > 0)
                {
                    if (query1[i].IdUsuario == result.UserID)
                    {
                        var removes = query2.Where(x => x.Id == query1[i].IdForum).Select(x => x);

                        List<ForumDTO> forumsToRemove = new();

                        foreach (var item in removes)
                        {
                            ForumDTO forinho = new()
                            {
                                Id = item.Id,
                                Titulo = item.Titulo,
                                Descricao = item.Descricao,
                                Inscritos = item.Inscritos,
                                ImageId = item.ImageId,
                                IdUsuario = item.IdUsuario
                            };
                            Console.WriteLine(forinho.Id);
                            Console.WriteLine(forinho.Titulo);
                            Console.WriteLine(forinho.Descricao);
                            Console.WriteLine(forinho.Inscritos);
                            Console.WriteLine(forinho.ImageId);
                            Console.WriteLine(forinho.IdUsuario);
                            forumsToRemove.Add(forinho);
                        }
                        // forumDTO.Join()
                    }
                }
            }

            foreach (var item in forumDTO)
                Console.WriteLine(item.Descricao);

            return Ok(forumDTO);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<Forum>> GetForum(
        [FromServices] IForumRepository forumRepository,
        string code)
    {
        try
        {
            var query = await forumRepository.Filter(f => f.Id == Convert.ToInt16(code));

            Console.WriteLine(query[0]);

            Forum f = new Forum()
            {
                Titulo = query[0].Titulo,
                Descricao = query[0].Descricao,
                Inscritos = query[0].Inscritos,
                ImageId = query[0].ImageId,
                IdUsuario = query[0].IdUsuario,
            };

            return f;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
