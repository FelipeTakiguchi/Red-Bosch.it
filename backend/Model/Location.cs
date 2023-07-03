using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class Location
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public int? Photo { get; set; }

    public virtual ICollection<Forum> Forums { get; set; } = new List<Forum>();

    public virtual ImageDatum PhotoNavigation { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
