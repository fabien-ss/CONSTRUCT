using AspnetCoreMvcFull.Models.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AspnetCoreMvcFull.Models.Documents;

public class DevisTravauxPdf : Pdf
{
  private List<Devis.DevisPdf> _devisList;

  public Devi Devi { get; set; }

  private string[] columns = new[] { "N°", "DESIGNATIONS", "U", "Q", "PU", "TOTAL" };

  public void Content()
  {
    this.Document.Add(DevisColonne());
    /*foreach (var devis in _devisList)
    {*/
    this.Document.Add(DevisTitre(_devisList[0]));
    this.Document.Add(BuildTable(_devisList[0]));
    this.Document.Add(Chunk.NEWLINE);
    this.Document.Add(DevisTotal(_devisList[0]));
    /*}*/
  }

  public void BuildPaiement()
  {
    this.Document.Add(Chunk.NEWLINE);
    this.Document.Add(new Paragraph("Paiements effectués: ", BoldFont));

    this.Document.Add(Chunk.NEWLINE);
    PdfPTable pdfPTable = GetTable(3);
    pdfPTable.WidthPercentage = 100;
    pdfPTable.AddCell("Reference");
    pdfPTable.AddCell("Date");
    pdfPTable.AddCell("Montant");

    foreach (var p in this.Devi.Paiements)
    {
      pdfPTable.AddCell(p.RefPaiement.ToString());
      pdfPTable.AddCell(p.DatePaiement.ToString());
      PdfPCell pdfPCell = new PdfPCell(new Paragraph(p.Montant.ToString("N")));
      pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
      pdfPTable.AddCell(pdfPCell);
    }

    pdfPTable.AddCell("");
    pdfPTable.AddCell(new Paragraph("TOTAL", BoldFont));
    PdfPCell pdfPCell1 = new PdfPCell(new Paragraph(this.Devi.Paiements.Sum(d => d.Montant).ToString("N")));
    pdfPCell1.HorizontalAlignment = Element.ALIGN_RIGHT;
    pdfPTable.AddCell(pdfPCell1);

    this.Document.Add(pdfPTable);
  }

  public PdfPTable DevisColonne()
  {
    PdfPTable pdfPTable = GetTable(this.columns.Length);
    pdfPTable.WidthPercentage = 100;
    pdfPTable.SetWidths(new float[] { 1, 2, 1, 1, 1, (float)1.2 });
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
    pdfPTable.SetWidths(new float[] { 1, 6 });
    pdfPTable.AddCell(new Paragraph(devis.Code, BoldFont));
    pdfPTable.AddCell(new Paragraph(devis.Titre, BoldFont));
    return pdfPTable;
  }

  public PdfPTable DevisTotal(Devis.DevisPdf devis)
  {
    PdfPTable pdfPTable = GetTable(2);
    pdfPTable.WidthPercentage = 100;
    pdfPTable.SetWidths(new float[] { (float)5, (float)1.5 });
    pdfPTable.AddCell("Total " + devis.Titre);
    // PdfPCell pdfPCell = new
    PdfPCell code = new PdfPCell(new Phrase(devis.SommePrixPrestation().ToString("N")));
    code.HorizontalAlignment = Element.ALIGN_RIGHT;
    pdfPTable.AddCell(code);
    return pdfPTable;
  }

  public PdfPTable BuildTable(Devis.DevisPdf devis)
  {
    PdfPTable pdfPTable = GetTable(columns.Length);
    pdfPTable.WidthPercentage = 100;
    pdfPTable.DefaultCell.PaddingBottom = 10;
    pdfPTable.DefaultCell.PaddingTop = 10;

    pdfPTable.SetWidths(new float[] { 1, 2, 1, 1, 1, (float)1.2 });
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
