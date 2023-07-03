using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class Comentario
{
    public int Id { get; set; }

    public string Conteudo { get; set; }

    public DateTime DataPublicacao { get; set; }

    public int IdUsuario { get; set; }

    public int IdPost { get; set; }

    public virtual Post IdPostNavigation { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; }
}
