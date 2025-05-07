using System.Text.Json;
using System.Text.Json.Serialization;
using Goibhniu.Model;
using Goibhniu.Shared;

namespace Goibhniu.Infrastructure;

/// <summary>
/// Signature contract for deserializing product data from a stream into a strongly typed result, supporting
/// asynchronous operations with optional cancellation.
/// </summary>
internal delegate Task<Result<IReadOnlyCollection<Product>>> ProductDataDeserializer(
  Stream stream, CancellationToken cancellationToken = default);

/// <summary>
/// A collection of pre-defined deserialization delegates for parsing product data streams.
/// </summary>
// ---
// NOTE: I opted for a delegate property approach here to keep the deserialization logic signatures DRY.
internal static class ProductDataDeserializers
{
  private const string DeserializationErrorMessage = "failed to deserialize data";

  /// <summary>
  /// Default deserializer; deserializes JSON data in the format provided with the exercise. 
  /// </summary>
  public static ProductDataDeserializer Default { get; } = async (stream, token) =>
    await JsonSerializer.DeserializeAsync<IReadOnlyCollection<Product>>(stream, default(JsonSerializerOptions), token)
      is not { } products
      ? DeserializationErrorMessage
      : Result<IReadOnlyCollection<Product>>.Success(products);

  /// <summary>
  /// Production data deserializer; Deserializes JSON data in the format used by the actual website. (same format as the
  /// test data, but wrapped in another response object)
  /// </summary>
  public static ProductDataDeserializer ProductionData { get; } = async (stream, token) =>
    await JsonSerializer.DeserializeAsync<ProductionDataWrapper>(stream, default(JsonSerializerOptions), token)
      is not { Results: { } products }
      ? DeserializationErrorMessage
      : Result<IReadOnlyCollection<Product>>.Success(products);

  /// <summary>Used internally to deserialize production data.</summary>
  private record ProductionDataWrapper
  {
    [JsonPropertyName("results")] 
    public IReadOnlyCollection<Product> Results { get; init; } = [];
  }
}