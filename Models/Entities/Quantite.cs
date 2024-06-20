using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models.Entities;

public partial class Quantite
{
    public int IdTypeMaison { get; set; }

    public int IdPrestation { get; set; }

    public decimal Quantite1 { get; set; }
    public int Duree { get; set; }

    public virtual Prestation IdPrestationNavigation { get; set; } = null!;

    public virtual TypeMaison IdTypeMaisonNavigation { get; set; } = null!;
    public double Surface { get; set; }
}
