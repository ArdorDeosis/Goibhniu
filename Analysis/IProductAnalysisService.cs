using Goibhniu.Model;
using Goibhniu.Shared;

namespace Goibhniu.Analysis;

/// <summary>
/// Defines a contract for analyzing collections of products.
/// </summary>
/// <remarks>This basically wraps <see cref="ProductAnalysis"/> and adds data validation and helpful errors.</remarks>
public interface IProductAnalysisService
{
  /// <summary>
  /// Determines the products with the lowest and highest prices from a given collection of products.
  /// </summary>
  /// <param name="products">A collection of products to analyze.</param>
  /// <returns>The cheapest and most expensive products, or an error, if the operation fails</returns>
  Result<PriceExtremesData> GetPriceExtremes(IEnumerable<Product> products);

  /// <summary>
  /// Retrieves products and their associated articles that match the specified price.
  /// </summary>
  /// <param name="products">A collection of products to search through.</param>
  /// <param name="price">The price to filter the products by.</param>
  /// <returns>A collection of products that match the specified price, or an error, if the operation fails</returns>
  // ---
  // NOTE: Right now the implementations of this operation cannot fail, but the operation returns a Result for
  // maintainability, maybe future implementations can fail. 
  Result<IEnumerable<ProductArticlePair>> GetProductsWithPrice(IEnumerable<Product> products, decimal price);

  /// <summary>
  /// Identifies the product that contains the highest number of bottles from a given collection of products.
  /// </summary>
  /// <param name="products">A collection of products to analyze.</param>
  /// <returns>A single product with the most bottles, or an error, if the operation fails.</returns>
  Result<ProductArticlePair> GetProductWithMostBottles(IEnumerable<Product> products);
}