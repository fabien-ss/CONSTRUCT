using System.ComponentModel.DataAnnotations;
using AspnetCoreMvcFull.Models.Entities;

namespace AspnetCoreMvcFull.Controllers.auth;

public class LoginAdminDto
{
  [Required(ErrorMessage = "Email is required")]
  public string Email{ get; set;}
  [Required(ErrorMessage = "Password is required")]
  public string MotDePasse{ get; set;}

  public Utilisateur mapDtoToEntity()
  {
    return new Utilisateur
    {
      Email = this.Email,
      MotDePasse = this.MotDePasse
    };
  }
}
