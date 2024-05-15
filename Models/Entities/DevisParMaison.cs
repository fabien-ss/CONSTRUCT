using System;
using System.Collections.Generic;
using AspnetCoreMvcFull.Context;

namespace AspnetCoreMvcFull.Models.Entities;

public partial class DevisParMaison
{
    public int? IdTypeMaison { get; set; }

    public decimal? Quantite { get; set; }

    public string? Prestation { get; set; }

    public int? IdPrestation { get; set; }

    public decimal? Total { get; set; }

    public string? Code { get; set; }

    public decimal? PrixUnitaire { get; set; }

    public string? Unite { get; set; }

    public int? IdUnite { get; set; }

  public int? idTypeTravaux { get; set; }
  public int Duree { get; set; }

  public List<DevisParMaison> GetDevisParMaisons(ConstructionDb constructionDb)
  {
    return constructionDb.DevisParMaisons.Where(dpm => dpm.IdTypeMaison == this.IdTypeMaison).ToList();
  }
}
