using System.Diagnostics;
using System.Security.Cryptography;
using System.Text.Json;
using AspnetCoreMvcFull.Context;
using AspnetCoreMvcFull.Controllers.auth;
using AspnetCoreMvcFull.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Models.DTO;

namespace AspnetCoreMvcFull.Controllers;

public class AuthController : Controller, MethodController
{
  private ConstructionDb ConstructionDb;
  public AuthController(ConstructionDb prom13)
  {
    this.ConstructionDb = prom13;
  }

  public IActionResult ForgotPasswordBasic() => View();

  [HttpGet]
  public IActionResult LoginAdmin()
  {
    return View();
  }

  [HttpPost]
  public IActionResult LoginAdmin(LoginAdminDto loginAdminDto)
  {
    if (ModelState.IsValid)
    {
      try
      {
        Utilisateur utilisateur = loginAdminDto.mapDtoToEntity();
        utilisateur = utilisateur.findAdmin(ConstructionDb);
        HttpContext.Session.SetString("user", value: JsonSerializer.Serialize(utilisateur));
        return Redirect("/admindashboard/Index");
      }
      catch (Exception e)
      {
        ModelState.AddModelError("error", e.Message);
      }
    }
    return View();
  }

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
        user = user.getOrCreateUser(prom13: this.ConstructionDb);
        HttpContext.Session.SetString("user", value: JsonSerializer.Serialize(user));
        return Redirect("/Devis/mesDevis");
      }
      catch (Exception e)
      {
        ModelState.AddModelError("Error", "not a valid input");
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
