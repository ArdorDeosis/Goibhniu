namespace Goibhniu.Model.Tests;

public class ArticleTests
{
  private const string UnparsableString = "This should really not be parsable into any meaningful value 🍺";
  
  private static readonly Article TestArticleBase = new()
  {
    Id = 1,
    ShortDescription = "Test Article",
    Price = 10.99m,
  };
  
  [TestCase(null, null, null)]
  [TestCase(UnparsableString, null, null)]
  [TestCase("(13,99 €/Liter)", null, 13.99)]
  [TestCase("(50000 €/Liter)", null, 50000)]
  [TestCase(null, 3.5, 3.5)]
  [TestCase("(13,99 €/Liter)", 10, 10)]
  public void PricePerUnit_ReturnsExpectedValue(string? valueText, decimal? value, decimal? expectedValue)
  {
    // Arrange
    var article = TestArticleBase with
    {
      PricePerUnitText = valueText,
      PricePerUnit = value,
    };

    // Act
    var result = article.PricePerUnit;

    // Assert
    Assert.That(result, Is.EqualTo(expectedValue));
  }

  [TestCase(UnparsableString, null, null)]
  [TestCase("20 x 0,5L (Glas)", null, 20)]
  [TestCase(UnparsableString, 13, 13)]
  [TestCase("9000 x 0,33L (Glas)", 12, 12)]
  public void ItemsInFrame_ReturnsExpectedValue(string description, int? value, int? expectedValue)
  {
    // Arrange
    var article = TestArticleBase with
    {
      ShortDescription = description,
      ItemsInFrame = value,
    };

    // Act
    var result = article.ItemsInFrame;

    // Assert
    Assert.That(result, Is.EqualTo(expectedValue));
  }
}