using System.Text.Json;
using AspnetCoreMvcFull.Context;
using AspnetCoreMvcFull.Models.Devis;
using AspnetCoreMvcFull.Models.Documents;
using AspnetCoreMvcFull.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMvcFull.Controllers.devis;

public class DevisController : Controller
{

  private ConstructionDb ConstructionDb;

  public DevisController(ConstructionDb constructionDb)
  {
    this.ConstructionDb = constructionDb;
  }

  public void CheckAdmin()
  {
    try
    {
      string user = HttpContext.Session.GetString("user");
      Utilisateur admin = JsonSerializer.Deserialize<Utilisateur>(user);
    }
    catch (Exception e)
    {
      throw new Exception("You don't have enough permission");
    }
  }

  public IActionResult DevisEnCours()
  {
    try
    {
      string user = HttpContext.Session.GetString("user");
      Utilisateur userq = JsonSerializer.Deserialize<Utilisateur>(user);
      if (userq.Privilege < 10) throw new Exception("User non autorisé");
    }
    catch (Exception e)
    {
      return RedirectToAction("LoginBasic","Auth");
    }
    List<Devi> devis = new List<Devi>();
    try
    {
      devis = new Devi().getDevisEnCours(ConstructionDb);
    }
    catch(Exception e)
    {
      ModelState.AddModelError("error ", e.Message);
    }

    ViewData["devis"] = devis;
    return View();
  }

  public IActionResult Exporter(int idDevis)
  {
    try
    {
      string user = HttpContext.Session.GetString("user");
      Utilisateur userq = JsonSerializer.Deserialize<Utilisateur>(user);
    }
    catch (Exception e)
    {
      return RedirectToAction("LoginBasic","Auth");
    }
    Devi devi = new Devi { IdDevis = idDevis };
    devi = devi.getDevis(ConstructionDb);
    devi.setDetailsDevi(ConstructionDb);

    List<DevisPdf> devisPdfs = devi.getDevisPdf(ConstructionDb);
    DevisTravauxPdf devisPdf = new DevisTravauxPdf();
    devisPdf.Devi = devi;

    Dictionary<string, string> data = new Dictionary<string, string>();
    data.Add("Total ", devi.PrixTotal.Value.ToString("N") + " Ar");
    data.Add("Date devis ", devi.DateDevis.ToString() + " mm/jj/aaaa");
    data.Add("Debut construction ", devi.DateFin.ToString() + " mm/jj/aaaa");

    devisPdf.LeftData = data;
    devisPdf.title = "DEVIS";
    devisPdf.DevisList = devisPdfs;

    devisPdf.Open();
    devisPdf.Header();
    devisPdf.Content();
    devisPdf.BuildPaiement();
    devisPdf.Close();
    ViewData["PDF"] = devisPdf.Path;
    return View();
  }

  public IActionResult MesDevis()
  {
    try
    {
      string user1 = HttpContext.Session.GetString("user");
      Utilisateur userq = JsonSerializer.Deserialize<Utilisateur>(user1);
    }
    catch (Exception e)
    {
      return RedirectToAction("LoginBasic","Auth");
    }
    string user = HttpContext.Session.GetString("user");
    Utilisateur chauffeur = JsonSerializer.Deserialize<Utilisateur>(user);
    List<Devi> mesDevis = ConstructionDb.Devis.
      Where(u => u.IdUtilisateur == chauffeur.IdUtilisateur).
      Include(d=>d.IdFinitionNavigation).Include(d => d.IdTypeMaisonNavigation).ToList();
    foreach (var devi in mesDevis)
    {
      devi.VEtatPaiementDevis =
        ConstructionDb.VEtatPaiementDevisEnumerable.Where(d => d.IdDevis == devi.IdDevis).First();
    }
    ViewData["MesDevis"] = mesDevis;
    return View();
  }

  [HttpGet]
  public IActionResult Create()
  {
    try
    {
      string user = HttpContext.Session.GetString("user");
      Utilisateur chauffeur = JsonSerializer.Deserialize<Utilisateur>(user);
    }
    catch (Exception e)
    {
      return RedirectToRoute("/auth/loginbasic");
    }
    ViewData["TypeMaison"] = ConstructionDb.TypeMaisons.ToList();
    ViewData["Finition"] = this.ConstructionDb.Finitions.ToList();
    ViewData["Lieu"] = this.ConstructionDb.Lieus.ToList();
    return View();
  }

  [HttpPost]
  public IActionResult Create(DevisDto devisDto)
  {
    try
    {
      string user = HttpContext.Session.GetString("user");
      Utilisateur chauffeur = JsonSerializer.Deserialize<Utilisateur>(user);
    }
    catch (Exception e)
    {
      return RedirectToRoute("/auth/loginbasic");
    }
    if(!ModelState.IsValid) ModelState.AddModelError("Formulaire invalide", "Formulaire invalide");
    if (ModelState.IsValid)
    {
      string user = HttpContext.Session.GetString("user");
      Utilisateur chauffeur = JsonSerializer.Deserialize<Utilisateur>(user);
      Devi devi = devisDto.mapDtoToEntity();
      devi.insertDetailsForImportedDevis(this.ConstructionDb);
      devi.IdUtilisateur = chauffeur.IdUtilisateur;
      devi.saveDevis(this.ConstructionDb);
    }
    ViewData["TypeMaison"] = ConstructionDb.TypeMaisons.ToList();
    ViewData["Finition"] = this.ConstructionDb.Finitions.ToList();
    ViewData["Lieu"] = this.ConstructionDb.Lieus.ToList();
    return View();
  }


  public IActionResult getDevis()
  {
    try
    {
      string user = HttpContext.Session.GetString("user");
      Utilisateur userq = JsonSerializer.Deserialize<Utilisateur>(user);
    }
    catch (Exception e)
    {
      return RedirectToAction("LoginBasic","Auth");
    }
    List<DevisPdf> devisList = new List<DevisPdf>();
    PrestationPdf prestationPdf = new PrestationPdf("000", "mur de terracement", "m3", 23, 1200);

    DevisPdf devis = new DevisPdf("000", "Travaux préparatoire");
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
  }
}
