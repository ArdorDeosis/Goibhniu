using Goibhniu.Model;
using Goibhniu.Shared;

namespace Goibhniu.Analysis;

/// <summary>
/// Provides raw, stateless analysis methods on collections of products.
/// </summary>
internal static class ProductAnalysis
{
  /// <summary>
  /// Finds the products with the lowest and highest prices from a collection of products.
  /// </summary>
  /// <param name="products">The collection of products to analyze.</param>
  /// <returns>A tuple containing the cheapest and most expensive product-article pairs.</returns>
  public static PriceExtremesData FindPriceExtremes(this IEnumerable<Product> products) =>
    products.ProductArticlePairs()
      .Where(data => data.Article.PricePerUnit.HasValue)
      .MinMaxBy(data => data.Article.PricePerUnit!.Value);

  /// <summary>
  /// Filters a collection of products to find all product-article pairs with a specific price.
  /// </summary>
  /// <param name="products">The collection of products to analyze.</param>
  /// <param name="price">The price to filter the products by.</param>
  /// <returns>
  /// A collection of product-article pairs where the article's price matches the specified price, ordered by price per
  /// unit.
  /// </returns>
  public static IEnumerable<ProductArticlePair> WithPrice(this IEnumerable<Product> products, decimal price) => products
    .ProductArticlePairs()
    .Where(data => data.Article.Price == price)
    .OrderBy(data => data.Article.PricePerUnit)
    .ToList();

  /// <summary>
  /// Identifies the product-article pair with the highest number of bottles from a collection of products.
  /// </summary>
  /// <param name="products">The collection of products to evaluate.</param>
  /// <returns>
  /// The product-article pair with the most bottles.
  /// </returns>
  // ---
  // NOTE: I decided to only return a single item, since the specification states "Which _one_ product [...]".
  public static ProductArticlePair WithMostBottles(this IEnumerable<Product> products) =>
    products.ProductArticlePairs().MaxBy(data => data.Article.ItemsInFrame);
}