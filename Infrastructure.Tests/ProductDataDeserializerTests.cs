using System.Text;
using Goibhniu.Testing;

namespace Goibhniu.Infrastructure.Tests;

public class ProductDataDeserializerTests
{
  [Test]
  public async Task DefaultDeserializer_ValidData_ReturnsDeserializedProducts()
  {
    // Arrange
    await using var stream = TestData.Json.TestData.ToTestStream();

    // Act
    var result = await ProductDataDeserializers.Default(stream);

    // Assert
    Assert.Multiple(() =>
    {
      Assert.That(result.IsSuccess, Is.True);
      Assert.That(result.Data, Is.EquivalentTo(TestData.Json.ExpectedData).Using(new ProductEqualityComparer()));
    });
  }

  [Test]
  public async Task DefaultDeserializer_EmptyArray_ReturnsEmptyProductList()
  {
    // Arrange
    await using var stream = "[]".ToTestStream();

    // Act
    var result = await ProductDataDeserializers.Default(stream);

    // Assert
    Assert.Multiple(() =>
    {
      Assert.That(result.IsSuccess, Is.True);
      Assert.That(result.Data, Is.Empty);
    });
  }

  [Test]
  public async Task ProductionDataDeserializer_ValidData_ReturnsDeserializedProducts()
  {
    // Arrange
    await using var stream = TestData.Json.ProductionData.ToTestStream();

    // Act
    var result = await ProductDataDeserializers.ProductionData(stream);

    // Assert
    Assert.Multiple(() =>
    {
      Assert.That(result.IsSuccess, Is.True);
      Assert.That(result.Data,
        Is.EquivalentTo(TestData.Json.ExpectedData)
          .Using(new ProductEqualityComparer()));
    });
  }

  [Test]
  public async Task ProductionDataDeserializer_EmptyResults_ReturnsEmptyProductList()
  {
    // Arrange
    const string emptyJson = """{"results":[]}""";
    await using var stream = emptyJson.ToTestStream();

    // Act
    var result = await ProductDataDeserializers.ProductionData(stream);

    // Assert
    Assert.Multiple(() =>
    {
      Assert.That(result.IsSuccess, Is.True);
      Assert.That(result.Data, Is.Empty);
    });
  }
}

file static class Extensions
{
  public static Stream ToTestStream(this string data) => new MemoryStream(Encoding.UTF8.GetBytes(data));
}