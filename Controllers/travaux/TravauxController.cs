using System.Text.Json;
using AspnetCoreMvcFull.Context;
using AspnetCoreMvcFull.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcFull.Controllers.travaux;

public class TravauxController : Controller
{
  private readonly ConstructionDb ConstructionDb;
  public TravauxController(ConstructionDb constructionDb)
  {
    this.ConstructionDb = constructionDb;
  }
  // GET
  public IActionResult Index()
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
    return View();
  }

  public IActionResult Travaux(int idDevis)
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
    Devi devi = ConstructionDb.Devis.Where(d => d.IdDevis == idDevis).First();
    devi.setEtatPaiement(this.ConstructionDb);
    List<DevisParMaison> devisParMaisons = new List<DevisParMaison>();
    try
    {
      DevisParMaison d = new DevisParMaison { IdTypeMaison = devi.IdTypeMaison };
      devisParMaisons = d.GetDevisParMaisons(this.ConstructionDb);
    }
    catch (Exception e)
    {
      ModelState.AddModelError("error ", e.Message);
    }

    ViewData["DevisParMaison"] = devisParMaisons;
    return View();
  }
}
