using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

using backend.Model;
using backend.Repositories;

using Microsoft.AspNetCore.Cors;
using backend.DTO;
using Security.Jwt;

[ApiController]
[EnableCors("MainPolicy")]
[Route("comments")]
public class CommentController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Comentario>>> GetAll(
        [FromServices] ICommentRepository commentRepository
    )
    {
        var query = await commentRepository.Filter(c => true);
        return query;
    }

    [HttpPost("/addComment")]
    public async Task<ActionResult> addComment(
        [FromServices] ICommentRepository commentRepository,
        [FromServices] IUsuarioForumRepository usuarioForumRepository,
        [FromServices] IJwtService jwtService)
    {
        var result = jwtService.Validate<UserJwt>(Request.Form["idUsuario"]);

        Comentario comentario = new()
        {
            Conteudo = Request.Form["conteudo"],
            DataPublicacao = DateTime.Parse(Request.Form["date"]),
            IdUsuario = result.UserID,
            IdPost = Convert.ToInt16(Request.Form["idPost"]),
        };

        await commentRepository.Add(comentario);

        return Ok();
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<List<CommentDTO>>> GetComments(
        [FromServices] ICommentRepository commentRepository,
        [FromServices] IJwtService jwtService,
        string code)
    {
        try
        {
            var query = await commentRepository.Filter(c => c.IdPost == Convert.ToInt16(code));

            List<CommentDTO> commentsDTO = new List<CommentDTO>();

            foreach (var comment in query)
            {
                CommentDTO commentDTO = new CommentDTO
                {
                    Id = comment.Id,
                    Conteudo = comment.Conteudo,
                    DataPublicacao = comment.DataPublicacao,
                    IdPost = comment.IdPost,
                };

                commentsDTO.Add(commentDTO);
            }

            for (int i = 0; i < commentsDTO.Count; i++)
                commentsDTO[i].Usuario = jwtService.GetToken<UserJwt>(new UserJwt { UserID = query[i].IdUsuario });

            return commentsDTO.OrderByDescending(c => c.Id).ToList();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("/deleteComment")]
    public async Task<ActionResult<Comentario>> DeleteComment(
        [FromServices] ICommentRepository commentRepository
    )
    {
        try
        {
            var comment = await commentRepository.Filter(c => c.Id == Convert.ToInt16(Request.Form["id"]));

            if (comment.Any())
            {
                await commentRepository.Delete(comment[0]);
                return comment[0];
            }

            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
