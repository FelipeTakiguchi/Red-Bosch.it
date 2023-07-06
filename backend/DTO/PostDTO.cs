using backend.Model;

public partial class PostDTO
{
    public int Id { get; set; }

    public int? ImageId { get; set; }

    public string Conteudo { get; set; }

    public DateTime DataPublicacao { get; set; }

    public int IdUsuario { get; set; }

    public string jwt { get; set; }
}