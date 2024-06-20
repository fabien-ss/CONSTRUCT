using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcFull.Controllers;

public class CsvController : Controller, MethodController
{

  public IActionResult Form(object o)
  {
    throw new NotImplementedException();
  }

  public IActionResult UploadFiles(List<IFormFile> files)
  {
    return null;
  }
}
