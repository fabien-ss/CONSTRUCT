using System.Text.Json;
using System.Text.Json.Serialization;
using AspnetCoreMvcFull.Context;
using AspnetCoreMvcFull.Controllers.devis;
using AspnetCoreMvcFull.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcFull.Controllers.paiement;

public class PaiementController : Controller
{

  private readonly ConstructionDb ConstructionDb;

  public PaiementController(ConstructionDb constructionDb)
  {
    this.ConstructionDb = constructionDb;
  }

  // GET
  public IActionResult Index()
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
    return View();
  }

  [HttpPost]
  public IActionResult Payer([FromBody] PaiementDto paiementDto)
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
    try
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      // Récupération de l'objet Devi
      Devi devi = this.ConstructionDb.Devis.First(d => d.IdDevis == paiementDto.IdDevis);
      try
      {
        double d = double.Parse(paiementDto.Montant);
      }
      catch (Exception e)
      {
        return StatusCode(501, $"Internal server error: montant invalide");
      }
      if (devi == null)
      {
        return NotFound("Devis not found.");
      }

      // Récupération de l'état de paiement
      var vEtatPaiementDevis = ConstructionDb.VEtatPaiementDevisEnumerable.First(d => d.IdDevis == devi.IdDevis);
      if (vEtatPaiementDevis == null)
      {
        return NotFound("VÉtatPaiementDevis not found.");
      }

      if ((vEtatPaiementDevis.Paye + double.Parse(paiementDto.Montant)) > vEtatPaiementDevis.PrixTotal)
      {
        return StatusCode(501, $"Internal server error: trop eleve");
      }
      devi.VEtatPaiementDevis = vEtatPaiementDevis;

      // Mappage et traitement du paiement
      Paiement p = paiementDto.mapDtoToEntity();
      p.payer(devi, ConstructionDb);
      var options = new JsonSerializerOptions
      {
        ReferenceHandler = ReferenceHandler.Preserve
      };
      ConstructionDb.SaveChanges();
   //   ConstructionDb.Dispose();

//      string json = JsonSerializer.Serialize(devi, options);
      devi.VEtatPaiementDevis.Paye += double.Parse(paiementDto.Montant);
      string vEtat = JsonSerializer.Serialize(devi.VEtatPaiementDevis, options);
      // Retour de l'objet Devi sous forme de JSON
      return Ok(vEtat);
    }
    catch (Exception ex)
    {
      // Gestion des exceptions
      return StatusCode(500, $"Internal server error: {ex.Message}");
    }
  }


  [HttpGet]
  public IActionResult Payer(int idDevis)
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
    devi.VEtatPaiementDevis = ConstructionDb.VEtatPaiementDevisEnumerable.Where(d => d.IdDevis == devi.IdDevis).First();
    devi.VEtatPaiementDevis = ConstructionDb.VEtatPaiementDevisEnumerable.Where(v => v.IdDevis == idDevis).First();
    return View(devi);
  }
}
