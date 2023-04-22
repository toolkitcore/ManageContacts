using System.Net;
using Newtonsoft.Json;

namespace ManageContacts.Model.Abstractions;

public class BaseResponseModel
{
    [JsonProperty("code")]
    public HttpStatusCode StatusCode { get; set; }
    
    [JsonProperty("error")]
    public string ErrorMessage { get; set; }
    
    [JsonProperty("message")]
    public string Message { get; set; }
    
    [JsonProperty("status")]
    public bool Status => string.IsNullOrEmpty(ErrorMessage);
    
    public BaseResponseModel()
    {
        StatusCode = HttpStatusCode.OK;
    }
    public BaseResponseModel(string message)
    {
        Message = message;
        StatusCode = HttpStatusCode.OK;
    }

    public BaseResponseModel(HttpStatusCode httpStatusCode, string errorMessage)
    {
        ErrorMessage = errorMessage;
        StatusCode = httpStatusCode;
    }
}