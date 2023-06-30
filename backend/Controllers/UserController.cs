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
        var emailList = await userRep.Filter(u => u.Email == loginData.Email);
        var nameList = await userRep.Filter(u => u.Nome == loginData.Email);

        if (emailList.Count() == 0 && nameList.Count() == 0)        
            return Ok(result);

        Usuario target;

        if(nameList.Count > 0)
            target = nameList.First();
        else
            target = emailList.First();
        
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

    [HttpPost("/getUser")]
    public async Task<ActionResult<Usuario>> getUser(
        [FromServices] IJwtService jwtService,
        [FromServices] IUserRepository userRepository,
        [FromBody] string jwt
    )
    {
        try {
            var result = jwtService.Validate<UserJwt>(jwt);
            var query = await userRepository.Filter(u => u.Id == result.UserID);
            
            Console.WriteLine(query[0]);
            Console.WriteLine(result);

            Usuario u = new Usuario()
            {
                Nome = query[0].Nome,
                Email = query[0].Email,
                Location = query[0].Location,
                DataNascimento = query[0].DataNascimento,
            };

            return u;
        } catch(Exception e){
            return BadRequest(e.Message);
        }
    }
}