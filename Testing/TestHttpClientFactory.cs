using System.Net;
using System.Text;
using System.Text.Json;

namespace Goibhniu.Testing;

/// <summary>
/// A factory class for creating customized instances of <see cref="HttpClient"/> for testing purposes.
/// Supports configurable HTTP response simulation and exception injection for unit testing scenarios.
/// <remarks>
/// Test HTTP clients create their response the moment it is requested from the current data in the factory. This means
/// existing clients change their behavior when the factory settings are changed. 
/// </remarks>
/// <example>
/// Create an instance of the TestHttpClientFactory
/// <code>var factory = new TestHttpClientFactory();</code>
/// Create an HttpClient instance
/// <code>var client = factory.CreateClient("exampleClient");</code> 
/// Set a mocked HTTP response
/// <code>factory.SetResponse(new { Message = "Hello, World!" }, HttpStatusCode.OK, "application/json");</code>
/// Optionally set an exception to simulate failure scenarios
/// <code>factory.Exception = new InvalidOperationException("An error occurred!");</code>
/// </example>
/// </summary>
internal class TestHttpClientFactory : IHttpClientFactory, IDisposable
{
  /// <summary>The message used when an operation is canceled via the cancellation token.</summary>
  public const string CancellationMessage = "you shall not pass! 🧙‍♂️";

  /// <summary>
  /// The exception to be thrown during HTTP requests. If not null, all HTTP requests will throw this exception.
  /// </summary>
  public Exception? Exception { get; set; }

  private readonly List<WeakReference<HttpClient>> createdClients = [];
  private HttpResponseMessage? response;

  /// <inheritdoc />
  public HttpClient CreateClient(string name)
  {
    var client = new HttpClient(new DynamicResponseHandler(this), disposeHandler: false)
    {
      DefaultRequestVersion = HttpVersion.Version20,
      DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
      MaxResponseContentBufferSize = 64 * 1024,
      Timeout = TimeSpan.FromSeconds(30),
    };

    createdClients.Add(new WeakReference<HttpClient>(client));
    return client;
  }

  /// <summary>
  /// Sets the response returned by the HTTP clients from this factory.
  /// </summary>
  /// <param name="content">The response content object. If null, no content will be set.</param>
  /// <param name="statusCode">The HTTP status code to return.</param>
  /// <param name="mediaType">The content type header.</param>
  /// <remarks>This is ignored (and not returned by the client) when <see cref="Exception"/> is set.</remarks>
  public void SetResponse(
    object? content,
    HttpStatusCode statusCode = HttpStatusCode.OK,
    string mediaType = "application/json")
  {
    response = new HttpResponseMessage
    {
      StatusCode = statusCode,
      Content = content != null
        ? new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, mediaType)
        : null,
    };
  }

  /// <inheritdoc />
  public void Dispose()
  {
    foreach (var client in createdClients)
    {
      if (client.TryGetTarget(out var target))
        target.Dispose();
    }

    GC.SuppressFinalize(this);
  }

  /// <summary>
  /// Internal response handler used by the clients to create responses based on the data set in the factory.
  /// </summary>
  private class DynamicResponseHandler : HttpMessageHandler
  {
    private readonly TestHttpClientFactory factory;

    public DynamicResponseHandler(TestHttpClientFactory factory)
    {
      this.factory = factory;
    }

    /// <inheritdoc />
    protected override async Task<HttpResponseMessage> SendAsync(
      HttpRequestMessage request,
      CancellationToken cancellationToken)
    {
      // Simulate async response
      await Task.Yield();

      if (cancellationToken.IsCancellationRequested)
        throw new OperationCanceledException(CancellationMessage);
      if (factory.Exception is { } exception)
        throw exception;
      return factory.response
             ?? throw new InvalidOperationException($"No response was set in {nameof(TestHttpClientFactory)}");
    }
  }
}