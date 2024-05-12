using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using AspnetCoreMvcFull.Entities;

namespace AspnetCoreMvcFull.Models.DTO;

public class RegisterDto: Dto
{

  [Required(ErrorMessage = "Le nom est obligatoire.")]
  [StringLength(500, ErrorMessage = "Le nom ne doit pas dépasser 500 caractères.")]
  public string Name { get; set; }

  [Required(ErrorMessage = "Le prénom est obligatoire.")]
  [StringLength(500, ErrorMessage = "Le prénom ne doit pas dépasser 500 caractères.")]
  public string FirstName { get; set; }

  [Required(ErrorMessage = "La date de naissance est obligatoire.")]
  [DataType(DataType.Date)]
  [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
  public DateOnly Birth { get; set; }

  [Required(ErrorMessage = "L'email est obligatoire.")]
  [EmailAddress(ErrorMessage = "Veuillez entrer une adresse e-mail valide.")]
  public string Email { get; set; }

  [Required(ErrorMessage = "Password required.")]
  [DataType(DataType.Password)]
  public string Password { get; set; }

  [Required(ErrorMessage = "Validate your password.")]
  [DataType(DataType.Password)]
  [Compare("PasswordValidation", ErrorMessage = "Password doesn't mach")]
  public string PasswordValidation { get; set; }

  [Required(ErrorMessage = "Phone number is required.")]
  [DataType(DataType.PhoneNumber)]
  [RegularExpression(@"^\+?1?\d{9,15}$", ErrorMessage = "Not a valid number. (03########)")]
  public string NumeroTelephone { get; set; }

  [DataType(DataType.Upload)] public IFormFile Image { get; set; }
  public object mapDtoToEntity()
  {
    return new Utilisateur
    {
      Nom = this.Name,
      Prenom = this.FirstName,
      Email = this.Email,
      MotDePasse = this.Password,
      DateNaissance = this.Birth.ToDateTime(TimeOnly.MinValue),
      DateInscription = DateTime.Now
    };
  }
}
