using System.Net;
using Goibhniu.Model;
using Goibhniu.Testing;

namespace Goibhniu.Infrastructure.Tests;

public class ProductDataServiceTests
{
  private TestHttpClientFactory testHttpClientFactory = null!;
  private IDeserializationStrategy deserializationStrategy = null!;
  private ProductDataService productDataService = null!;
  private const string TestUrl = "https://test.com/products";

  private static IEnumerable<IEnumerable<Product>> ProductLists =>
  [
    [],
    [TestData.Products.Duff],
    [TestData.Products.KlabusterbrauDunkel],
    [TestData.Products.Duff, TestData.Products.KlabusterbrauDunkel, TestData.Products.Oe],
  ];

  [SetUp]
  public void Setup()
  {
    testHttpClientFactory = new TestHttpClientFactory();
    deserializationStrategy =
      DeserializationStrategy.Default; // testing with basic deserialization (not testing production data)
    productDataService = new ProductDataService(testHttpClientFactory, deserializationStrategy);
  }

  [TearDown]
  public void TearDown()
  {
    testHttpClientFactory.Dispose();
  }

  [TestCaseSource(nameof(ProductLists))]
  public async Task FetchProductData_HttpRequestSucceeds_ReturnsDeserializedProducts(IEnumerable<Product> products)
  {
    // Arrange
    testHttpClientFactory.SetResponse(products);

    // Act
    var result = await productDataService.FetchProductData(TestUrl);

    // Assert
    Assert.Multiple(() =>
    {
      Assert.That(result.IsSuccess, Is.True);
      Assert.That(result.Data, Is.EquivalentTo(products).Using(new ProductEqualityComparer()));
    });
  }

  [Test]
  public async Task FetchProductData_HttpRequestFails_ReturnsFailureResult()
  {
    // Arrange
    testHttpClientFactory.SetResponse(null, HttpStatusCode.BadRequest);

    // Act
    var result = await productDataService.FetchProductData(TestUrl);

    // Assert
    Assert.Multiple(() =>
    {
      Assert.That(result.IsSuccess, Is.False);
      Assert.That(result.ErrorMessage, Is.EqualTo("failed to fetch data"));
    });
  }

  [Test]
  public async Task FetchProductData_Throws_ReturnsFailureResult()
  {
    // Arrange
    const string exceptionMessage = "'twas a cat!";
    testHttpClientFactory.Exception = new Exception(exceptionMessage);

    // Act
    var result = await productDataService.FetchProductData(TestUrl);

    // Assert
    Assert.Multiple(() =>
    {
      Assert.That(result.IsSuccess, Is.False);
      Assert.That(result.ErrorMessage, Is.EqualTo(exceptionMessage));
    });
  }

  [Test]
  public async Task FetchProductData_WithCancellation_CancellationTokenIsPassed()
  {
    // Arrange
    using var cts = new CancellationTokenSource();
    var token = cts.Token;
    await cts.CancelAsync();

    // Act
    var result = await productDataService.FetchProductData(TestUrl, token);

    // Assert
    Assert.Multiple(() =>
    {
      Assert.That(result.IsSuccess, Is.False);
      Assert.That(result.ErrorMessage, Is.EqualTo(TestHttpClientFactory.CancellationMessage));
    });
  }
}