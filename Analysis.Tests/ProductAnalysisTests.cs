using Goibhniu.Model;
using Goibhniu.Testing;

namespace Goibhniu.Analysis.Tests;

public class ProductAnalysisTests
{
  /// <summary> Tuples of input data and expected output. </summary>
  private static IEnumerable<object[]> FindExtremesTestData =>
  [
    // single product, single article
    [
      new[] { TestData.Products.Duff },
      new PriceExtremesData(
        new ProductArticlePair(TestData.Products.Duff, TestData.Articles.Duff),
        new ProductArticlePair(TestData.Products.Duff, TestData.Articles.Duff)
      ),
    ],
    // single product, two articles
    [
      new[] { TestData.Products.KlabusterbrauDunkel },
      new PriceExtremesData(
        new ProductArticlePair(TestData.Products.KlabusterbrauDunkel, TestData.Articles.KlabusterbrauKasten),
        new ProductArticlePair(TestData.Products.KlabusterbrauDunkel, TestData.Articles.KlabusterbrauFass)
      ),
    ],
    // two products
    [
      new[] { TestData.Products.Duff, TestData.Products.Oe },
      new PriceExtremesData(
        new ProductArticlePair(TestData.Products.Oe, TestData.Articles.Oe),
        new ProductArticlePair(TestData.Products.Duff, TestData.Articles.Duff)
      ),
    ],
    // two products, but mox extremes are of a single product
    [
      new[] { TestData.Products.Oe, TestData.Products.KlabusterbrauDunkel },
      new PriceExtremesData(
        new ProductArticlePair(TestData.Products.KlabusterbrauDunkel, TestData.Articles.KlabusterbrauKasten),
        new ProductArticlePair(TestData.Products.KlabusterbrauDunkel, TestData.Articles.KlabusterbrauFass)
      ),
    ],
    // multiple products
    [
      new[]
      {
        TestData.Products.Duff, TestData.Products.KlabusterbrauDunkel, TestData.Products.Oe, TestData.Products.NulnOil
      },
      new PriceExtremesData(
        new ProductArticlePair(TestData.Products.KlabusterbrauDunkel, TestData.Articles.KlabusterbrauKasten),
        new ProductArticlePair(TestData.Products.Duff, TestData.Articles.Duff)
      ),
    ],
  ];

  [TestCaseSource(nameof(FindExtremesTestData))]
  public void FindPriceExtremes_ValidData_ReturnsCorrectData(IEnumerable<Product> products,
    PriceExtremesData expectedResult)
  {
    // Act
    var result = products.FindPriceExtremes();

    // Assert
    Assert.That(result, Is.EqualTo(expectedResult));
  }

  [Test]
  public void FindPriceExtremes_EmptyData_Throws()
  {
    // Arrange
    var emptyProducts = Array.Empty<Product>();

    // Act & Assert
    Assert.That(() => emptyProducts.FindPriceExtremes(), Throws.TypeOf<InvalidOperationException>());
  }

  /// <summary> Tuples of price and expected output. </summary>
  private static IEnumerable<object[]> WithPriceTestData =>
  [
    // prices with no articles
    [0m, Array.Empty<ProductArticlePair>()],
    [16m, Array.Empty<ProductArticlePair>()],
    // prices with one article
    [
      15.99m,
      new ProductArticlePair[] { new(TestData.Products.Duff, TestData.Articles.Duff) },
    ],
    [
      21.99m,
      new ProductArticlePair[] { new(TestData.Products.KlabusterbrauDunkel, TestData.Articles.KlabusterbrauKasten) },
    ],
    // price with two articles, ordered by price per unit
    [
      29.99m,
      new ProductArticlePair[]
      {
        new(TestData.Products.NulnOil, TestData.Articles.NulnOil),
        new(TestData.Products.KlabusterbrauDunkel, TestData.Articles.KlabusterbrauFass),
      },
    ],
  ];

  [TestCaseSource(nameof(WithPriceTestData))]
  public void WithPrice_ValidData_ReturnsCorrectData(decimal price, IEnumerable<ProductArticlePair> expectedResult)
  {
    // Act
    var result = TestData.Products.All.WithPrice(price);

    // Assert
    Assert.That(result, Is.EqualTo(expectedResult));
  }

  [TestCase(0)]
  [TestCase(1)]
  [TestCase(15.99)]
  [TestCase(21.99)]
  [TestCase(29.99)]
  public void WithPrice_EmptyData_ReturnsEmptyResult(decimal price)
  {
    // Arrange
    var emptyProductList = Array.Empty<Product>();

    // Act
    var result = emptyProductList.WithPrice(price);

    // Assert
    Assert.That(result, Is.Empty);
  }


  /// <summary> Tuples of product lists and expected output. </summary>
  private static IEnumerable<object[]> WithMostBottlesTestData =>
  [
    // single product, single article
    [
      new[] { TestData.Products.Duff },
      new ProductArticlePair(TestData.Products.Duff, TestData.Articles.Duff),
    ],
    // single product, two articles
    [
      new[] { TestData.Products.KlabusterbrauDunkel },
      new ProductArticlePair(TestData.Products.KlabusterbrauDunkel, TestData.Articles.KlabusterbrauKasten),
    ],
    // multiple products, single correct answer
    [
      new[] { TestData.Products.Duff, TestData.Products.Oe, TestData.Products.NulnOil },
      new ProductArticlePair(TestData.Products.NulnOil, TestData.Articles.NulnOil),
    ],
    // multiple products, multiple correct answer
    [
      TestData.Products.All,
      new ProductArticlePair(TestData.Products.KlabusterbrauDunkel, TestData.Articles.KlabusterbrauKasten),
      // NOTE: Technically, Nuln Oil would also be correct; but the specification asks for a single item, which in the
      // current implementation is the first found.
      // This test might break if Microsoft changes the implementation of .MaxBy()
    ],
  ];

  [TestCaseSource(nameof(WithMostBottlesTestData))]
  public void WithMostBottles_ValidData_ReturnsCorrectData(IEnumerable<Product> products,
    ProductArticlePair expectedResult)
  {
    // Act
    var result = products.WithMostBottles();

    // Assert
    Assert.That(result, Is.EqualTo(expectedResult));
  }

  [Test]
  public void WithMostBottles_EmptyData_Throws()
  {
    // Arrange
    var emptyProducts = Array.Empty<Product>();

    // Act & Assert
    Assert.That(() => emptyProducts.WithMostBottles(), Throws.TypeOf<InvalidOperationException>());
  }
}