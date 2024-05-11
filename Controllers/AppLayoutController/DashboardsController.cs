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
    try
    {
      HttpContext.Session.GetString("user");
    }
    catch (Exception e)
    {
      ModelState.AddModelError("Login", "Log to see more");
      RedirectToAction("LoginBasic", "Auth");

    }
  }

  public IActionResult Index()
  {
    string user = HttpContext.Session.GetString("user");
      Utilisateur chauffeur = JsonSerializer.Deserialize<Utilisateur>(user);
    Console.WriteLine("user "+ user);
    Console.WriteLine("ALORS");
    /*
   */
    return View();
  }

  public IActionResult BigNotification()
  {
    return View();
  }
}
