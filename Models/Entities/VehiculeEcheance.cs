using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Entities;

public partial class VehiculeEcheance
{
    public string? Plaque { get; set; }

    public string? IdEcheance { get; set; }

    public DateOnly? DateDebut { get; set; }

    public DateOnly? DateFin { get; set; }

    public double? MontantRecu { get; set; }

    public virtual Echeance? IdEcheanceNavigation { get; set; }

    public virtual Vehicule? PlaqueNavigation { get; set; }
}
