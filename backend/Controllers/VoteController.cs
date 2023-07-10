using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

using backend.Model;
using backend.Repositories;

using Microsoft.AspNetCore.Cors;
using Security.Jwt;

[ApiController]
[EnableCors("MainPolicy")]
public class VoteController : ControllerBase
{
    [HttpPost("/addVote")]
    public async Task<ActionResult> addVote(
        [FromServices] IVoteRepository voteRepository,
        [FromServices] IRepository<ImageDatum> repo,
        [FromServices] IImageRepository imgr,
        [FromServices] IPostRepository postRepository,
        [FromServices] IJwtService jwtService)
    {
        var result = jwtService.Validate<UserJwt>(Request.Form["idUsuario"]);

        var post = await voteRepository.Filter(p => p.IdPost == Convert.ToInt16(Request.Form["idPost"])
            && p.IdUsuario == result.UserID);

        var update = await postRepository.Filter(p => p.Id == Convert.ToInt16(Request.Form["idPost"]));

        if (post.Count > 0)
        {
            if (Request.Form["state"] == "true")
            {
                if (!post[0].State)
                {
                    update[0].Votes += 1;
                    await postRepository.Update(update[0]);
                }

                post[0].State = true;
                await voteRepository.Update(post[0]);
            }
            else
            {
                if (post[0].State)
                {
                    update[0].Votes -= 1;
                    await postRepository.Update(update[0]);
                }

                post[0].State = false;
                await voteRepository.Update(post[0]);
            }
        }
        else
        {
            Vote vote = new Vote()
            {
                State = Convert.ToBoolean(Request.Form["state"]),
                IdUsuario = result.UserID,
                IdPost = Convert.ToInt16(Request.Form["idPost"]),
            };

            if (Request.Form["state"] == "true")
            {
                update[0].Votes = 1;
                await postRepository.Update(update[0]);
            }

            await voteRepository.Add(vote);
        }

        return Ok();
    }

    [HttpGet("votes/{code}")]
    public async Task<ActionResult<List<VoteDTO>>> GetPosts(
        [FromServices] IVoteRepository voteRepository,
        [FromServices] IJwtService jwtService,
        string code
    )
    {
        try
        {
            var upVotes = await voteRepository.Filter(p => p.IdPost == Convert.ToInt16(code));
            List<VoteDTO> voteDTOs = new List<VoteDTO>();

            foreach (var vote in upVotes)
            {
                VoteDTO voteDTO = new VoteDTO
                {
                    Id = vote.Id,
                    IdPost = vote.IdPost,
                    IdUsuario = vote.IdUsuario.ToString(),
                    State = vote.State
                };

                voteDTOs.Add(voteDTO);
            }

            for (int i = 0; i < voteDTOs.Count; i++)
                voteDTOs[i].IdUsuario = jwtService.GetToken<UserJwt>(new UserJwt { UserID = Convert.ToInt16(voteDTOs[i].IdUsuario) });

            return voteDTOs;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}