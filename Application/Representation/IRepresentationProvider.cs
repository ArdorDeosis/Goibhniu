namespace Goibhniu.Application;

/// <summary>
/// A service for generating client-facing representations of arbitrary types.
/// </summary>
public interface IRepresentationProvider
{
  /// <summary>
  /// Generates a client-facing representation of the provided data.
  /// </summary>
  /// <param name="data">The data to generate a representation for. This can be an object of any type.</param>
  /// <returns>A representation of the provided data, or null if no suitable representation can be generated.</returns>
  public object? MakeRepresentation(object? data);
}