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
        [FromServices] IRepository<ImageDatum> repo,
        [FromServices] IPasswordHasher psh,
        [FromServices] IImageRepository imgr)
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
        Console.WriteLine(Request.Form["nome"]);
        Console.WriteLine(Request.Form["email"]);
        // var query = await userRep.Filter(u => u.Nome == Request.Form["nome"] || u.Email == Request.Form["email"]);

        // if (query.Count() > 0)
        //     return BadRequest();


        byte[] hashPassword;
        string salt;

        (hashPassword, salt) = psh.GetHashAndSalt(Request.Form["senha"]);

        Console.WriteLine(Request.Form["dataNascimento"]);

        Usuario u = new Usuario()
        {
            Nome = Request.Form["nome"],
            Email = Request.Form["email"],
            DataNascimento = DateTime.Parse(Request.Form["dataNascimento"]),
            Senha = hashPassword,
            Salt = salt,
        };

        u.ImageId = await imgr.GetLastIndex();

        await userRep.Add(u);

        return Ok();
    }

    [HttpPost("/validate")]
    public ActionResult<bool> ValidateJwt(
        [FromServices] IJwtService jwtService,
        [FromBody] string jwt
    )
    {
        try
        {
            var result = jwtService.Validate<UserJwt>(jwt);
            return Ok(true);
        }
        catch (Exception e)
        {
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

        if (nameList.Count > 0)
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
        [FromBody] JwtDTO jwt
    )
    {
        try
        {
            var result = jwtService.Validate<UserJwt>(jwt.Jwt);
            Console.WriteLine(result.UserID);
            var query = await userRepository.Filter(u => u.Id == result.UserID);

            Console.WriteLine(query[0]);
            Usuario u = new Usuario()
            {
                Nome = query[0].Nome,
                Email = query[0].Email,
                Descricao = query[0].Descricao,
                DataNascimento = query[0].DataNascimento,
                ImageId = query[0].ImageId
            };

            return u;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("/update")]
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
