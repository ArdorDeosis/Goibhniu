using Goibhniu.Model;

namespace Goibhniu.Application;

/// <summary>
/// An <see cref="IRepresentationProvider"/> that returns raw <see cref="Product"/>s incl. all their
/// <see cref="Article"/>s.
/// </summary>
internal sealed class RawProductRepresentationProvider : ProductRepresentationProvider
{
  /// <inheritdoc />
  protected override object MakeRepresentation(ProductArticlePair data) => data.Product;

  /// <inheritdoc />
  protected override object MakeRepresentation(IEnumerable<ProductArticlePair> data) => 
    data.Select(datum => datum.Product).Distinct();
}