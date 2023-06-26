using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class Forum
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descricao { get; set; } = null!;

    public int Inscritos { get; set; }

    public int IdUsuario { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<UsuarioCargo> UsuarioCargos { get; set; } = new List<UsuarioCargo>();

    public virtual ICollection<UsuarioForum> UsuarioForums { get; set; } = new List<UsuarioForum>();
}
