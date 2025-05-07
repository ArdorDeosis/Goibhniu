using Goibhniu.Model;

namespace Goibhniu.Testing;

internal static partial class TestData
{
  /// <summary>
  /// Provides predefined JSON strings of product data for testing purposes.
  /// </summary>
  public static class Json
  {
    public const string TestData =
      """
      [
        {
          "id": 1,
          "brandName": "Duff",
          "name": "Duff Lager",
          "descriptionText": "The original from Springfield!",
          "articles": [
            {
              "id": 100,
              "shortDescription": "6 x 0,33L (Dose)",
              "price": 15.99,
              "pricePerUnitText": "8,00 €/Liter",
              "image": "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ-FqdnjbQ4Cnkkuc0884rupvrw82RTgOB-xQ&s"
            }
          ]
        },
        {
          "id": 2,
          "brandName": "Klabusterbräu",
          "name": "Klabusterbräu Dunkel",
          "articles": [
            {
              "id": 201,
              "shortDescription": "20 x 0.5L (Glas)",
              "price": 21.99,
              "pricePerUnitText": "2,20 €/Liter"
            },
            {
              "id": 202,
              "shortDescription": "1 x 5L (Fass)",
              "price": 29.99,
              "pricePerUnitText": "6,00 €/Liter"
            }
          ]
        },
        {
          "id": 3,
          "brandName": "Ö",
          "name": "Ö Export",
          "descriptionText": "Zieh's dir Ryan!",
          "articles": [
            {
              "id": 301,
              "shortDescription": "2 x 0.2L (Renntasse)",
              "price": 1.0,
              "pricePerUnitText": "2,50 €/Liter"
            }
          ]
        },
        {
          "id": 4,
          "brandName": "Nuln",
          "name": "Nuln Oil",
          "articles": [
            {
              "id": 401,
              "shortDescription": "20 x 0.5L (Glas)",
              "price": 29.99,
              "pricePerUnitText": "3,00 €/Liter"
            }
          ]
        }
      ]
      """;

    public const string ProductionData =
      $$"""
        {
          "info": {
            "needed": 0.041,
            "indexUsed": "transient-article-api/cf",
            "lastUpdatedForWarehouse": "all",
            "updated": "2025-05-07T03:52:35.700Z"
          },
          "results": {{TestData}} 
        }
        """;

    public static readonly Product[] ExpectedData = [Products.Duff, Products.KlabusterbrauDunkel, Products.Oe, Products.NulnOil];
  }
}