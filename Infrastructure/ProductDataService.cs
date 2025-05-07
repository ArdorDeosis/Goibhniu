using Goibhniu.Model;
using Goibhniu.Shared;

namespace Goibhniu.Infrastructure;

/// <inheritdoc />
internal sealed class ProductDataService : IProductDataService
{
  private readonly IHttpClientFactory httpClientFactory;
  private readonly IDeserializationStrategy deserializationStrategy;

  public ProductDataService(IHttpClientFactory httpClientFactory, IDeserializationStrategy deserializationStrategy)
  {
    this.httpClientFactory = httpClientFactory;
    this.deserializationStrategy = deserializationStrategy;
  }

  /// <inheritdoc />
  public async Task<Result<IReadOnlyCollection<Product>>> FetchProductData(string url, CancellationToken cancellationToken = default)
  {
    try
    {
      var httpClient = httpClientFactory.CreateClient();
      var response = await httpClient.GetAsync(url, cancellationToken);
      if (!response.IsSuccessStatusCode)
        return "failed to fetch data";

      await using var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
      return await deserializationStrategy.Resolve(url)(contentStream, cancellationToken);
    }
    catch (Exception exception)
    {
      return exception.Message;
    }
  }
}