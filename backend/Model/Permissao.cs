using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class Permissao
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string? Descricao { get; set; }

    public virtual ICollection<Cargo> Cargos { get; set; } = new List<Cargo>();
}
