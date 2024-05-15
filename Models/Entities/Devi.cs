using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AspnetCoreMvcFull.Context;
using AspnetCoreMvcFull.Models.Devis;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace AspnetCoreMvcFull.Models.Entities;

public partial class Devi
{
  public int IdDevis { get; set; }

  public decimal? PrixTotal { get; set; }

  public DateOnly? DateDevis { get; set; }
  public DateOnly? DateFin { get; set; }

  public DateOnly? DateConstruction { get; set; }

  public int IdFinition { get; set; }

  public int IdTypeMaison { get; set; }

  public int IdUtilisateur { get; set; }

  public bool? EstPaye { get; set; }

  public bool? EstFini { get; set; }

  public string? RefDevis { get; set; }
  public virtual Finition IdFinitionNavigation { get; set; } = null!;

  public virtual TypeMaison IdTypeMaisonNavigation { get; set; } = null!;

  public virtual Utilisateur IdUtilisateurNavigation { get; set; } = null!;

  public virtual ICollection<Paiement> Paiements { get; set; } = new List<Paiement>();

  public virtual ICollection<Paiement> IdPaiements { get; set; } = new List<Paiement>();

  [NotMapped] public VEtatPaiementDevis VEtatPaiementDevis { get; set; }
  public string? Lieu { get; set; }

  [NotMapped] private List<TypeTravaux> TypeTravaux;

  public void setEtatPaiement(ConstructionDb constructionDb)
  {
    this.VEtatPaiementDevis =
      constructionDb.VEtatPaiementDevisEnumerable.Where(d => d.IdDevis == this.IdDevis).First();
  }

  public List<Devi> getDevisEnCours(ConstructionDb constructionDb)
  {
    List<Devi> devisEnCour = constructionDb.Devis.Where(d => d.EstFini == false).Include(d => d.IdTypeMaisonNavigation).Include(d=> d.IdFinitionNavigation).ToList();
    foreach (var dev in devisEnCour)
    {
      dev.setEtatPaiement(constructionDb);
    }

    return devisEnCour;
  }

  public List<Devi> getPaginedData(ConstructionDb constructionDb, int page)
  {
    int nombrePage = 0;
    int nombreAfficher = 10;
    return constructionDb.Devis.Skip(10).Take(5).ToList(); // ignore les 10 premier et prends les 5 pochains
  }

  public Devi getDevis(ConstructionDb constructionDb)
  {
    return constructionDb.
      Devis.
      Where(d => d.IdDevis == this.IdDevis).
      Include(d => d.IdFinitionNavigation).
      Include(d => d.Paiements)
      .First();
  }

  public void setDetailsDevi(ConstructionDb constructionDb)
  {
    List<DetailsDevi>
      detailsDevis =
        constructionDb.DetailsDevis.Where(dd => dd.idDevis == this.IdDevis).ToList(); // ireto daholo le asa atao
    //this.TypeTravaux = constructionDb.TypeTravauxes.ToList();
    List<TypeTravaux> typeTravauxes = constructionDb.TypeTravauxes.ToList();

    //  this.DetailsDevis = constructionDb.DetailsDevis.Where(dd => dd.idDevis == this.IdDevis).ToList();
  }

  public Finition getFinition(ConstructionDb constructionDb)
  {
    return constructionDb.Finitions.Where(f => f.IdFinition == this.IdFinition).First();
  }

  public void insertDetailsForImportedDevis(ConstructionDb constructionDb)
  {
    List<Devi> devisImportes = constructionDb.Devis.Where(d => d.DateFin == null).ToList();
    foreach (var devi in devisImportes)
    {
      devi.updateDevis(constructionDb);
    }
  }

  public void updateDevis(ConstructionDb constructionDb)
  {

    List<DevisParMaison> devisParMaisons = getDevisByTypeMaison(constructionDb);
    DetailsDevi[] detailsDetails = new DetailsDevi[devisParMaisons.Count];
    this.DateFin = dateFin(constructionDb);//dateFin(devisParMaisons); // calculer la date fin
    decimal prixSansMajoration = prixTotal(devisParMaisons); // caluler le prix total sans maj
    this.PrixTotal = prixSansMajoration + prixSansMajoration * getFinition(constructionDb).Majoration / 100; // prix avec maj

    constructionDb.Devis.Update(this);
    constructionDb.SaveChanges();
    foreach (var dm in devisParMaisons)
    {
      DetailsDevi devi = new DetailsDevi
      {
        Code = dm.Code,
        IdPrestation = dm.IdPrestation,
        IdTypeMaison = dm.IdTypeMaison,
        IdUnite = dm.IdUnite,
        Prestation = dm.Prestation,
        PrixUnitaire = dm.PrixUnitaire,
        Quantite = dm.Quantite,
        idDevis = this.IdDevis,
        Duree = dm.Duree
      };
      detailsDetails.Append(devi);
      constructionDb.DetailsDevis.Add(devi);
    }
    constructionDb.SaveChanges();
  }
  public void saveDevis(ConstructionDb constructionDb)
  {

    List<DevisParMaison> devisParMaisons = getDevisByTypeMaison(constructionDb);
    DetailsDevi[] detailsDetails = new DetailsDevi[devisParMaisons.Count];
    this.DateFin = dateFin(constructionDb);//dateFin(devisParMaisons); // calculer la date fin
    decimal prixSansMajoration = prixTotal(devisParMaisons); // caluler le prix total sans maj
    this.PrixTotal = prixSansMajoration + prixSansMajoration * getFinition(constructionDb).Majoration / 100; // prix avec maj
      constructionDb.Devis.Add(this);
      constructionDb.SaveChanges();

    foreach (var dm in devisParMaisons)
    {
      DetailsDevi devi = new DetailsDevi
      {
        Code = dm.Code,
        IdPrestation = dm.IdPrestation,
        IdTypeMaison = dm.IdTypeMaison,
        IdUnite = dm.IdUnite,
        Prestation = dm.Prestation,
        PrixUnitaire = dm.PrixUnitaire,
        Quantite = dm.Quantite,
        idDevis = this.IdDevis,
        Duree = dm.Duree
      };
      detailsDetails.Append(devi);
      constructionDb.DetailsDevis.Add(devi);
    }
    constructionDb.SaveChanges();
  }

