using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models.Entities;

public partial class TypeTravaux
{
    public int IdTypeTravaux { get; set; }

    public string Code { get; set; } = null!;

    public string? TypeTravaux1 { get; set; }

    public virtual ICollection<Prestation> Prestations { get; set; } = new List<Prestation>();

    [NotMapped] public List<Prestation> PrestationsList;
}
