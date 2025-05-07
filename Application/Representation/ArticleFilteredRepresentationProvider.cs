using Goibhniu.Model;

namespace Goibhniu.Application;

/// <summary>
/// An <see cref="IRepresentationProvider"/> that returns <see cref="Product"/>s, but removes unrelated
/// <see cref="Article"/> from them.
/// </summary>
internal sealed class ArticleFilteredRepresentationProvider : ProductRepresentationProvider
{
  /// <inheritdoc />
  protected override object MakeRepresentation(ProductArticlePair data) => data.Product with{Articles = [data.Article]};

  /// <inheritdoc />
  protected override object MakeRepresentation(IEnumerable<ProductArticlePair> data) => data
      .GroupBy(datum => datum.Product, datum => datum.Article)
      .Select(group => group.Key with {Articles = group.ToArray()});
}