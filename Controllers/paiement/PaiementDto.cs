using System.ComponentModel.DataAnnotations;
using AspnetCoreMvcFull.Models.Entities;

namespace AspnetCoreMvcFull.Controllers.devis;

public class PaiementDto
{
  [Required(ErrorMessage = "Id devis is null")]
  public int IdDevis{ get; set;}
  [Required(ErrorMessage = "Montant id ni")]
  public string Montant{ get; set;}

  [Required(ErrorMessage = "Date must be")]
  public DateTime DatePaiemment { get; set; }

  public Paiement mapDtoToEntity()
  {
    return new Paiement
    {
      IdDevis = this.IdDevis,
      Montant = double.Parse(this.Montant),
      DatePaiement = this.DatePaiemment
    };
  }
}
