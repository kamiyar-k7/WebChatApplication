using System.Text;
using System.Text.Json;
using WebChatBlazor.Services.Base;


namespace WebChatBlazor.Services.ExHandler;


public static class ExceptionHandler
{
    private static Dictionary<string, List<string>> DictioneryErrors { get; } = new Dictionary<string, List<string>>();
    public static async Task<object> HandleApiException(ApiException ex)
    {

        try
        {
            var errors = JsonSerializer.Deserialize<JsonElement>(ex.Response);
            DictioneryErrors.Clear();
            if (errors.ValueKind == JsonValueKind.Array ) 
            {
                foreach (var error in errors.EnumerateArray())
                {

                    if (error.TryGetProperty("propertyname", out var propertyName) &&
                        error.TryGetProperty("errors", out var errorMessages) &&
                            errorMessages.ValueKind == JsonValueKind.Array)
                    {
                        var errorlist = JsonSerializer.Deserialize<List<string>>(errorMessages.GetRawText());
                        DictioneryErrors[propertyName.GetString() ?? ""] = errorlist;
                    }
                }
                return DictioneryErrors; 

            }
            else
            {
                return new ExeptionDto
                {
                    Message = ex.Response,
                    StatusCode = ex.StatusCode,
                };
            }
       
          
        }
        catch
        {
            return new ExeptionDto
            {
                Message = ex.Response,
                StatusCode = ex.StatusCode,
            };
        }
       
 
    }

}
public class ExeptionDto
{
    public string Message { get; set; }
    public int StatusCode { get; set; }
}


