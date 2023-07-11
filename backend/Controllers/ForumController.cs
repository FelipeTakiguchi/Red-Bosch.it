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

    [HttpGet("/getAllUsersForum")]
    public async Task<ActionResult<List<UsuarioForumDTO>>> GetAllUsersForum(
                    [FromServices] IForumRepository forumRepository,
                    [FromServices] IUsuarioForumRepository usuarioForumRepository,
                    [FromServices] IJwtService jwtService,
                    string id
                    )
    {
        var query = await usuarioForumRepository.Filter(uf => true);

        List<UsuarioForumDTO> usuariosForumDTO = new List<UsuarioForumDTO>();

        foreach (var usuarioForum in query)
        {
            UsuarioForumDTO usuarioForumDTO = new()
            {
                Id = usuarioForum.Id,
                Usuario = jwtService.GetToken<UserJwt>(new UserJwt { UserID = usuarioForum.IdUsuario }),
                IdForum = usuarioForum.IdForum,
            };

            usuariosForumDTO.Add(usuarioForumDTO);
        }

        return usuariosForumDTO;
    }

    [HttpGet("/getUsersForum/{id}")]
    public async Task<ActionResult<List<UsuarioForumDTO>>> GetUsersForum(
                [FromServices] IForumRepository forumRepository,
                [FromServices] IUsuarioForumRepository usuarioForumRepository,
                [FromServices] IJwtService jwtService,
                string id
                )
    {
        var query = await usuarioForumRepository.Filter(uf => uf.IdForum == Convert.ToInt16(id));

        List<UsuarioForumDTO> usuariosForumDTO = new List<UsuarioForumDTO>();

        foreach (var usuarioForum in query)
        {
            UsuarioForumDTO usuarioForumDTO = new()
            {
                Id = usuarioForum.Id,
                Usuario = jwtService.GetToken<UserJwt>(new UserJwt { UserID = usuarioForum.IdUsuario }),
                IdForum = usuarioForum.IdForum,
            };

            usuariosForumDTO.Add(usuarioForumDTO);
        }

        return usuariosForumDTO;
    }

    [HttpPost("/exitForum")]
    public async Task<ActionResult> RemoveUserInForum(
            [FromServices] IForumRepository forumRepository,
            [FromServices] IUsuarioForumRepository usuarioForumRepository,
            [FromServices] IJwtService jwtService)
    {
        var result = jwtService.Validate<UserJwt>(Request.Form["idUsuario"]);
        var user = await usuarioForumRepository.Filter(uf => uf.IdUsuario == result.UserID && uf.IdForum == Convert.ToInt16(Request.Form["idForum"]));

        if (user.Any())
            await usuarioForumRepository.Delete(user[0]);

        var query = await forumRepository.Filter(f => f.Id == Convert.ToInt16(Request.Form["idForum"]));

        if (query.Any() && user.Any())
        {
            query[0].Inscritos -= 1;
            await forumRepository.Update(query[0]);
        }

        return Ok();
    }

    [HttpPost("/deleteForum")]
    public async Task<ActionResult> DeleteForum(
                [FromServices] IForumRepository forumRepository,
                [FromServices] IUsuarioForumRepository usuarioForumRepository,
                [FromServices] IJwtService jwtService,
                [FromServices] IPostRepository postRepository,
                [FromServices] IVoteRepository voteRepository,
                [FromServices] ICommentRepository commentRepository
    )
    {
        var users = await usuarioForumRepository.Filter(uf => true);

        foreach (var usuario in users)
            await usuarioForumRepository.Delete(usuario);

        var posts = await postRepository.Filter(p => p.IdForum == Convert.ToInt16(Request.Form["idForum"]));

        foreach (var post in posts)
        {
            var votes = await voteRepository.Filter(v => v.IdPost == post.Id);
            var comments = await commentRepository.Filter(c => c.IdPost == post.Id);

            foreach (var comment in comments)
            {
                await commentRepository.Delete(comment);
            }

            foreach (var vote in votes)
            {
                await voteRepository.Delete(vote);
            }
        }

        foreach (var post in posts)
            await postRepository.Delete(post);

        var forum = await forumRepository.Filter(f => f.Id == Convert.ToInt16(Request.Form["idForum"]));

        if (forum.Any())
            await forumRepository.Delete(forum[0]);

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
                if (query1.Count > 0 && query2.Count > 0)
                    if (query1[i].IdUsuario == result.UserID)
                    {
                        var removes = query2.Where(x => x.Id == query1[i].IdForum).Select(x => x);

                        foreach (var val in removes)
                            foreach (var item in forumDTO.ToList())
                                if (item.Id == val.Id)
                                    forumDTO.Remove(item);
                    }

            return Ok(forumDTO.OrderByDescending(forumDTO => forumDTO.Id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("/getJoinedForums")]
    public async Task<ActionResult<List<Forum>>> GetJoinedForums(
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

            for (int i = 0; i < query1.Count; i++)
                if (query1.Count > 0 && query2.Count > 0)
                    if (query1[i].IdUsuario == result.UserID)
                    {
                        var myQuery = query2.Where(x => x.Id == query1[i].IdForum).Select(x => x);

                        foreach (var item in myQuery)
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
                    }

            return Ok(forumDTO.OrderByDescending(forumDTO => forumDTO.Id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<ForumOwnerDTO>> GetForum(
        [FromServices] IForumRepository forumRepository,
        [FromServices] IJwtService jwtService,
        string code)
    {
        try
        {
            var query = await forumRepository.Filter(f => f.Id == Convert.ToInt16(code));

            ForumOwnerDTO fo = new ForumOwnerDTO()
            {
                Titulo = query[0].Titulo,
                Descricao = query[0].Descricao,
                Inscritos = query[0].Inscritos,
                ImageId = query[0].ImageId,
                Usuario = jwtService.GetToken<UserJwt>(new UserJwt { UserID = query[0].IdUsuario }),
            };

            return fo;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
