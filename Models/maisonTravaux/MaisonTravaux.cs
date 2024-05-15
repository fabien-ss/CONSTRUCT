using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace AspnetCoreMvcFull.Models.maisonTravaux;

[Table("maison_travaux")]
public class MaisonTravaux
{
  [Key] [Column("id")] public int? Id { get;set; }
  [Column("type_maison")] public string TypeMaison { get;set; }
  [Column("description")] public string Description { get;set; }
  [Column("surface")] public double Surface { get;set; }
  [Column("code_travaux")] public string CodeTravaux { get;set; }
  [Column("type_travaux")] public string TypeTravaux { get;set; }
  [Column("unite")] public string Unite { get;set; }
  [Column("prix_unitaire")] public decimal PrixUnitaire { get;set; }
  [Column("quantite")] public decimal Quantite { get;set; }
  [Column("duree_travaux")] public int DureeTravaux { get;set; }


  [NotMapped]
  public string type_maison
  {
    get => TypeMaison;
    set => TypeMaison = value.Trim() ?? throw new ArgumentNullException(nameof(value));
  }

  [NotMapped]
  public string description
  {
    get => Description;
    set => Description = value.Trim() ?? throw new ArgumentNullException(nameof(value));
  }

  [NotMapped]
  public string surface
  {
    get => Surface.ToString("N");
    set => Surface = int.Parse(value.Trim());
  }

  [NotMapped]
  public string code_travaux
  {
    get => CodeTravaux;
    set => CodeTravaux = value.Trim() ?? throw new ArgumentNullException(nameof(value));
  }

  [NotMapped]
  public string type_travaux
  {
    get => TypeTravaux;
    set => TypeTravaux = value.Trim() ?? throw new ArgumentNullException(nameof(value));
  }

  [NotMapped]
  public string unité
  {
    get => Unite;
    set => Unite = value.Trim() ?? throw new ArgumentNullException(nameof(value));
  }

  [NotMapped]
  public string prix_unitaire
  {
    get => PrixUnitaire.ToString("N");
    set => PrixUnitaire = decimal.Parse(value.Trim().Replace(",", "."));
  }

  [NotMapped]
  public string quantite
  {
    get => Quantite.ToString("N");
    set => Quantite = decimal.Parse(value.Trim().Replace(",", "."));
  }

  [NotMapped]
  public string duree_travaux
  {
    get => DureeTravaux.ToString();
    set => DureeTravaux = int.Parse(value.Trim());
  }
}
