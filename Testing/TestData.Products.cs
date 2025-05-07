using Goibhniu.Model;

namespace Goibhniu.Testing;

internal static partial class TestData
{
  /// <summary>
  /// Provides predefined product data for testing purposes.
  /// </summary>
  /// <remarks>
  /// <see cref="Json">TestData.Json</see> contains corresponding JSON strings.
  /// </remarks>
  public static class Products
  {
    public static IReadOnlyCollection<Product> All => [Duff, KlabusterbrauDunkel, Oe, NulnOil];
    
    public static readonly Product Duff = new()
    {
      Id = 1,
      BrandName = "Duff",
      Name = "Duff Lager",
      DescriptionText = "The original from Springfield!",
      Articles =
      [
        new Article
        {
          Id = 100,
          ShortDescription = "6 x 0,33L (Dose)",
          Price = 15.99m,
          PricePerUnitText = "8,00 €/Liter",
          Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ-FqdnjbQ4Cnkkuc0884rupvrw82RTgOB-xQ&s",
        },
      ],
    };

    public static readonly Product KlabusterbrauDunkel = new()
    {
      Id = 2,
      BrandName = "Klabusterbräu",
      Name = "Klabusterbräu Dunkel",
      Articles =
      [
        new Article
        {
          Id = 201,
          ShortDescription = "20 x 0.5L (Glas)",
          Price = 21.99m,
          PricePerUnitText = "2,20 €/Liter",
        },
        new Article
        {
          Id = 202,
          ShortDescription = "1 x 5L (Fass)",
          Price = 29.99m,
          PricePerUnitText = "6,00 €/Liter",
        },
      ],
    };

    public static readonly Product Oe = new()
    {
      Id = 3,
      BrandName = "Ö",
      Name = "Ö Export",
      DescriptionText = "Zieh's dir Ryan!",
      Articles =
      [
        new Article
        {
          Id = 301,
          ShortDescription = "2 x 0.2L (Renntasse)",
          Price = 1m,
          PricePerUnitText = "2,50 €/Liter",
        },
      ],
    };

    public static readonly Product NulnOil = new()
    {
      Id = 4,
      BrandName = "Nuln",
      Name = "Nuln Oil",
      Articles =
      [
        new Article
        {
          Id = 401,
          ShortDescription = "20 x 0.5L (Glas)",
          Price = 29.99m,
          PricePerUnitText = "3,00 €/Liter",
        },
      ],
    };
  }
}