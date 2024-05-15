using System.Text.Json;
using AspnetCoreMvcFull.Context;
using AspnetCoreMvcFull.Models.Entities;
using AspnetCoreMvcFull.Models.maisonTravaux;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMvcFull.Controllers.csv;

public class CsvController : Controller
{
  private readonly IWebHostEnvironment _hostEnvironment;
  private readonly ConstructionDb _constructionDb;
  private Utilisateur _utilisateur;
  public CsvController(IWebHostEnvironment hostingEnvironment, ConstructionDb constructionDb)
  {
    this._hostEnvironment = hostingEnvironment;
    this._constructionDb = constructionDb;/*
    string user = HttpContext.Session.GetString("user");
    _utilisateur = JsonSerializer.Deserialize<Utilisateur>(user);*/
  }

  // GET
  [HttpGet]
  public IActionResult MaisonTravaux()
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
  //  if (_utilisateur.Privilege > 10) return RedirectToRoute("/auth/loginbasic");
    return View();
  }

  [HttpGet]
  public IActionResult Devis()
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
  //  if (_utilisateur.Privilege > 10) return RedirectToRoute("/auth/loginbasic");
    return View();
  }

  [HttpGet]
  public IActionResult Paiement()
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
  //  if (_utilisateur.Privilege > 10) return RedirectToRoute("/auth/loginbasic");
    return View();
  }

  [HttpPost]
  public IActionResult MaisonTravaux(CsvDto csvDto)
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
  //  if (_utilisateur.Privilege > 10) return RedirectToRoute("/auth/loginbasic");
    Console.WriteLine("maison");
    try
    {
      string fileName = "maison.csv";
      csvDto.saveFile(_hostEnvironment.WebRootPath, fileName);
      csvDto.saveEntities(_hostEnvironment.WebRootPath, this._constructionDb);
      HttpContext.Session.SetString("M", "M");
    }
    catch (Exception e)
    {
      ModelState.AddModelError("error", e.Message);
    }
    return View();
  }

  public IActionResult MultipleInsertTravaux()
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
    // insertion des type de travaux ect
    // Exécution de la première série de fonctions
    _constructionDb.Database.ExecuteSql($"SELECT mutlipleInsert()");
    _constructionDb.Database.ExecuteSql($"SELECT insert_type_prestations()");
    _constructionDb.Database.ExecuteSql($"SELECT insert_type_quantite()");

// Exécution de la deuxième série de fonctions
    _constructionDb.Database.ExecuteSql($"SELECT insert_client()");
    _constructionDb.Database.ExecuteSql($"SELECT insert_finition()");
    _constructionDb.Database.ExecuteSql($"SELECT insert_devis()");

// Exécution de la troisième fonction
    _constructionDb.Database.ExecuteSql($"SELECT insert_paiement()");

    Devi devi = new Devi();
    devi.insertDetailsForImportedDevis(constructionDb:_constructionDb);

    return RedirectToAction("Index", "AdminDashBoard");
  }

  [HttpPost]
  public IActionResult Devis(CsvDto csvDto)
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
 //   if (_utilisateur.Privilege > 10) return RedirectToRoute("/auth/loginbasic");
    Console.WriteLine("devis");
    try
    {
      string fileName = "devis.csv";
      csvDto.saveFile(_hostEnvironment.WebRootPath, fileName);
      csvDto.saveDevis(_hostEnvironment.WebRootPath, this._constructionDb);
      HttpContext.Session.SetString("D", "D");
    }
    catch (Exception e)
    {
      ModelState.AddModelError("error", e.Message);
    }
    return View();
  }

  [HttpPost]
  public IActionResult Paiement(CsvDto csvDto)
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
   // if (_utilisateur.Privilege > 10) return RedirectToRoute("/auth/loginbasic");
    try
    {
      string fileName = "paiement.csv";
      csvDto.saveFile(_hostEnvironment.WebRootPath, fileName);
      csvDto.savePaiement(_hostEnvironment.WebRootPath, this._constructionDb);
      HttpContext.Session.SetString("P", "P");
    }
    catch (Exception e)
    {
      ModelState.AddModelError("error", e.Message);
    }

    Console.WriteLine("paiement");
    return View();
  }
}
