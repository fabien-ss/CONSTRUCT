using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Controllers;

public class TablesController : Controller, MethodController
{
  public IActionResult Basic() => View();
  public IActionResult TableDark() => View();
  public IActionResult TableBasic() => View();
  public IActionResult Form()
  {
    throw new NotImplementedException();
  }
}
