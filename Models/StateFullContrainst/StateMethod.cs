using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;

namespace AspnetCoreMvcFull.Models.StateFullContrainst;

public class StateMethod
{
  public object Deserializer(string json)
  {
    try
    {
      return JsonSerializer.Deserialize<object>(json);
    }
    catch (Exception e)
    {
      throw new Exception(e.Message + "Error rebuilding object");
    }
  }

  public string Serializer()
  {
    try
    {
      return JsonSerializer.Serialize(this);
    }
    catch (Exception e)
    {
      throw new Exception(e.Message + "Error parsing json");
    }
  }
}
