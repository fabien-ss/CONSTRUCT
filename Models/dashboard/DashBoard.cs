using AspnetCoreMvcFull.Context;
using AspnetCoreMvcFull.Models.Entities;

namespace AspnetCoreMvcFull.Models.dashboard;

public class DashBoard
{
  public double MontantTotalDevis { get; set; }
  public string TargetYear { get; set; } = "2024";
  public List<VSommePaiement> VSommePaiements { get; set; }
  public List<MontantParMois> MontantParMoisList { get; set; }

  public double sommePaiement()
  {
    return this.VSommePaiements.Sum(p => p.Montant);
  }

  public decimal? montantParAnne()
  {
    //16341429
    double sm = 0;
    foreach (var VARIABLE in MontantParMoisList)
    {
      sm += (double)VARIABLE.Montant;
    }

    return MontantParMoisList.Sum(m => m.Montant);
  }

  public string getMontantParMois()
  {
    MontantParMoisList = MontantParMoisList.OrderBy(e => e.Numero).ToList();
    string data = "[";
    foreach (var montant in MontantParMoisList)
    {
      data += montant.Montant + ",";
    }
    Console.WriteLine(data);
    return data + "]";
  }
  public string GetMois()
  {
    string data = "[";
    foreach (var montant in MontantParMoisList)
    {
      // Utilisez la méthode Replace pour échapper les apostrophes
      string moisEchappe = montant.Mois.Replace("'", "&#x27;");
      data += "'" + moisEchappe + "',";
    }
    // Supprimez la dernière virgule et ajoutez la fermeture de crochets
    data = data.TrimEnd(',') + "]";
    Console.WriteLine(data);
    return data;
  }


  public void setMontantParMois(ConstructionDb constructionDb)
  {
    this.MontantParMoisList = constructionDb.MontantParMoisEnumerable.Where(m => m.Annees == this.TargetYear).ToList();
  }

  public void setMontantTotal(ConstructionDb constructionDb)
  {
    List<Devi> devis = constructionDb.Devis.ToList();
    this.MontantTotalDevis = calculer(devis);
  }

  public double calculer(List<Devi> devis)
  {
    double sm = 0;
    foreach (var d in devis)
    {
      sm += (double)d.PrixTotal;
    }

    return sm;
  }
}
