using Goibhniu.Model;

namespace Goibhniu.Testing;

internal static partial class TestData
{
  /// <summary>
  /// Provides predefined article data for testing purposes.
  /// </summary>
  /// <remarks>
  /// Articles in this class are derived from the predefined products in the <see cref="TestData.Products"/> class.
  /// </remarks>
  public static class Articles
  {
    public static readonly Article Duff = Products.Duff.Articles[0];

    public static readonly Article KlabusterbrauKasten = Products.KlabusterbrauDunkel.Articles[0];

    public static readonly Article KlabusterbrauFass = Products.KlabusterbrauDunkel.Articles[1];

    public static readonly Article Oe = Products.Oe.Articles[0];

    public static readonly Article NulnOil = Products.NulnOil.Articles[0];
  }
}