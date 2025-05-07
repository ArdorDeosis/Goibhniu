using Goibhniu.Model;

namespace Goibhniu.Application;

/// <summary>
/// Provides a specialized implementation for creating client-facing representations of
/// <see cref="ProductArticlePair">product/article pairs</see> using type-based rules.
/// </summary>
/// <remarks>
/// Subclasses should implement the abstract methods to define the transformation logic for the representation of
/// <see cref="ProductArticlePair"/> and its collections.
/// </remarks>
internal abstract class ProductRepresentationProvider : TypeRuleBasedRepresentationProvider
{
  /// <inheritdoc />
  protected override IReadOnlyList<Rule> Rules =>
  [
    new Rule<ProductArticlePair>(MakeRepresentation),
    new Rule<IEnumerable<ProductArticlePair>>(MakeRepresentation),
    new Rule<object>(MakeRepresentationOfUnknownType),
  ];

  /// <summary>
  /// Transforms the given <see cref="ProductArticlePair"/> into a client-facing representation.
  /// </summary>
  /// <param name="data">The <see cref="ProductArticlePair"/> instance to be transformed into a representation.</param>
  /// <returns>An object representing the transformed <see cref="ProductArticlePair"/> for client-facing use.</returns>
  protected abstract object MakeRepresentation(ProductArticlePair data);

  /// <summary>
  /// Creates a client-facing representation for a collection of <see cref="ProductArticlePair"/> objects.
  /// </summary>
  /// <param name="data">
  /// The collection of <see cref="ProductArticlePair"/> objects to be transformed into a representation.
  /// </param>
  /// <returns>An object representing the transformed representation of the provided data collection.</returns>
  protected abstract object MakeRepresentation(IEnumerable<ProductArticlePair> data);

  /// <summary>
  /// Handles all other types. This method creates a representation of an arbitrary object by copying all properties
  /// this provider can create representations for (<see cref="ProductArticlePair"/> or a collection of such).<br/>
  /// Properties whose representation would be null are ignored.
  /// </summary>
  private Dictionary<string, object>? MakeRepresentationOfUnknownType(object data)
  {
    var properties = GetProperties().ToDictionary();
    return properties.Count > 0 ? properties : null;

    IEnumerable<KeyValuePair<string, object>> GetProperties()
    {
      foreach (var property in data.GetType().GetProperties())
      {
        if (MakeRepresentation(property.GetValue(data)) is {} representation)
          yield return KeyValuePair.Create(property.Name, representation);
      }
    }
  }
}