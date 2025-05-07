using Goibhniu.Model;

namespace Goibhniu.Testing;

/// <summary>
/// Equality comparer for products for TESTING PURPOSES ONLY.
/// </summary>
internal class ProductEqualityComparer : IEqualityComparer<Product>
{
  /// <inheritdoc />
  public bool Equals(Product? a, Product? b)
  {
    if (ReferenceEquals(a, b)) return true;
    if (a is null || b is null) return false;

    return a.Id == b.Id &&
           a.BrandName == b.BrandName &&
           a.Name == b.Name &&
           a.DescriptionText == b.DescriptionText &&
           a.Articles.SequenceEqual(b.Articles);
  }

  /// <inheritdoc />
  public int GetHashCode(Product obj) => HashCode.Combine(obj.Id, obj.BrandName, obj.Name, obj.DescriptionText);
}