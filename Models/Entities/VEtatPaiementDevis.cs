using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models.Entities;

[Table(name:"v_etat_devis_paiement")]
public class VEtatPaiementDevis
{
  [Key]
  [Column("id_devis")]
  public int IdDevis { get; set; }
  [Column("paye")]
  public double Paye { get; set; }
  [Column("prix_total")]
  public double PrixTotal { get; set; }

  public double percentageProgress()
  {
    Console.WriteLine(Paye);
    Console.WriteLine(PrixTotal);
    return ((Paye * 100) / PrixTotal);
  }
}
