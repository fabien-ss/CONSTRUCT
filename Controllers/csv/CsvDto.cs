namespace AspnetCoreMvcFull.Controllers;

public class CsvDto
{
  private IFormFile csv;
  private string targetTable;

  public IFormFile Csv
  {
    get => csv;
    set => csv = value ?? throw new ArgumentNullException(nameof(value));
  }

  public string TargetTable
  {
    get => targetTable;
    set => targetTable = value ?? throw new ArgumentNullException(nameof(value));
  }

  public CsvDto(IFormFile csv, string targetTable)
  {
    this.csv = csv;
    this.targetTable = targetTable;
  }


  public void saveFile(string filePath)
  {
    if (this.Csv != null && this.Csv.Length > 0 && this.Csv.FileName.Contains(".csv"))
    {
      var path = Path.Combine(filePath, "uploads/documents/csv/", this.Csv.FileName);
      using (var stream = System.IO.File.Create(path))
      {
        this.Csv.CopyTo(stream);
      }
    }
  }
}
