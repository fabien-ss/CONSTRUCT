using AspnetCoreMvcFull.Context;
using AspnetCoreMvcFull.Models.dashboard;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcFull.Controllers;

public class AdminDashBoardController : Controller
{
  private readonly ConstructionDb ConstructionDb;

  public AdminDashBoardController(ConstructionDb constructionDb)
  {
    this.ConstructionDb = constructionDb;
  }
  // GET
  [HttpGet]
  public IActionResult Index()
  {
    DashBoard dashBoard = new DashBoard();
    dashBoard.setMontantParMois(constructionDb:ConstructionDb);
    dashBoard.setMontantTotal(ConstructionDb);
    dashBoard.VSommePaiements = ConstructionDb.VSommePaiements.ToList();
    return View(dashBoard);
  }
  [HttpPost]
  public IActionResult Index(string annee)
  {
    DashBoard dashBoard = new DashBoard();
    Console.WriteLine("annee ",annee);
    dashBoard.TargetYear = annee;
    dashBoard.setMontantParMois(constructionDb:ConstructionDb);
    dashBoard.setMontantTotal(ConstructionDb);
    dashBoard.VSommePaiements = ConstructionDb.VSommePaiements.ToList();
    return View(dashBoard);
  }

}
