using System;
using System.Collections.Generic;
using AspnetCoreMvcFull.Context;

namespace AspnetCoreMvcFull.Models.Entities;

public partial class Paiement
{
    public int IdPaiement { get; set; }

    public DateTime DatePaiement { get; set; }
    public double Montant { get; set; }

    public int? IdDevis { get; set; }

    public virtual Devi? IdDevis1 { get; set; }

    public virtual ICollection<Devi> IdDevisNavigation { get; set; } = new List<Devi>();
    public string RefPaiement { get; set; }

    public void payer(Devi d, ConstructionDb constructionDb)
    {
      if (d.EstPaye == true) throw new Exception("Ce devi est entierement payé");
      double resteAPayer = d.VEtatPaiementDevis.PrixTotal - (d.VEtatPaiementDevis.Paye + this.Montant);
      if (resteAPayer > 0)
      {
        constructionDb.Paiements.Add(this);
      }
      else if(resteAPayer == 0)
      {
        constructionDb.Paiements.Add(this);
       // resteAPayer = Math.Abs( d.VEtatPaiementDevis.PrixTotal - (d.VEtatPaiementDevis.Paye + this.Montant) );
      }
      /*else if (resteAPayer <= 0 && d.VEtatPaiementDevis.Paye > 0)
      {
        // 95 | 90 + 100 => 95 - 190 = 5
        this.Montant = Math.Abs(d.VEtatPaiementDevis.PrixTotal - (d.VEtatPaiementDevis.Paye + this.Montant));
        d.EstPaye = true;
        constructionDb.Paiements.Add(this);
        constructionDb.Devis.Update(d);
      }else if (resteAPayer <= 0 && d.VEtatPaiementDevis.Paye <= 0)
      {
        // 95 | 0 + 100 => 95 - 100 = 5
        this.Montant = Math.Abs(d.VEtatPaiementDevis.PrixTotal);
        d.EstPaye = true;
        constructionDb.Paiements.Add(this);
        constructionDb.Devis.Update(d);
      }*/
        constructionDb.SaveChanges();
    }
}
