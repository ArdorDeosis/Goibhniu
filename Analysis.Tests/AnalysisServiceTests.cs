using Goibhniu.Testing;

namespace Goibhniu.Analysis.Tests;

[TestFixture]
public class ProductAnalysisServiceTests
{
  private ProductAnalysisService service = null!;

  [SetUp]
  public void Setup()
  {
    service = new ProductAnalysisService();
  }

  [Test]
  public void GetPriceExtremes_ValidData_ReturnsSuccess()
  {
    // Arrange
    var expectedResult = TestData.Products.All.FindPriceExtremes();

    // Act
    var result = service.GetPriceExtremes(TestData.Products.All);

    // Assert
    Assert.Multiple(() =>
    {
      Assert.That(result.IsSuccess, Is.True);
      Assert.That(result.Data, Is.EqualTo(expectedResult));
    });
  }

  [Test]
  public void GetPriceExtremes_EmptyList_ReturnsFailure()
  {
    // Act
    var result = service.GetPriceExtremes([]);

    // Assert
    Assert.That(result.IsSuccess, Is.False);
  }

  [Test]
  public void GetProductsWithPrice_ValidData_ReturnsSuccess()
  {
    // Arrange
    const decimal price = 15.99m;
    var expectedResult = TestData.Products.All.WithPrice(price);

    // Act
    var result = service.GetProductsWithPrice(TestData.Products.All, price);

    // Assert
    Assert.Multiple(() =>
    {
      Assert.That(result.IsSuccess, Is.True);
      Assert.That(result.Data, Is.EqualTo(expectedResult));
    });
  }

  [Test]
  public void GetProductsWithPrice_EmptyList_ReturnsEmptySuccess()
  {
    // Arrange
    const decimal price = 15.99m;

    // Act
    var result = service.GetProductsWithPrice([], price);

    // Assert
    Assert.Multiple(() =>
    {
      Assert.That(result.IsSuccess, Is.True);
      Assert.That(result.Data, Is.Empty);
    });
  }

  [Test]
  public void GetProductWithMostBottles_ValidData_ReturnsSuccess()
  {
    // Arrange
    var expectedResult = TestData.Products.All.WithMostBottles();

    // Act
    var result = service.GetProductWithMostBottles(TestData.Products.All);

    // Assert
    Assert.Multiple(() =>
    {
      Assert.That(result.IsSuccess, Is.True);
      Assert.That(result.Data, Is.EqualTo(expectedResult));
    });
  }

  [Test]
  public void GetProductWithMostBottles_EmptyList_ReturnsFailure()
  {
    // Act
    var result = service.GetProductWithMostBottles([]);

    // Assert
    Assert.That(result.IsSuccess, Is.False);
  }
}