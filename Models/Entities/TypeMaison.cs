using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models.Entities;

public partial class TypeMaison
{
    public int IdTypeMaison { get; set; }

    public string TypeMaison1 { get; set; } = null!;

    public int Duree { get; set; }

    public string? Photo { get; set; }

    public decimal? PrixTotal { get; set; }

    public string? Desciption { get; set; }

    public virtual ICollection<DetailsDevi> DetailsDevis { get; set; } = new List<DetailsDevi>();

    public virtual ICollection<Devi> Devis { get; set; } = new List<Devi>();

    public virtual ICollection<Quantite> Quantites { get; set; } = new List<Quantite>();
    public double Surface { get; set; }
}
