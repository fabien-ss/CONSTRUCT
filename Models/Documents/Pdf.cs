using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AspnetCoreMvcFull.Models.Documents;

public class Pdf : PdfContrainst
{
  private readonly Font BoldFont = new(Font.FontFamily.HELVETICA, 11, Font.BOLD);
  private readonly Document Document;
  private string Path;
  private readonly string Title = "Ceci est un Test";
  private readonly Font Titlefont = new(Font.FontFamily.HELVETICA, 21, Font.BOLD);

  public Pdf()
  {
    Document = new Document();
    PdfWriter.GetInstance(Document, new FileStream("./wwwroot/pdf/pdf.pdf", FileMode.Create));
    Document.Open();
    Header();
    Document.CloseDocument();
  }

  private void Ligne(Paragraph contenu, int nombre)
  {
    for (var i = 0; i < nombre; i++) contenu.Add(Chunk.NEWLINE);
  }

  private void Header()
  {
    var title = new Paragraph(Title, Titlefont);
    title.Alignment = Element.ALIGN_CENTER;
    Ligne(title, 1);
    Document.Add(title);
    var table = new PdfPTable(2);
    table.TotalWidth = Document.PageSize.Width - Document.LeftMargin;
    table.DefaultCell.Border = Rectangle.NO_BORDER;
    var Contenu = new Paragraph();
    Contenu.SpacingBefore = 20f;
    Ligne(Contenu, 1);
    Contenu.Add(new Chunk("Nom : ", BoldFont));
    Contenu.Add(new Chunk("Rakoto"));
    table.AddCell(Contenu);
    var Contenu2 = new Paragraph();
    Contenu2.SpacingBefore = 20f;
    Ligne(Contenu2, 1);
    Contenu2.Add(new Chunk("Classification : ", BoldFont));
    Contenu2.Add(new Chunk("Peter"));
    table.AddCell(Contenu2);
    Document.Add(table);
  }

  public void Footer()
  {
    throw new NotImplementedException();
  }

  public void Content()
  {
    throw new NotImplementedException();
  }

  void PdfContrainst.Header()
  {
    Header();
  }

  public static void Main(string[] args)
  {
    Console.WriteLine("Hello world!");
  }
}
