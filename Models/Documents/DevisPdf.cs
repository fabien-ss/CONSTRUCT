using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AspnetCoreMvcFull.Models.Documents;

public class TravauxDevis : Pdf
{
  private List<Devis.DevisPdf> _devisList;

  private string[] columns = new[] { "N°", "DESIGNATIONS", "U", "Q", "PU", "TOTAL" };

  public void Content()
  {
    this.Document.Add(DevisColonne());
    foreach (var devis in _devisList)
    {
      this.Document.Add(DevisTitre(devis));
      this.Document.Add(BuildTable(devis));
      this.Document.Add(DevisTotal(devis));
    }
  }
  public PdfPTable DevisColonne()
  {
    PdfPTable pdfPTable = GetTable(this.columns.Length);
    pdfPTable.WidthPercentage = 100;
    pdfPTable.SetWidths(new float[] { 1, 2, 1, 1, 1, 1});
    foreach (var c in columns)
    {
      PdfPCell cell = new PdfPCell(new Phrase(c.ToUpper(), BoldFont));
      cell.HorizontalAlignment = Element.ALIGN_CENTER;
      pdfPTable.AddCell(cell);
    }
    return pdfPTable;
  }
  public PdfPTable DevisTitre(Devis.DevisPdf devis)
  {
    PdfPTable pdfPTable = GetTable(2);
    pdfPTable.WidthPercentage = 100;
    pdfPTable.SetWidths(new float[]{ 1, 6});
    pdfPTable.AddCell(new Paragraph(devis.Code, BoldFont));
    pdfPTable.AddCell(new Paragraph(devis.Titre, BoldFont));
    return pdfPTable;
  }

  public PdfPTable DevisTotal(Devis.DevisPdf devis)
  {
    PdfPTable pdfPTable = GetTable(2);
    pdfPTable.WidthPercentage = 100;
    pdfPTable.SetWidths(new float[]{ 6, 1});
    pdfPTable.AddCell("Total " + devis.Titre);
    pdfPTable.AddCell(devis.SommePrixPrestation().ToString());
    return pdfPTable;
  }

  public PdfPTable BuildTable(Devis.DevisPdf devis)
  {
    PdfPTable pdfPTable = GetTable(columns.Length);
    pdfPTable.WidthPercentage = 100;
    pdfPTable.DefaultCell.PaddingBottom = 10;
    pdfPTable.DefaultCell.PaddingTop = 10;

    pdfPTable.SetWidths(new float[] { 1, 2, 1, 1, 1, 1});
    // pour chaque prestations
    foreach (var p in devis.Prestations)
    {
      pdfPTable.AddCell(p.GetCellCode());
      pdfPTable.AddCell(p.GetCellDesignation());
      pdfPTable.AddCell(p.GetCellUnite());
      pdfPTable.AddCell(p.GetCellQuantite());
      pdfPTable.AddCell(p.GetCellPrixUnitaire());
      pdfPTable.AddCell(p.GetCellTotal());
    }

    return pdfPTable;
  }

  public string[] Columns
  {
    get => columns;
    set => columns = value ?? throw new ArgumentNullException(nameof(value));
  }

  public List<Devis.DevisPdf> DevisList
  {
    get => _devisList;
    set => _devisList = value ?? throw new ArgumentNullException(nameof(value));
  }

  public string Title1
  {
    get => title;
    set => title = value ?? throw new ArgumentNullException(nameof(value));
  }
}
