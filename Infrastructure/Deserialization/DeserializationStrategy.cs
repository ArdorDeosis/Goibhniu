using System.Text.RegularExpressions;

namespace Goibhniu.Infrastructure;

/// <summary>
/// A set of deserialization strategies used to resolve the appropriate deserializer for processing product data based
/// on specific conditions (...the URL, it is just based on the URL right now).
/// </summary>
internal static partial class DeserializationStrategy
{
  /// <summary>
  /// Specifies the default deserialization strategy to be used for processing product data.
  /// This strategy always uses the <see cref="ProductDataDeserializers.Default"/> deserializer.
  /// </summary>
  public static IDeserializationStrategy Default { get; } = new BasicDeserialization();
  
  /// <summary>
  /// This strategy uses <see cref="ProductDataDeserializers.ProductionData"/> deserializer, when the URL is detected to
  /// be a production system URL. Otherwise, it falls back onto <see cref="Default"/> strategy.
  /// </summary>
  public static IDeserializationStrategy IncludeProductionData { get; } = new ProductionDataDeserialization();
  
  private sealed class BasicDeserialization : IDeserializationStrategy
  {
    public ProductDataDeserializer Resolve(string _) => ProductDataDeserializers.Default;
  }
  
  private sealed partial class ProductionDataDeserialization : IDeserializationStrategy
  {
    // NOTE: Tried to somewhat obscure the actual URL, since this is hosted publicly
    [GeneratedRegex(@"https:\/\/www\.[acefhlnopst]{12}\.(shop|com|de|app)/elastic-query-portal/\d+/v1/articles.+")]
    private static partial Regex ProductionDataUrlPattern { get; }
  
    public ProductDataDeserializer Resolve(string url) => ProductionDataUrlPattern.IsMatch(url) 
      ? ProductDataDeserializers.ProductionData
      : Default.Resolve(url);
  }
}