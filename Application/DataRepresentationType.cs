using System.Text.Json.Serialization;

namespace Goibhniu.Application;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DataRepresentationType
{
  /// <summary><see cref="RawProductRepresentationProvider"/></summary>
  Raw,
  /// <summary><see cref="ArticleFilteredRepresentationProvider"/></summary>
  OnlyRelevantArticles,
}