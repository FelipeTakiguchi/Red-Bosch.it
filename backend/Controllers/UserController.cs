using Microsoft.AspNetCore.Mvc;

namespace Reddit.Controllers;

using backend.Model;
using backend.Repositories;
using backend.DTO;

using Microsoft.AspNetCore.Cors;
using backend.Services;
using Security.Jwt;

[ApiController]
[EnableCors("MainPolicy")]
[Route("users")]
public class UserController : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<List<Usuario>>> GetAll(
        [FromServices] IUserRepository userRepository
    )
    {
        var query = await userRepository.Filter(u => true);
        return query;
    }

    [HttpPost("/register")]
    public async Task<ActionResult> Register(
        [FromServices] IUserRepository userRep,
        [FromServices] IPasswordHasher psh,
        [FromBody] UserSignup userData)
    {

        var query = await userRep.Filter(u => u.Nome == userData.Nome || u.Email == userData.Email);

        if(query.Count() > 0)
            return BadRequest();
        
        byte[] hashPassword;
        string salt;

        (hashPassword, salt) = psh.GetHashAndSalt(userData.Senha);

        Usuario u = new Usuario()
        {
            Nome = userData.Nome,
            Email = userData.Email,
            Senha = hashPassword,
            Salt = salt,
            Location = null,
            DataNascimento = userData.DataNascimento,
        };

        await userRep.Add(u);

        return Ok();

    }

    [HttpPost("/validate")]
    public async Task<ActionResult<bool>> ValidateJwt(
        [FromServices] IJwtService jwtService,
        [FromBody] string jwt
    )
    {
        try {
            var result = jwtService.Validate<UserJwt>(jwt);
            return Ok(true);
        } catch(Exception e){
            return BadRequest(e.Message);
        }
    }


    [HttpPost("/login")]
    public async Task<ActionResult<bool>> Login(
        [FromBody] UserLogin loginData,
        [FromServices] IPasswordHasher psh,
        [FromServices] IUserRepository userRep,
        [FromServices] IJwtService jwtService
    )
    {   
        var result = new LoginResult();

        var userList = await userRep.Filter(u => u.Email == loginData.Email);

        result.UserExists = userList.Count() > 0;
        if (!result.UserExists)
        {
            return Ok(result);
        }
        
        Usuario target = userList.First();

        if (psh.Validate(loginData.Senha, target.Senha, target.Salt))
        {
            string token = jwtService.GetToken<UserJwt>(new UserJwt { UserID = target.Id });

            result.Jwt = token;
            result.Success = true;
            return Ok(result);
        }

        result.Success = false;
        return Ok(result);
    }
}