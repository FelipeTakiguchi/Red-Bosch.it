using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class Post
{
    public int Id { get; set; }

    public int? ImageId { get; set; }

    public string Conteudo { get; set; }

    public DateTime DataPublicacao { get; set; }

    public int IdUsuario { get; set; }

    public int IdForum { get; set; }

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual Forum IdForumNavigation { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; }

    public virtual ImageDatum Image { get; set; }

    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
