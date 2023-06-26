using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class Cargo
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public int IdPermissao { get; set; }

    public int IdUsuario { get; set; }

    public virtual Permissao IdPermissaoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<UsuarioCargo> UsuarioCargos { get; set; } = new List<UsuarioCargo>();
}
