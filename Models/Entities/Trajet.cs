using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Entities;

public partial class Trajet
{
    public string IdTrajet { get; set; } = null!;

    public string? Depart { get; set; }

    public string? Arrive { get; set; }

    public int? Kilometrage { get; set; }

    public double? MontantCarburant { get; set; }

    public string? Plaque { get; set; }

    public string Details { get; set; } = null!;

    public DateTime? DateTrajet { get; set; }

    public string? IdChauffeur { get; set; }

    public virtual Chauffeur? IdChauffeurNavigation { get; set; }

    public virtual Vehicule? PlaqueNavigation { get; set; }
}
