using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class Forum
{
    public int Id { get; set; }

    public string Titulo { get; set; }

    public string Descricao { get; set; }

    public int Inscritos { get; set; }

    public int? Location { get; set; }

    public int IdUsuario { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; }

    public virtual Location LocationNavigation { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<UsuarioCargo> UsuarioCargos { get; set; } = new List<UsuarioCargo>();

    public virtual ICollection<UsuarioForum> UsuarioForums { get; set; } = new List<UsuarioForum>();
}
