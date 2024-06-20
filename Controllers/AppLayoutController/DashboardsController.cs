using System.Diagnostics;
using System.Text.Json;
using AspnetCoreMvcFull.Models.Entities;
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

  public bool checkUserSession()
  {
    try
    {
      return true;
    }
    catch (Exception e)
    {
      return false;
    }
  }

  public IActionResult Index()
  {
    if (checkUserSession())
    {
      string user = HttpContext.Session.GetString("user");
      Utilisateur chauffeur = JsonSerializer.Deserialize<Utilisateur>(user);
      return View();
    }
    else
    {
      return Redirect("Auth/LoginBasic");
    }
  }

  public IActionResult BigNotification()
  {
    return View();
  }
}
