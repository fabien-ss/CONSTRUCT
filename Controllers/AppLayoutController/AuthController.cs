using System.Diagnostics;
using System.Text.Json;
using AspnetCoreMvcFull.Context;
using AspnetCoreMvcFull.Entities;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Models.DTO;

namespace AspnetCoreMvcFull.Controllers;

public class AuthController : Controller, MethodController
{
  public AuthController(Prom13 prom13)
  {
    this.Prom13 = prom13;
  }

  private Prom13 Prom13;
  public IActionResult ForgotPasswordBasic() => View();
  public IActionResult LoginBasic(LoginModel loginModel)
  {
    if (ModelState.IsValid)
    {
      try
      {
        Chauffeur user = (Chauffeur) loginModel.mapDtoToEntity();
        user = user.getChauffeur(prom13: this.Prom13);
        HttpContext.Session.SetString("user", JsonSerializer.Serialize(user));
        return Redirect("Dashboards/Index");
      }
      catch (Exception e)
      {
        ModelState.AddModelError("Error", e.Message);
      }
    }
    return View(loginModel);
  }

  public IActionResult RegisterBasic(RegisterModel registerModel)
  {
    if (ModelState.IsValid)
    {
      var user = registerModel.mapDtoToEntity();TempData["user"] = user;
      HttpContext.Session.SetString("user", JsonSerializer.Serialize(user));
      return Redirect("Dashboards/Index");
    }
    return View(registerModel);
  }

  public IActionResult Form()
  {
    throw new NotImplementedException();
  }
}
