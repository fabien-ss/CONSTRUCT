
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcFull.Controllers;

public class CsvController: Controller, MethodController
{
  public IActionResult Formulaire(IFormFile file)
  {
    ViewBag.title = "Formulaire csv";
    return View();
  }

  public IActionResult Form(object o)
  {
    throw new NotImplementedException();
  }
}
