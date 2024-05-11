using System.Diagnostics;
using System.Text.Json;
using AspnetCoreMvcFull.Entities;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Controllers;

public class DashboardsController : Controller
{
  public DashboardsController()
  {
  }

  public IActionResult Index()
  {
    string user = HttpContext.Session.GetString("user");

    Chauffeur chauffeur = JsonSerializer.Deserialize<Chauffeur>(user);
    Console.WriteLine("user "+ user);
    Console.WriteLine("ALORS");
    /*
   */
    return View();
  }
}
