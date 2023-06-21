using Microsoft.AspNetCore.Mvc;
namespace ProjetoWeb.Controllers;
[ApiController]
[Route("test")]
public class TestController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "Server is running...";
    }
}