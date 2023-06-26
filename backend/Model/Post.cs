using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class Post
{
    public int Id { get; set; }

    public string Conteudo { get; set; } = null!;

    public DateTime DataPublicacao { get; set; }

    public int IdUsuario { get; set; }

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
