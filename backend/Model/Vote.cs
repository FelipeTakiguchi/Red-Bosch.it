using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class Vote
{
    public int Id { get; set; }

    public bool State { get; set; }

    public int IdUsuario { get; set; }

    public int IdPost { get; set; }

    public virtual Post IdPostNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
