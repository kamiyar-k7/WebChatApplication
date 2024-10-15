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
            if (ex.Response.Contains("propertyname"))
            {
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(ex.Response)))
                {

                    var errorsList = await JsonSerializer.DeserializeAsync<List<Dictionary<string, object>>>(stream);


                    foreach (var error in errorsList)
                    {
                        if (error.TryGetValue("propertyname", out var propertyName) &&
                            error.TryGetValue("errors", out var errorMessages))
                        {
                            DictioneryErrors[propertyName.ToString()] = JsonSerializer.Deserialize<List<string>>(errorMessages.ToString());
                        }
                    }
                    return DictioneryErrors; // Return populated dictionary

                }
            }
            else
            {
                return new ExeptionDto
                {
                    Message = ex.Response,
                    StatusCode = ex.StatusCode
                };

            }
                  
                
            
        }
        catch 
        {
            
            return new ExeptionDto
            {
                Message = "Received an invalid response from the server.",
                StatusCode = ex.StatusCode
            };
        }


       
    }
}


public  class ExeptionDto
{
    public  string Message { get; set; }
    public  int StatusCode { get; set; }
}

