using System.Text.Json;
using AspnetCoreMvcFull.Context;
using AspnetCoreMvcFull.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMvcFull.Controllers.database;

public class DataBaseController : Controller
{

  private readonly ConstructionDb ConstructionDb;
  public DataBaseController(ConstructionDb constructionDb)
  {
    this.ConstructionDb = constructionDb;
  }

  // GET
  [HttpGet]
  public IActionResult Index()
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
    //if (_utilisateur.Privilege > 10) return RedirectToRoute("/auth/loginbasic");
    return View();
  }


  public IActionResult Purge()
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
    //if (_utilisateur.Privilege > 10) return RedirectToRoute("/auth/loginbasic");
    // check user aloa
    ConstructionDb.Database.ExecuteSql($"TRUNCATE details_devis, devis, unite, devis_temp, finition, maison_travaux, paiement, paiement_temp, prestation, quantite, type_maison CASCADE");
    ConstructionDb.Database.ExecuteSql($"TRUNCATE utilisateur cascade ");
    ConstructionDb.Database.ExecuteSql(
      $"insert into utilisateur (numero, email, privilege, mot_de_passe) values ('0322021225','admin@gmail.com',10,'adminadmin')");
    ConstructionDb.SaveChanges();
    return RedirectToAction("Index", "DataBase");
  }
}
