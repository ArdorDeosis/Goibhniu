using System.Text.Json.Serialization;

namespace Goibhniu.Model;

public record Product
{
  [JsonPropertyName("id")] 
  public required int Id { get; init; }

  [JsonPropertyName("brandName")] 
  public string? BrandName { get; init; }

  [JsonPropertyName("name")] 
  public required string Name { get; init; }

  [JsonPropertyName("descriptionText")] 
  public string? DescriptionText { get; init; }

  [JsonPropertyName("articles")] 
  public IReadOnlyList<Article> Articles { get; init; } = [];
}