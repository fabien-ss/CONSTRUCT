using System.ComponentModel.DataAnnotations;
using AspnetCoreMvcFull.Models.Entities;

namespace AspnetCoreMvcFull.Models.DTO;

public class LoginDto : Dto
{
  [Required(ErrorMessage = "Phone number is required.")]
  [DataType(DataType.PhoneNumber)]
  [RegularExpression(@"^03\d{8}$", ErrorMessage = "Not a valid number. (03########)")]
  private string numero;
/*
  private string _email;
  private string _password;

  [Required(ErrorMessage = "Email can't be null")]
  [EmailAddress(ErrorMessage = "Provide a valid email")]
  public string Email
  {
    get => _email;
    set => _email = value; //?? throw new ArgumentNullException(nameof(value));
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
*/
  [Required(ErrorMessage = "Phone number is required.")]
  [DataType(DataType.PhoneNumber)]
  [RegularExpression(@"^\+?1?\d{9,15}$", ErrorMessage = "Not a valid number. (03########)")]
  public string Numero
  {
    get => numero;
    set => numero = value ?? throw new ArgumentNullException(nameof(value));
  }

  public object mapDtoToEntity()
  {
    Utilisateur chauffeur = new Utilisateur
    {
      Numero = this.Numero
    };
    return chauffeur;
  }
}
