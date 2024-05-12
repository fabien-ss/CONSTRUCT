using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcFull.Controllers;

public class CsvController : Controller, MethodController
{
  private readonly IWebHostEnvironment _hostEnvironment;

  public CsvController(IWebHostEnvironment hostingEnvironment)
  {
    this._hostEnvironment = hostingEnvironment;
  }

  [HttpGet]
  public IActionResult Index()
  {
    return View();
  }

  [HttpPost]
  public IActionResult Index(CsvDto csvDto)
  {
    if (ModelState.IsValid)
    {
      csvDto.saveFile(_hostEnvironment.WebRootPath);
    }

    ViewBag.title = "Formulaire csv";
    return View(csvDto);
  }

  public IActionResult Form(object o)
  {
    throw new NotImplementedException();
  }

  public IActionResult UploadFiles(List<IFormFile> files)
  {
    return null;
  }
}
