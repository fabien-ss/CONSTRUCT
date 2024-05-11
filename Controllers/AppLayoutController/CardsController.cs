using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Controllers;

public class CardsController : Controller, MethodController
{
  public IActionResult Basic() => View();

  public IActionResult Form(object o)
  {
    throw new NotImplementedException();
  }
}
