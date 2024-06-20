using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models.Entities;

public partial class Unite
{
    public int IdUnite { get; set; }

    public string Unite1 { get; set; } = null!;

    public string? Initial { get; set; }

    public virtual ICollection<DetailsDevi> DetailsDevis { get; set; } = new List<DetailsDevi>();

    public virtual ICollection<Prestation> Prestations { get; set; } = new List<Prestation>();
}
