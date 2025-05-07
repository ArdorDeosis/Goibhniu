namespace Goibhniu.Infrastructure.Tests;

// NOTE: In a 'real' scenario, there should probably be more test URLs
public class DeserializationStrategyTests
{
  private const string ProductionUrl = 
    "https://www.aaaaaaaaaaaa.com/elastic-query-portal/1337/v1/articles?list=%5B%5B86,1237,6797,136,2480,6722,3020,638,6671,1409,4244,1491,923,18452,7096,7048,1817,2692,6330,7291%5D%5D";

  private const string NonProductionUrl =
    "https://www.warhammer-community.com/en-gb/";

  [TestCase(NonProductionUrl)]
  [TestCase(ProductionUrl)]
  public void DefaultStrategy_AnyUrl_ReturnsDefaultDeserializer(string url)
  {
    // Arrange
    var strategy = DeserializationStrategy.Default;

    // Act
    var deserializer = strategy.Resolve(NonProductionUrl);

    // Assert
    Assert.That(deserializer, Is.EqualTo(ProductDataDeserializers.Default));
  }

  [Test]
  public void IncludeProductionDataStrategy_NonProductionUrl_ReturnsDefaultDeserializer()
  {
    // Arrange
    var strategy = DeserializationStrategy.IncludeProductionData;

    // Act
    var deserializer = strategy.Resolve(NonProductionUrl);

    // Assert
    Assert.That(deserializer, Is.EqualTo(ProductDataDeserializers.Default));
  }

  [Test]
  public void IncludeProductionDataStrategy_ProductionUrl_ReturnsProductionDataDeserializer()
  {
    // Arrange
    var strategy = DeserializationStrategy.IncludeProductionData;

    // Act
    var deserializer = strategy.Resolve(ProductionUrl);

    // Assert
    Assert.That(deserializer, Is.EqualTo(ProductDataDeserializers.ProductionData));
  }
}