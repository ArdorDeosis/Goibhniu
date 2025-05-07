using System.Text.Json.Serialization;

namespace Goibhniu.Model;

// NOTE: I'm using a preview C# 14 here, to _finally_ be able to use the field keyword, took 'em long enough 🫠
public record Article
{
  [JsonPropertyName("id")]
  public required int Id { get; init; }
  
  [JsonPropertyName("shortDescription")]
  public required string ShortDescription { get; init; }
  
  [JsonPropertyName("price")]
  public required decimal Price { get; init; }
  
  [JsonPropertyName("unit")]
  public string? Unit { get; init; }
  
  [JsonPropertyName("pricePerUnitText")]
  public string? PricePerUnitText { get; init; }

  [JsonPropertyName("pricePerUnit")]
  public decimal? PricePerUnit
  {
    get => field ??= this.ParsePricePerUnitFromText();
    init;
  }

  [JsonPropertyName("image")]
  public string? Image { get; init; }

  
  [JsonPropertyName("itemsInFrame")]
  public int? ItemsInFrame
  {
    get => field ??= this.ParseItemCountPerFrameFromText();
    init;
  }
}