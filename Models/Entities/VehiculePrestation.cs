using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Entities;

public partial class VehiculePrestation
{
    public string? Plaque { get; set; }

    public string IdPrestation { get; set; } = null!;

    public DateTime? DateHeure { get; set; }

    public double? Montant { get; set; }

    public string? Details { get; set; }

    public virtual Vehicule? PlaqueNavigation { get; set; }
}
