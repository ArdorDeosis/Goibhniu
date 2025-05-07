using Microsoft.AspNetCore.Mvc;

namespace Goibhniu.Application;

/// <summary>
/// An Exception that contains an HTTP response result to be returned from the controller.
/// </summary>
public sealed class HttpResponseException : Exception
{
  public IActionResult Result { get; }

  public HttpResponseException(IActionResult result)
  {
    Result = result;
  }
  
  public HttpResponseException(string message)
  {
    Result = new ObjectResult(message) { StatusCode = 500 };
  }
}