using Newtonsoft.Json;

namespace TaskManagement.Api.Errors;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}

