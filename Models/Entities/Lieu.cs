using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetCoreMvcFull.Models.Entities;

[Table("lieu")]
public class Lieu
{
  [Key]
  public int? id { get; set; }
  public string lieu { get; set; }
}
