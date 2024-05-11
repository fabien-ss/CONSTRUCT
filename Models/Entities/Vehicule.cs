using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Entities;

public partial class Vehicule
{
    public string Plaque { get; set; } = null!;

    public DateOnly? DateMiseEnRoute { get; set; }

    public string? IdVehiculeType { get; set; }

    public virtual VehiculeType? IdVehiculeTypeNavigation { get; set; }

    public virtual ICollection<Trajet> Trajet { get; set; } = new List<Trajet>();

    public virtual ICollection<VehiculePrestation> VehiculePrestation { get; set; } = new List<VehiculePrestation>();
}
