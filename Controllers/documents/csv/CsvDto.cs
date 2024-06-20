using System.Globalization;
using System.Text.RegularExpressions;
using AspnetCoreMvcFull.Context;
using AspnetCoreMvcFull.Models.maisonTravaux;
using CsvHelper.Configuration;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using NuGet.Protocol;
using CsvReader = CsvHelper.CsvReader;

namespace AspnetCoreMvcFull.Controllers;

public class CsvDto
{
  private IFormFile csv;
  public IFormFile Csv
  {
    get => csv;
    set => csv = value ?? throw new ArgumentNullException(nameof(value));
  }

  public void saveFile(string filePath, string fileName)
  {
    Console.WriteLine(filePath);
    if (!this.Csv.FileName.Contains(".csv")) throw new Exception("Not a valid csv file.");
    if (this.Csv != null && this.Csv.Length > 0 && this.Csv.FileName.Contains(".csv"))
    {
      // uploads/documents/csv/evl.csv
      var path = Path.Combine(filePath, "uploads/documents/csv/", fileName);
      using (var stream = System.IO.File.Create(path))
      {
        this.Csv.CopyTo(stream);
      }
    }
  }

  public void saveEntities(string rootPath, ConstructionDb constructionDb)
  {
    if (!this.Csv.FileName.Contains(".csv")) throw new Exception("Not a valid csv file.");
    var path = Path.Combine(rootPath, "uploads/documents/csv/", "maison.csv");
    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
      Delimiter = ",",
      HeaderValidated = null,
      MissingFieldFound = null
    };
    using (var reader = new StreamReader(path))
    using (var csv = new CsvReader(reader, config))
    {
      var records = csv.GetRecords<MaisonTravaux>().ToList();
      foreach (var VARIABLE in records)
      {
        if (VARIABLE.Surface >= 0)
        {
          if (VARIABLE.PrixUnitaire >= 0)
          {
            if (VARIABLE.Quantite >= 0)
            {
              if (VARIABLE.DureeTravaux >= 0)
              {
                constructionDb.MaisonTravauxes.Add(VARIABLE);
              }
            }
          }

        }
      }
      constructionDb.SaveChanges();
    }
  }

  public void saveDevis(string rootPath, ConstructionDb constructionDb)
  {
    var path = Path.Combine(rootPath, "uploads/documents/csv/", "devis.csv");
    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
      Delimiter = ",",
      HeaderValidated = null,
      MissingFieldFound = null
    };
    using (var reader = new StreamReader(path))
    using (var csv = new CsvReader(reader, config))
    {
      var records = csv.GetRecords<DevisTemp>().ToList();
      foreach (var VARIABLE in records)
      {
       // VARIABLE.taux_finition = VARIABLE.taux_finition.Replace("%", "").Replace(",",".");
        //Console.WriteLine(VARIABLE.ref_devis);
        string dateString = VARIABLE.DateDebut;
        string date2 = VARIABLE.DateDevis;
        DateTime date;
        if (DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)
            && DateTime.TryParseExact(date2, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)
            )
        {
          if (double.Parse(VARIABLE.taux_finition) >= 0)
          {
            if (EstNumeroTelephoneValide(VARIABLE.client))
            {
              Console.WriteLine(("VALIDE "+VARIABLE.Client));
            constructionDb.DevisTemps.Add(VARIABLE);
            }
            else
            {
              Console.WriteLine("PAS VALDE "+VARIABLE.Client);
            }
          }
          Console.WriteLine("Valid date: " + date.ToShortDateString());
        }
        else
        {
          Console.WriteLine("Invalid date");
        }
      }
      constructionDb.SaveChanges();
    }
  }

  static bool EstNumeroTelephoneValide(string numero)
  {
    // Expression régulière pour le format spécifié (03########)
    string pattern = @"^03\d{8}$";

    // Vérifier si le numéro de téléphone correspond au motif
    return Regex.IsMatch(numero, pattern);
  }

  public void savePaiement(string rootPath, ConstructionDb constructionDb)
  {
    if (!this.Csv.FileName.Contains(".csv")) throw new Exception("Not a valid csv file.");
    var path = Path.Combine(rootPath, "uploads/documents/csv/", "paiement.csv");
    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
      Delimiter = ",",
      HeaderValidated = null,
      MissingFieldFound = null
    };
    using (var reader = new StreamReader(path))
    using (var csv = new CsvReader(reader, config))
    {
      var records = csv.GetRecords<PaiementTemp>().ToList();
      foreach (var VARIABLE in records)
      {
        string dateString = VARIABLE.DatePaiement;
        DateTime date;
        if (DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)
           )
        {
          if (double.Parse(VARIABLE.montant) >= 0)
          {
            constructionDb.PaiementTemps.Add(VARIABLE);
          }
          Console.WriteLine("Valid date: " + date.ToShortDateString());
        }
        else
        {
          Console.WriteLine("Invalid date");
        }
        Console.WriteLine(VARIABLE.ref_devis);

      }
      constructionDb.SaveChanges();
    }
  }
}
