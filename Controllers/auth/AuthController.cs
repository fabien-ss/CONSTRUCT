using System.Diagnostics;
using System.Security.Cryptography;
using System.Text.Json;
using AspnetCoreMvcFull.Context;
using AspnetCoreMvcFull.Entities;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Models.DTO;

namespace AspnetCoreMvcFull.Controllers;

public class AuthController : Controller, MethodController
{
  private Tsakitsaky Tsakitsaky;
  public AuthController(Tsakitsaky prom13)
  {
    this.Tsakitsaky = prom13;
  }

  public IActionResult ForgotPasswordBasic() => View();

  [HttpGet]
  public IActionResult LoginBasic()
  {
    return View();
  }

  [HttpGet]
  public IActionResult RegisterBasic()
  {
    return View();
  }
  [HttpPost]
  public IActionResult LoginBasic(LoginDto loginDto)
  {
    if (ModelState.IsValid)
    {
      try
      {
        Utilisateur user = (Utilisateur) loginDto.mapDtoToEntity();
        user = user.getChauffeur(prom13: this.Tsakitsaky);
        HttpContext.Session.SetString("user", value: JsonSerializer.Serialize(user));
        return Redirect("Dashboards/Index");
      }
      catch (Exception e)
      {
        ModelState.AddModelError("Error", e.Message);
      }
    }
    return View(loginDto);
  }

  [HttpPost]
  public IActionResult RegisterBasic(RegisterDto registerDto)
  {
    if (ModelState.IsValid)
    {
      var user = registerDto.mapDtoToEntity();
      TempData["user"] = user;
      HttpContext.Session.SetString("user", JsonSerializer.Serialize(user));
      return Redirect("Dashboards/Index");
    }
    return View(registerDto);
  }

  public IActionResult Logout()
  {
    HttpContext.Session.Clear();
    return Redirect("/Auth/LoginBasic");
  }

  public IActionResult Form(object o)
  {
    throw new NotImplementedException();
  }
}
