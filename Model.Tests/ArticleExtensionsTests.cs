namespace Goibhniu.Model.Tests;

public class ArticleExtensionsTests
{
  private const string UnparsableString = "I'll be damned, if this is parsable 🍤";
  
  private static readonly Article TestArticleBase = new()
  {
    Id = 1,
    ShortDescription = "Test Article",
    Price = 3.50m,
  };

  [TestCase(null, null)]
  [TestCase("", null)]
  [TestCase(UnparsableString, null)]
  [TestCase("(13,99 €/Liter)", 13.99)]
  [TestCase("(13.99 €/Liter)", null)] // German culture format needed
  [TestCase("(50000 €/Liter)", 50000)]
  [TestCase("(0,5 €/Liter)", 0.5)]
  [TestCase(" (10,50 €/Liter) more text", 10.50)]
  public void ParsePricePerUnitFromText_ReturnsExpectedValue(string? pricePerUnitText, decimal? expectedResult)
  {
    // Arrange
    var article = TestArticleBase with
    {
      PricePerUnitText = pricePerUnitText
    };

    // Act
    var result = article.PricePerUnit; // triggers parsing from PricePerUnitText

    // Assert
    Assert.That(result, Is.EqualTo(expectedResult));
  }

  [TestCase("", null)]
  [TestCase(UnparsableString, null)]
  [TestCase("20 x 0,5L (Glas)", 20)]
  [TestCase("10x0,5L (Glas)", 10)]
  [TestCase("24X 0,25L (Glas)", 24)]
  [TestCase("3 x 0,33L Dose", 3)]
  [TestCase("24 x 0,33L", 24)]
  [TestCase("(6 x 1L bottles)", 6)]
  public void ParseItemCountPerFrameFromText_ReturnsExpectedValue(string description, int? expectedResult)
  {
    // Arrange
    var article = TestArticleBase with
    {
      ShortDescription = description
    };

    // Act
    var result = article.ItemsInFrame; // triggers parsing from ShortDescription

    // Assert
    Assert.That(result, Is.EqualTo(expectedResult));
  }
}