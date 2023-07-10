namespace backend.DTO;

public partial class CommentDTO
{
    public int Id { get; set; }

    public string Conteudo { get; set; }

    public DateTime DataPublicacao { get; set; }

    public string Usuario { get; set; }

    public int IdPost { get; set; }
}
