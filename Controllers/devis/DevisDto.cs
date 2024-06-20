using System.ComponentModel.DataAnnotations;
using AspnetCoreMvcFull.Models.Devis;
using AspnetCoreMvcFull.Models.Entities;

namespace AspnetCoreMvcFull.Controllers.devis;

public class DevisDto
{
  [Required(ErrorMessage = "Type maison can't be null")]
  public int TypeMaison { get; set; }
  [Required(ErrorMessage = "Type finition can't be null")]
  public int TypeFinition { get; set; }

  [Required(ErrorMessage = "Date debut est requis")]
  public DateOnly DateDebut { get; set; }

  [Required(ErrorMessage = "Lieu is required")]
  public string Lieu { get; set; }

  public Devi mapDtoToEntity()
  {
    return new Devi
    {
      IdTypeMaison = this.TypeMaison,
      IdFinition = Convert.ToInt32(this.TypeFinition),
      DateDevis = this.DateDebut,
      Lieu = this.Lieu
    };
  }
}
