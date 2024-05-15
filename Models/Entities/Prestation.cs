using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.Models.Entities;

public partial class Prestation
{
  [Key]
    public int IdPrestation { get; set; }

    public string? Prestation1 { get; set; }

    public string Code { get; set; } = null!;

    public decimal? PrixUnitaire { get; set; }

    public int? Duree { get; set; }

    public int IdUnite { get; set; }

    public int? IdPrestation1 { get; set; }

    public int IdTypeTravaux { get; set; }

    public virtual ICollection<DetailsDevi> DetailsDevis { get; set; } = new List<DetailsDevi>();

    public virtual Prestation? IdPrestation1Navigation { get; set; }

    public virtual TypeTravaux? IdTypeTravauxNavigation { get; set; }

    public virtual Unite? IdUniteNavigation { get; set; }

    public virtual ICollection<Prestation> InverseIdPrestation1Navigation { get; set; } = new List<Prestation>();

    public virtual ICollection<Quantite> Quantites { get; set; } = new List<Quantite>();
}
