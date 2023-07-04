namespace backend.DTO;

public class UserSignup
{
    public required string Nome { get; set;}
    public required string Senha { get; set;}
    public required string Email { get; set;}
    public DateTime DataNascimento { get; set;}
    public string FotoNome { get; set; }
    public IFormFile Foto {get; set;}
    public string Jwt { get; set; }
}