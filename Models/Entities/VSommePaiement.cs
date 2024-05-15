using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMvcFull.Models.Entities;

[Keyless]
[Table("v_somme_paiement")]
public class VSommePaiement
{
  [Column("id_devis")]
  public int IdDevis { get; set; }
  [Column("montant")]
  public double Montant { get; set; }


}
