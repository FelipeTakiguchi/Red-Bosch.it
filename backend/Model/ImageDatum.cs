using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class ImageDatum
{
    public int Id { get; set; }

    public byte[] Photo { get; set; }

    public virtual ICollection<Forum> Forums { get; set; } = new List<Forum>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
