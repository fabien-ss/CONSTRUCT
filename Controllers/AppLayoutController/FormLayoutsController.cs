using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Controllers;

public class FormLayoutsController : Controller, MethodController
{
  private readonly IWebHostEnvironment _hostingEnvironment;

  public FormLayoutsController(IWebHostEnvironment hostingEnvironment)
  {
    this._hostingEnvironment = hostingEnvironment;
  }

  public IActionResult Horizontal() => View();
  public IActionResult Vertical() => View();
  public IActionResult Form(object o) => View();
  public IActionResult Upload() => View();

  public IActionResult UploadFile(IFormFile file)
  {
    if (file != null && file.Length > 0)
    {
      // Specify the path to save the file
      var path = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", file.FileName);
      using (var stream = System.IO.File.Create(path))
      {
        file.CopyTo(stream);
      }
    }

    // Redirect to a view or return a result
    return RedirectToAction("Upload");
  }

  [HttpPost]
  public async Task<IActionResult> UploadFiles(List<IFormFile> files)
  {
    foreach (var file in files)
    {
      Console.WriteLine(file.FileName);
      if (file != null && file.Length > 0)
      {
        var path = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", file.FileName);
        using (var stream = System.IO.File.Create(path))
        {
          await file.CopyToAsync(stream);
        }
      }
    }

    return RedirectToAction("Upload");
  }
}
