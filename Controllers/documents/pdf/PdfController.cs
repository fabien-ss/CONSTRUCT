using AspnetCoreMvcFull.Models.Documents;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcFull.Controllers.pdf;

public class PdfController : Controller
{
  public PdfController()
  {

  }

  [HttpGet]
  public IActionResult Index()
  {
    Dictionary<string, string> leftData = new Dictionary<string, string>();

    leftData.Add("Nom: ", "Rakoto");
    leftData.Add("Prenom: ", "Kot");
    leftData.Add("Date de naissance: ", "12-01-23");

    string[][] data = new[]
    {
      new[] { "N°", "DESIGNATION", "U", "Q", "PU", "Total" },
      new[] { "000 - TRAVAUX PREPARATOIE", "", "", "", "" },
      new[] { "001", "mur de soutenement et demi Cloture ht 1m", "m3", "101,36", "", "1 800 000" },
      new[] { "Vary", "180 000", "10", "1 800 000" }
    };

    Pdf pdf = new Pdf();

    pdf.LeftData = leftData;
    pdf.RightData = leftData;
    pdf.Title = "PDF DE Fabien";

   // pdf.Data = data;
    pdf.Open();
    pdf.Header();
   // pdf.Content();
    pdf.Close();
    return View();
  }
}
