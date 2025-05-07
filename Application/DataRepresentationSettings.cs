namespace Goibhniu.Application;

/// <summary>Configuration for data representation.</summary>
internal record DataRepresentationSettings
{
  /// <summary>Configuration section name.</summary>
  public const string SectionName = "DataRepresentation";

  /// <summary>
  /// The type of data representation to be used for generating client-facing outputs.
  /// </summary>
  /// <remarks>See <see cref="DataRepresentationType"/>'s documentation for more detail.</remarks>
  public DataRepresentationType DataRepresentationType { get; init; } = DataRepresentationType.OnlyRelevantArticles;
}