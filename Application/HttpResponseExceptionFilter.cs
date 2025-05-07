using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Goibhniu.Application;

/// <summary>
/// A filter that handles <see cref="HttpResponseException"/>s during action execution in ASP.NET Core MVC.
/// </summary>
/// <remarks>
/// This filter intercepts instances of <see cref="HttpResponseException"/> thrown during the action execution pipeline.
/// When caught, it returns the <see cref="IActionResult"/> held by the exception and prevents it from propagating
/// further. This allows controllers to throw specific exceptions with pre-defined response results instead of manually
/// setting responses.
/// </remarks>
public class HttpResponseExceptionFilter : IActionFilter
{
  /// <inheritdoc />
  public void OnActionExecuting(ActionExecutingContext context) { }

  /// <inheritdoc />
  public void OnActionExecuted(ActionExecutedContext context)
  {
    if (context.Exception is not HttpResponseException exception) return;
    context.Result = exception.Result;
    context.ExceptionHandled = true;
  }
}