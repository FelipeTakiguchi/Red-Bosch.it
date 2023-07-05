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
[Route("posts")]
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
            IdUsuario =  result.UserID,
            IdForum = Convert.ToInt16(Request.Form["idForum"]),
        };

        if (files.Count > 0)
            post.ImageId = await imgr.GetLastIndex();

        await postRepository.Add(post);

        return Ok();
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<List<Post>>> getPosts(
        [FromServices] IPostRepository postRepository,
        string code
    )
    {
        try
        {            
            return await postRepository.Filter(p => p.IdForum == Convert.ToInt16(code));
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

        usuario.ImageId = await imgr.GetLastIndex();

        await userRep.Update(usuario);

        return Ok();
    }

}
