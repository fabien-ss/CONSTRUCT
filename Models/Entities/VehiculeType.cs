using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Entities;

public partial class VehiculeType
{
    public string IdVehiculeType { get; set; } = null!;

    public string VehiculeType1 { get; set; } = null!;

    public virtual ICollection<Vehicule> Vehicule { get; set; } = new List<Vehicule>();
}
