namespace backend.DTO;

public class ForumDTO
{
    public int Id { get; set; }

    public string Titulo { get; set; }

    public string Descricao { get; set; }

    public int Inscritos { get; set; }

    public int? ImageId { get; set; }

    public int IdUsuario { get; set; }

}