using Goibhniu.Model;
using Goibhniu.Shared;

namespace Goibhniu.Analysis;

/// <inheritdoc />
// ---
// NOTE: all cases of multiple enumerations in this class have been considered and deemed ok.
internal sealed class ProductAnalysisService : IProductAnalysisService
{
  private const string CollectionEmptyMessage = "product list is empty";
  
  /// <inheritdoc />
  public Result<PriceExtremesData> GetPriceExtremes(IEnumerable<Product> products) => 
    products.Any() ? products.FindPriceExtremes() : CollectionEmptyMessage;

  /// <inheritdoc />
  public Result<IEnumerable<ProductArticlePair>> GetProductsWithPrice(IEnumerable<Product> products, decimal price) => 
    products.WithPrice(price).ToSuccess();

  /// <inheritdoc />
  public Result<ProductArticlePair> GetProductWithMostBottles(IEnumerable<Product> products) => 
    products.Any() ? products.WithMostBottles() : CollectionEmptyMessage;
}