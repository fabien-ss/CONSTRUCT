using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AspnetCoreMvcFull.Models.Devis;

public class PrestationPdf
{
  private string code;
  private string designation;
  private string unite;
  private double quantite;
  private double prixUnitaire;
  private double total;

  public PrestationPdf(string code, string designation, string unite, double quantite, double prixUnitaire)
  {
    this.Code = code;
    this.Designation = designation;
    this.Unite = unite;
    this.Quantite = quantite;
    this.PrixUnitaire = prixUnitaire;
  }

  public string Code
  {
    get => code;
    set => code = value ?? throw new ArgumentNullException(nameof(value));
  }

  public string Designation
  {
    get => designation;
    set => designation = value ?? throw new ArgumentNullException(nameof(value));
  }

  public string Unite
  {
    get => unite;
    set => unite = value ?? throw new ArgumentNullException(nameof(value));
  }

  public double Quantite
  {
    get => quantite;
    set => quantite = value;
  }

  public double PrixUnitaire
  {
    get => prixUnitaire;
    set => prixUnitaire = value;
  }

  public double Total
  {
    get => PrixUnitaire * Quantite;
    set => total = value;
  }

  public PdfPCell GetCellCode()
  {
    PdfPCell code = new PdfPCell(new Phrase(this.Code));
    code.HorizontalAlignment = Element.ALIGN_CENTER;
    code.PaddingBottom = 10;
    code.PaddingTop = 10;
     return code;
  }

  public PdfPCell GetCellDesignation()
  {
    PdfPCell code = new PdfPCell(new Phrase(this.Designation));
    code.HorizontalAlignment = Element.ALIGN_LEFT;
    return code;
  }
  public PdfPCell GetCellUnite()
  {
    PdfPCell code = new PdfPCell(new Phrase(this.unite));
    code.HorizontalAlignment = Element.ALIGN_CENTER;
    return code;
  }

  public PdfPCell GetCellQuantite()
  {
    PdfPCell code = new PdfPCell(new Phrase(this.quantite.ToString("N")));
    code.HorizontalAlignment = Element.ALIGN_RIGHT;
    return code;
  }

  public PdfPCell GetCellPrixUnitaire()
  {
    PdfPCell code = new PdfPCell(new Phrase(this.prixUnitaire.ToString("N")));
    code.HorizontalAlignment = Element.ALIGN_RIGHT;
    return code;
  }

  public PdfPCell GetCellTotal()
  {
    PdfPCell code = new PdfPCell(new Phrase(this.Total.ToString("N")));
    code.HorizontalAlignment = Element.ALIGN_RIGHT;
    return code;
  }
}