  public List<DevisParMaison> getDevisByTypeMaison(ConstructionDb constructionDb)
  {
    return constructionDb.DevisParMaisons.Where(dpm => dpm.IdTypeMaison == this.IdTypeMaison).OrderBy(dpm => dpm.Code)
      .ToList();
  }

  public DateOnly dateFin(ConstructionDb constructionDb)
  {
    int duree = constructionDb.TypeMaisons.Where(tp => tp.IdTypeMaison == this.IdTypeMaison).First().Duree;
    return this.DateDevis.Value.AddDays(duree);
  }

  public DateOnly dateFin(List<DevisParMaison> devisParMaisons)
  {
    int jour = 0;
    foreach (var VARIABLE in devisParMaisons)
    {
      jour += VARIABLE.Duree;
    }

    return this.DateDevis.Value.AddDays(jour);
  }

  public decimal prixTotal(List<DevisParMaison> devisParMaisons)
  {
    decimal prix = 0;
    foreach (var VARIABLE in devisParMaisons)
    {
      prix += (decimal)(VARIABLE.Quantite * VARIABLE.PrixUnitaire);
    }

    return prix;
  }

  public List<DevisPdf> getDevisPdf(ConstructionDb constructionDb)
  {
    List<DevisPdf> devisPdfs = new List<DevisPdf>();
    List<TypeTravaux> typeTravauxes = constructionDb.TypeTravauxes.ToList();
    List<DetailsDevi>
      detailsDevis =
        constructionDb.DetailsDevis.Where(dd => dd.idDevis == this.IdDevis).ToList(); // ireto daholo le asa atao
/*
      foreach(TypeTravaux tt in typeTravauxes)
      {*/
    DevisPdf devisPdf = new DevisPdf("Trx" /*tt.Code*/, "TRAVAUX"); //tt.TypeTravaux1);
    double somme = 0;
    foreach (var dd in detailsDevis)
    {
      /*
        if (dd.idTypeTravaux == tt.IdTypeTravaux)
        {*/
      PrestationPdf prestationPdf = new PrestationPdf(dd.Code,
        dd.Prestation, constructionDb.Unites.Where(u => u.IdUnite == dd.IdUnite).First().Unite1, (double)dd.Quantite,
        (double)dd.PrixUnitaire);
      devisPdf.Prestations.Add(prestationPdf);
      /*}*/
      /*}*/

      if (devisPdf.Prestations.Count > 0)
      {
        /*
        devisPdf.Prestations.Add(new PrestationPdf(
            "Finition", "GOLD", "", 1, 1000
          ));*/
        devisPdfs.Add(devisPdf);
      }
    }

    double somme1 = 0;

    foreach (var VARIABLE in devisPdfs[0].Prestations)
    {
      somme1 += VARIABLE.PrixUnitaire * VARIABLE.Quantite;
    }

    devisPdfs[0].Prestations.Add(new PrestationPdf(
      "", this.IdFinitionNavigation.Finition1, "", (double)(this.IdFinitionNavigation.Majoration / 100), somme1
    ));
    return devisPdfs;
    /*
     *    List<DevisPdf> devisList = new List<DevisPdf>();
        PrestationPdf prestationPdf = new PrestationPdf("000", "mur de terracement", "m3", 23, 1200);

        DevisPdf devis = new DevisPdf()("000", "Travaux préparatoire");
        devis.Prestations.Add(prestationPdf);
        devis.Prestations.Add(prestationPdf);
        devis.Prestations.Add(prestationPdf);
        devis.Prestations.Add(prestationPdf);

        devisList.Add(devis);

        devisList.Add(devis);

        devisList.Add(devis);

        DevisTravauxPdf devisPdf = new DevisTravauxPdf();
        devisPdf.title = "DEVIS";
        devisPdf.DevisList = devisList;

        devisPdf.Open();
        devisPdf.Header();
        devisPdf.Content();
        devisPdf.Close();

        return View();

     */
  }
}
