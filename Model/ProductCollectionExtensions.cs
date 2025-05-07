namespace Goibhniu.Model;

public static class ProductCollectionExtensions
{
  /// <summary>
  /// Retrieves a sequence of product and article pairs from the provided collection of products. Each pair consists of
  /// a product and one of its associated articles.<br/> 
  /// </summary>
  /// <param name="products">The collection of products from which to generate the product and article pairs.</param>
  /// <returns>An IEnumerable containing pairs of products and their associated articles.</returns>
  /// <remarks>
  /// This view is helpful since most operations are performed on <see cref="Article">articles</see>, but they do not
  /// have a back-reference to their product.
  /// </remarks>
  public static IEnumerable<ProductArticlePair> ProductArticlePairs(this IEnumerable<Product> products) =>
    from product in products
    from article in product.Articles
    select new ProductArticlePair(product, article);
}