using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models.Entities;

public partial class DetailsDevi
{
 // public int idTypeTravaux;

  [Column("id_devis")]
    public int idDevis { get; set; }
    public int IdDetailsDevis { get; set; }

    public int? IdPrestation { get; set; }

    public int? IdTypeMaison { get; set; }

    public decimal? Quantite { get; set; }

    public string? Prestation { get; set; }

    public decimal? Total { get; set; }

    public string? Code { get; set; }

    public decimal? PrixUnitaire { get; set; }

    public string? Unite { get; set; }

    public int? IdUnite { get; set; }

    public int Duree { get; set; }
    public virtual Prestation? IdPrestationNavigation { get; set; }

    public virtual TypeMaison? IdTypeMaisonNavigation { get; set; }

    public virtual Unite? IdUniteNavigation { get; set; }
}
