using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Entities;

public partial class VTrajetChauffeurNotOver
{
    public string? IdTrajet { get; set; }

    public string? Depart { get; set; }

    public string? Arrive { get; set; }

    public int? Kilometrage { get; set; }

    public double? MontantCarburant { get; set; }

    public string? Plaque { get; set; }

    public string? Details { get; set; }

    public DateTime? DateTrajet { get; set; }

    public string? IdChauffeur { get; set; }

    public string? NomPrenom { get; set; }
}
