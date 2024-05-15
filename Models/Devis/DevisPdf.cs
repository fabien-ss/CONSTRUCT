namespace AspnetCoreMvcFull.Models.Devis;

public class DevisPdf
{
  private string code;
  private string titre;
  private List<PrestationPdf> _prestations = new List<PrestationPdf>();
  private double total;

  public DevisPdf(string code, string titre)
  {
    this.Code = code;
    this.Titre = titre;
  }

  public double SommePrixPrestation()
  {
    double somme = 0;
    foreach (var p in this.Prestations)
    {
      somme += p.Total;
    }
    return somme;
  }

  public string Code
  {
    get => code;
    set => code = value ?? throw new ArgumentNullException(nameof(value));
  }

  public string Titre
  {
    get => titre;
    set => titre = value ?? throw new ArgumentNullException(nameof(value));
  }

  public List<PrestationPdf> Prestations
  {
    get => _prestations;
    set => _prestations = value ?? throw new ArgumentNullException(nameof(value));
  }

  public double Total
  {
    get => total;
    set => total = value;
  }
}
