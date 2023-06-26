using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class UsuarioCargo
{
    public int Id { get; set; }

    public int IdCargo { get; set; }

    public int IdUsuario { get; set; }

    public int IdForum { get; set; }

    public virtual Cargo IdCargoNavigation { get; set; } = null!;

    public virtual Forum IdForumNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
