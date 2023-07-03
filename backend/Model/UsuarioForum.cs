using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class UsuarioForum
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public int IdForum { get; set; }

    public virtual Forum IdForumNavigation { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; }
}
