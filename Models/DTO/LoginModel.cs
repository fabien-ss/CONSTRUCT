using System.ComponentModel.DataAnnotations;
using AspnetCoreMvcFull.Entities;

namespace AspnetCoreMvcFull.Models.DTO;

public class LoginModel : Dto
{

  private string _email;
  private string _password;

  [Required(ErrorMessage = "Email can't be null")]
  [EmailAddress(ErrorMessage = "Provide a valid email")]
  public string Email
  {
    get => _email;
    set => _email = value ?? throw new ArgumentNullException(nameof(value));
  }

  [Required(ErrorMessage = "Password can't be null")]
  public string Password
  {
    get => _password;
    set
    {
      this._password = value;
    }
  }

  public object mapDtoToEntity()
  {
    Chauffeur chauffeur = new Chauffeur
    {
        Email = this.Email,
        MotDePasse = this.Password
    };
    return chauffeur;
  }
}
