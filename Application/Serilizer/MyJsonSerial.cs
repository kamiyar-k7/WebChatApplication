using System.Text.Json;


namespace Application.Serilizer;

public static class MyJsonSerial
{

    public static string Serialize(object obj)
    {
         return JsonSerializer.Serialize(obj , options: new JsonSerializerOptions { WriteIndented = true});
     
    }


}
