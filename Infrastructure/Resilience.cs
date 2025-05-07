using Polly;
using Polly.Extensions.Http;

namespace Goibhniu.Infrastructure;

/// <summary>
/// Provides resilience policies to handle HTTP calls in the application.
/// </summary>
internal static class Resilience
{
  /// <summary>Default resilience policy</summary>
  // ---
  // NOTE: I really do not have any experience with Polly, so this might be crap
  public static IAsyncPolicy<HttpResponseMessage> DefaultPolicy { get; } = HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(3, retryAttempt => Math.Pow(2, retryAttempt) * TimeSpan.FromMilliseconds(500),
      onRetry: (outcome, timespan, retryAttempt, context) =>
      {
        // TODO: log, maybe?
      });
}