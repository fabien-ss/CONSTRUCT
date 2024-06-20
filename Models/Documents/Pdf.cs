using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AspnetCoreMvcFull.Models.Documents;

public class Pdf : PdfContrainst
{
  public readonly Font BoldFont = new(Font.FontFamily.HELVETICA, 11, Font.BOLD);
  public Document Document;
  public string title = "Ceci est un Test";
  private readonly Font Titlefont = new(Font.FontFamily.HELVETICA, 21, Font.BOLD);
  private Dictionary<string, string> leftData = new Dictionary<string, string>();
  private Dictionary<string, string> rightData = new Dictionary<string, string>();
  public string Path { get; set; }

  public string ImagePath { get; set; } = "/img/favicon/logo.jpeg" ;
  private string[][] data = new string[0][];
  public Pdf()
  {
    Document = new Document();
    string name = "pdf" + DateTime.Now.Ticks / 10000 + ".pdf";
    this.Path = "/documents/pdf/" + name;
    PdfWriter.GetInstance(Document, new FileStream("./wwwroot/documents/pdf/" + name, FileMode.Create));
  }

  public void createPdf()
  {
    this.Document.Open();
    Header();
    Content();
    this.Document.CloseDocument();
  }

  public void Open()
  {
    this.Document.Open();
  }

  public void Close()
  {
    this.Document.CloseDocument();
  }

  private void Ligne(Paragraph contenu, int nombre)
  {
    for (var i = 0; i < nombre; i++) contenu.Add(Chunk.NEWLINE);
  }

  public void Header()
  {
    var title = new Paragraph(Title, Titlefont);
    title.Alignment = Element.ALIGN_CENTER;
    Ligne(title, 2);
    Document.Add(title);
    var table = GetTable(2);
    //table.TotalWidth = Document.PageSize.Width; //Document.PageSize.Width - Document.LeftMargin;
    table.DefaultCell.Border = Rectangle.NO_BORDER;
      table.WidthPercentage = 100;

    var paragraphLeft = BuildParagraph(this.leftData, 1);
    //var paragraphLeft = new Jpeg();
    //paragraphLeft.SpacingBefore = 20f;
    //var paragraphLeft = new Jpeg(new Uri(this.ImagePath));
    table.AddCell(paragraphLeft); // 1 colonne = 1 paragraph

    var paragraphRight = BuildParagraph(this.rightData, 1);
    //paragraphRight.SpacingBefore = 20f;
    table.AddCell(paragraphRight);
    Document.AddAuthor("Fabien");
    Document.Add(table);
    Document.Add(Chunk.NEWLINE);
  }

  private PdfPTable BuildTable()
  {
    PdfPTable pdfPTable = GetTable(data[0].Length);
    for (int i = 0; i < data.Length; i++)
    {
      for (int j = 0; j < data[i].Length; j++)
      {
        pdfPTable.AddCell(data[i][j]);
      }
    }
    return pdfPTable;
  }

  public PdfPTable GetTable(int column)
  {
    return new PdfPTable(column);
  }

  private Paragraph BuildParagraph(Dictionary<string, string> data, int lineSeparator)
  {
    Paragraph paragraph = new Paragraph();
    foreach (var ligne in data)
    {
      paragraph.Add(new Chunk(ligne.Key, this.BoldFont));
      paragraph.Add(ligne.Value);
      Ligne(paragraph, lineSeparator);
    }

    return paragraph;
  }

  public void Footer()
  {
    throw new NotImplementedException();
  }

  public void Content()
  {
    this.Document.Add(BuildTable());
  }


  public static void Main(string[] args)
  {
    Console.WriteLine("Hello world!");
  }

  public Dictionary<string, string> LeftData
  {
    get => leftData;
    set => leftData = value ?? throw new ArgumentNullException(nameof(value));
  }

  public Dictionary<string, string> RightData
  {
    get => rightData;
    set => rightData = value ?? throw new ArgumentNullException(nameof(value));
  }

  public string[][] Data
  {
    get => data;
    set => data = value ?? throw new ArgumentNullException(nameof(value));
  }

  public string Title
  {
    get => title;
    set => title = value ?? throw new ArgumentNullException(nameof(value));
  }
}
