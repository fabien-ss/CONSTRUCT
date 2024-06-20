using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcFull.Models.Entities;

public partial class Finition
{
  [Key]
    public int IdFinition { get; set; }

    public string Finition1 { get; set; } = null!;

    public decimal Majoration { get; set; }

    public string? Photo { get; set; }

    public virtual ICollection<Devi> Devis { get; set; } = new List<Devi>();
}
