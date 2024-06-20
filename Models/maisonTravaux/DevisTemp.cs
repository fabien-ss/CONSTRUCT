using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models.maisonTravaux;

[Table("devis_temp")]
public class DevisTemp
{
  [Key]
  [Column("id")]
  public int? Id { get; set; }
  [Column("client")] public string Client { get; set; }
  [Column("ref_devis")] public string RefDevis { get; set; }
  [Column("type_maison")] public string TypeMaison { get; set; }
  [Column("finition")] public string Finition { get; set; }
  [Column("taux_finition")] public string TauxFinition { get; set; }
  [Column("date_devis")] public string DateDevis { get; set; }
  [Column("date_debut")] public string DateDebut { get; set; }
  [Column("lieu")] public string Lieu { get; set; }

  [NotMapped]
  public string client
  {
    get => Client;
    set => Client = value.Trim() ?? throw new ArgumentNullException(nameof(value));
  }

  [NotMapped]
  public string ref_devis
  {
    get => RefDevis;
    set => RefDevis = value.Trim() ?? throw new ArgumentNullException(nameof(value));
  }

  [NotMapped]
  public string type_maison
  {
    get => TypeMaison;
    set => TypeMaison = value.Trim() ?? throw new ArgumentNullException(nameof(value));
  }

  [NotMapped]
  public string finition
  {
    get => Finition;
    set => Finition = value.Trim() ?? throw new ArgumentNullException(nameof(value));
  }

  [NotMapped]
  public string taux_finition
  {
    get => TauxFinition;
    set => TauxFinition = value.Replace("%", "").Replace(",",".").Trim() ?? throw new ArgumentNullException(nameof(value));
  }

  [NotMapped]
  public string date_devis
  {
    get => DateDevis;
    set => DateDevis = value.Trim() ?? throw new ArgumentNullException(nameof(value));
  }

  [NotMapped]
  public string date_debut
  {
    get => DateDebut;
    set => DateDebut = value.Trim() ?? throw new ArgumentNullException(nameof(value));
  }

  [NotMapped]
  public string lieu
  {
    get => Lieu;
    set => Lieu = value.Trim() ?? throw new ArgumentNullException(nameof(value));
  }
}
