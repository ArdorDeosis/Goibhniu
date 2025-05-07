namespace Goibhniu.Infrastructure;

/// <summary>Configuration for data sources.</summary>
internal record DataSourcesSettings
{
  /// <summary>Configuration section name.</summary>
  public const string SectionName = "DataSources";

  /// <summary>
  /// Whether production data should be deserialized or not.
  /// </summary>
  /// <remarks>
  /// This property impacts the deserialization strategy used in the application.
  /// When set to true, the deserialization strategy will try to detect data sources with production data and handle the
  /// special format.
  /// </remarks>
  public bool AllowProductionData { get; init; } = true;
}