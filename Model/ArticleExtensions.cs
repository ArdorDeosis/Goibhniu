using System.Globalization;
using System.Text.RegularExpressions;

namespace Goibhniu.Model;

// ---
// NOTE: I'm not 100% comfortable placing this string conversion logic here, but right now I don't feel like pulling it
// out into an injectable service. One could argue that it is tightly coupled to the domain model after all. 
internal static partial class ArticleExtensions
{
  private const NumberStyles PricePerUnitNumberStyle = NumberStyles.AllowDecimalPoint;
  private const NumberStyles ItemsPerFrameNumberStyle = NumberStyles.None;
  
  private static readonly IFormatProvider NumberFormatInfo = CultureInfo.GetCultureInfo("de-DE");

  [GeneratedRegex(@"([\d,.]*) €/Liter")]
  private static partial Regex PricePerUnitTextPattern { get; }

  /// <summary>
  /// Parses the price per unit from the price per unit text of an article.
  /// Uses a regular expression to extract the price value specified in the text.
  /// </summary>
  /// <param name="article">The article whose price per unit to retrieve.</param>
  /// <returns>The parsed price per unit as a decimal if found; otherwise, null.</returns>
  public static decimal? ParsePricePerUnitFromText(this Article article) =>
    !string.IsNullOrEmpty(article.PricePerUnitText) &&
    PricePerUnitTextPattern.FindFirstMatchedGroup(article.PricePerUnitText) is { } matchedPriceString &&
    decimal.TryParse(matchedPriceString, PricePerUnitNumberStyle, NumberFormatInfo, out var pricePerUnit)
      ? pricePerUnit
      : null;

  
  [GeneratedRegex(@"(\d+)\s*x\s*", RegexOptions.IgnoreCase)]
  private static partial Regex ItemCountPerFrameTextPattern { get; }

  /// <summary>
  /// Parses the number of items (usually bottles) in the article from its short description text using
  /// <see cref="ItemCountPerFrameTextPattern"/>.
  /// </summary>
  /// <param name="article">The article whose item count shall be retrieved.</param>
  /// <returns>
  /// The item count if the article has a <see cref="Article.ShortDescription"/> that can be parsed with
  /// <see cref="ItemCountPerFrameTextPattern"/>; otherwise, null.
  /// </returns>
  public static int? ParseItemCountPerFrameFromText(this Article article) =>
    !string.IsNullOrEmpty(article.ShortDescription) &&
    ItemCountPerFrameTextPattern.FindFirstMatchedGroup(article.ShortDescription) is { } matchedItemCountString &&
    int.TryParse(matchedItemCountString, ItemsPerFrameNumberStyle, NumberFormatInfo, out var itemCount)
      ? itemCount
      : null;

  /// <summary>
  /// Finds the first matching group within a given string based on the regular expression pattern.
  /// </summary>
  /// <param name="regex">The regular expression used to find the match.</param>
  /// <param name="value">The input string to search for a match.</param>
  /// <returns>The value of the first matched group if found; otherwise, null.</returns>
  private static string? FindFirstMatchedGroup(this Regex regex, string value) =>
    regex.Match(value) is { Success: true, Groups: [_, { Value: { } groupValue }] } ? groupValue : null;
}