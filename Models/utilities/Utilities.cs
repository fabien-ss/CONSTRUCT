namespace AspnetCoreMvcFull.Models.utilities;

public class Utilities
{
  public static string Format(decimal? number)
  {
    if (number == null) return "0";
    return ( (decimal) number).ToString("N");
  }
  public static string Format(double? number)
  {
    if (number == null) return "0";
    return ( (double) number).ToString("N");
  }
}
