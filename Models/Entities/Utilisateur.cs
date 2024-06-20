using System;
using System.Collections.Generic;
using AspnetCoreMvcFull.Context;

namespace AspnetCoreMvcFull.Models.Entities;

public partial class Utilisateur
{
  public int IdUtilisateur { get; set; }

  public string? Nom { get; set; }

  public string? Prenom { get; set; }

  public string? Numero { get; set; }

  public string? Email { get; set; }

  public int Privilege { get; set; }

  public string? MotDePasse { get; set; }

  public string? Photo { get; set; }

  public virtual ICollection<Devi> Devis { get; set; } = new List<Devi>();

  public Utilisateur getOrCreateUser(ConstructionDb prom13)
  {
    try
    {
      return prom13.Utilisateurs.Where(u => u.Numero == this.Numero).First();
    }
    catch (Exception e)
    {
      prom13.Utilisateurs.Add(this);
      prom13.SaveChanges();
      return this.getOrCreateUser(prom13);
    }
  }

  public Utilisateur findAdmin(ConstructionDb constructionDb)
  {
    try
    {
      return constructionDb.Utilisateurs
        .Where(u => u.Privilege > 0 && u.Email == this.Email && u.MotDePasse == this.MotDePasse).First();
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      throw new Exception("No user found "+e.Message);
    }
  }
}
