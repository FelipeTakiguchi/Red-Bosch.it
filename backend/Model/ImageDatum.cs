using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class ImageDatum
{
    public int Id { get; set; }

    public byte[] Photo { get; set; }

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}
