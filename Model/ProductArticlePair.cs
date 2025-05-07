namespace Goibhniu.Model;

/// <summary>
/// A pair of a <see cref="Product"/> with an associated <see cref="Article"/>.
/// </summary>
/// <remarks>
/// This structure is particularly useful when processing collections of products and their associated articles,
/// enabling operations that require both the product and its specific article context.
/// </remarks>
public readonly record struct ProductArticlePair
{
  public readonly Product Product;
  public readonly Article Article;
  
  public ProductArticlePair(Product product, Article article)
  {
    Product = product;
    Article = article;
  }

  public void Deconstruct(out Product product, out Article article)
  {
    product = Product;
    article = Article;
  }
}