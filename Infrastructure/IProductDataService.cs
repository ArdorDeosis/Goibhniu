using Goibhniu.Model;
using Goibhniu.Shared;

namespace Goibhniu.Infrastructure;

/// <summary>
/// A service providing product data from a URL.
/// </summary>
public interface IProductDataService
{
  /// <summary>
  /// Fetches product data from the specified URL and deserializes it into a collection of products.
  /// </summary>
  /// <param name="url">The URL from which to fetch product data.</param>
  /// <param name="cancellationToken">A token to signal cancellation of the operation.</param>
  /// <returns>A collection of products.</returns>
  Task<Result<IReadOnlyCollection<Product>>> FetchProductData(string url, CancellationToken cancellationToken = default);
}