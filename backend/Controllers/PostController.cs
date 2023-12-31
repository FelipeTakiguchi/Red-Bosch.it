using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

using backend.Model;
using backend.Repositories;
using backend.DTO;

using Microsoft.AspNetCore.Cors;
using backend.Services;
using Security.Jwt;

[ApiController]
[EnableCors("MainPolicy")]
public class PostController : ControllerBase
{
    [HttpPost("/addPost")]
    public async Task<ActionResult> addPost(
        [FromServices] IPostRepository postRepository,
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

        var result = jwtService.Validate<UserJwt>(Request.Form["idUsuario"]);

        Post post = new Post()
        {
            Conteudo = Request.Form["content"],
            DataPublicacao = DateTime.Parse(Request.Form["date"]),
            IdUsuario = result.UserID,
            Votes = 0,
            IdForum = Convert.ToInt16(Request.Form["idForum"]),
        };

        if (files.Count > 0)
            post.ImageId = await imgr.GetLastIndex();

        await postRepository.Add(post);

        return Ok();
    }

    [HttpGet("posts/{code}")]
    public async Task<ActionResult<List<PostDTO>>> GetPosts(
        [FromServices] IPostRepository postRepository,
        [FromServices] IJwtService jwtService,
        string code
    )
    {
        try
        {
            var post = await postRepository.Filter(p => p.IdForum == Convert.ToInt16(code));
            List<PostDTO> postDTOs = new();

            for (int i = 0; i < post.Count; i++)
            {
                PostDTO postDTO = new()
                {
                    Id = post[i].Id,
                    Conteudo = post[i].Conteudo,
                    ImageId = post[i].ImageId,
                    DataPublicacao = post[i].DataPublicacao,
                    IdUsuario = post[i].IdUsuario,
                    Votes = post[i].Votes,
                    IdForum = post[i].IdForum,
                    jwt = jwtService.GetToken<UserJwt>(new UserJwt { UserID = post[i].IdUsuario }),
                };
                postDTOs.Add(postDTO);
            }
            postDTOs = postDTOs.OrderByDescending(postDTO => postDTO.Id).ToList();
            return postDTOs.OrderByDescending(postDTO => postDTO.Votes).ToList();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("/deletePost")]
    public async Task<ActionResult<Post>> DeletePost(
        [FromServices] IPostRepository postRepository,
        [FromServices] IVoteRepository voteRepository,
        [FromServices] ICommentRepository commentRepository
    )
    {
        try
        {
            var post = await postRepository.Filter(p => p.Id == Convert.ToInt16(Request.Form["idPost"]));
            var votes = await voteRepository.Filter(v => v.IdPost == Convert.ToInt16(Request.Form["idPost"]));
            var comments = await commentRepository.Filter(c => c.IdPost == Convert.ToInt16(Request.Form["idPost"]));

            if (comments.Any())
                foreach (var comment in comments)
                {
                    await commentRepository.Delete(comment);
                }

            if (votes.Any())
                foreach (var vote in votes)
                {
                    await voteRepository.Delete(vote);
                }

            if (post.Any())
                await postRepository.Delete(post[0]);
            else
                return NotFound();

            return post[0];
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("/posts")]
    public async Task<ActionResult<List<PostDTO>>> GetAll(
        [FromServices] IPostRepository postRepository,
        [FromServices] IJwtService jwtService
    )
    {
        try
        {
            var post = await postRepository.Filter(p => true);
            List<PostDTO> postDTOs = new();

            for (int i = 0; i < post.Count; i++)
            {
                PostDTO postDTO = new()
                {
                    Id = post[i].Id,
                    Conteudo = post[i].Conteudo,
                    ImageId = post[i].ImageId,
                    DataPublicacao = post[i].DataPublicacao,
                    Votes = post[i].Votes,
                    IdUsuario = post[i].IdUsuario,
                    IdForum = post[i].IdForum,
                    jwt = jwtService.GetToken<UserJwt>(new UserJwt { UserID = post[i].IdUsuario }),
                };

                postDTOs.Add(postDTO);
            }

            postDTOs = postDTOs.OrderByDescending(postDTO => postDTO.Id).ToList();
            return postDTOs.OrderByDescending(postDTO => postDTO.Votes).ToList();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("/updatePost")]
    public async Task<ActionResult> Update(
        [FromServices] IUserRepository userRep,
        [FromServices] IRepository<ImageDatum> repo,
        [FromServices] IPasswordHasher psh,
        [FromServices] IImageRepository imgr,
        [FromServices] JwtService jwtService,
        [FromServices] UserRepository userRepository,
        [FromBody] UserSignup userSignup)
    {
        Usuario usuario;

        var token = jwtService.Validate<UserToken>(userSignup.Jwt);

        if (!token.Autenticado)
            throw new InvalidDataException();

        usuario = await userRepository.Find(token.IdUsuario);

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

        usuario.Nome = Request.Form["nome"];
        usuario.Descricao = Request.Form["descricao"];

        if (files.Count > 0)
            usuario.ImageId = await imgr.GetLastIndex();

        await userRep.Update(usuario);

        return Ok();
    }

}
