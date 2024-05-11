using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AspnetCoreMvcFull.Context;
using AspnetCoreMvcFull.Models.StateFullContrainst;

namespace AspnetCoreMvcFull.Entities;

public partial class Chauffeur: StateMethod
{
    [Key]
    public string? IdChauffeur { get; set; }

    public string NomPrenom { get; set; } = null!;

    public DateOnly? DateNaissance { get; set; }

    public string Numero { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string MotDePasse { get; set; } = null!;

    public string Sexe { get; set; } = null!;

    public virtual ICollection<Trajet> Trajet { get; set; } = new List<Trajet>();

    public Chauffeur getChauffeur(Prom13 prom13)
    {
      var findChauffeur = prom13.Chauffeur.Where(c=> c.Email == this.Email).First();
      if (findChauffeur.MotDePasse !=  this.MotDePasse)
      {
        throw new Exception("Mot de passe incorect");
      }
      return findChauffeur;
    }
}
