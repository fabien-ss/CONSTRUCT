using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models.maisonTravaux;

[Table("paiement_temp")]
public class PaiementTemp
{
  [Key]
  [Column("id")]
  public int id { get; set; }
  [Column("ref_devis")]
  public string RefDevis { get; set; }
  [Column("ref_paiement")]
  public string RefPaiement { get; set; }
  [Column("date_paiement")]
  public string DatePaiement { get; set; }
  [Column("montant")]
  public string Montant { get; set; }

[NotMapped]
  public string ref_devis
  {
    get => RefDevis;
    set => RefDevis = value.Trim() ?? throw new ArgumentNullException(nameof(value));
  }
  [NotMapped]
  public string ref_paiement
  {
    get => RefPaiement;
    set => RefPaiement = value.Trim() ?? throw new ArgumentNullException(nameof(value));
  }
  [NotMapped]
  public string date_paiement
  {
    get => DatePaiement;
    set => DatePaiement = value.Trim() ?? throw new ArgumentNullException(nameof(value));
  }
  [NotMapped]
  public string montant
  {
    get => Montant;
    set => Montant = value.Trim().Replace(",",".") ?? throw new ArgumentNullException(nameof(value));
  }
}
