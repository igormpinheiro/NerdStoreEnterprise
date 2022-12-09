using System.Net;

namespace NSE.WebApp.MVC.Extensions;

public class CustomHttpRequestException : Exception
{
    public HttpStatusCode StatusCode { get; set; }

    public CustomHttpRequestException()
    {
    }

    public CustomHttpRequestException(string message, Exception innerEx) : base(message, innerEx)
    {
    }

    public CustomHttpRequestException(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }
}