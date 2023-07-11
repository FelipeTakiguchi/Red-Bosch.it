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

        string nome = Request.Form["nome"];
        string email = Request.Form["email"];
        var query = await userRep.Filter(u => u.Nome == nome || u.Email == email);

        if (query.Count > 0)
            return BadRequest();

        byte[] hashPassword;
        string salt;

        (hashPassword, salt) = psh.GetHashAndSalt(Request.Form["senha"]);

        Usuario u = new Usuario()
        {
            Nome = Request.Form["nome"],
            Email = Request.Form["email"],
            Descricao = "Hey There! I'm a new Red Bosch.it User",
            DataNascimento = DateTime.Parse(Request.Form["dataNascimento"]),
            Senha = hashPassword,
            Salt = salt,
        };

        if (files.Count > 0)
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
        var result = new LoginResultDTO();
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
    public async Task<ActionResult<Usuario>> GetUser(
        [FromServices] IJwtService jwtService,
        [FromServices] IUserRepository userRepository,
        [FromBody] JwtDTO jwt
    )
    {
        try
        {
            var result = jwtService.Validate<UserJwt>(jwt.Jwt);
            var query = await userRepository.Filter(u => u.Id == result.UserID);

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

    [HttpGet("/getUserByName/{nome}")]
    public async Task<ActionResult<Usuario>> GetUserByName(
        [FromServices] IUserRepository userRepository,
        string nome
    )
    {
        try
        {
            var query = await userRepository.Filter(u => u.Nome == nome);

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
        [FromServices] IImageRepository imgr,
        [FromServices] IJwtService jwtService)
    {
        var jwt = Request.Form["idUser"];
        var result = jwtService.Validate<UserJwt>(jwt);
        var usuario = await userRep.Filter(u => u.Id == result.UserID);
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

        usuario[0].Nome = Request.Form["nome"];
        usuario[0].Descricao = Request.Form["descricao"];

        if (files.Count > 0)
            usuario[0].ImageId = await imgr.GetLastIndex();

        await userRep.Update(usuario[0]);

        return Ok();
    }

}
