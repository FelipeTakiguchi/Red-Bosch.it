using backend.Model;


public partial class VoteDTO
{
    public int Id { get; set; }

    public bool State { get; set; }

    public string IdUsuario { get; set; }

    public int IdPost { get; set; }
}