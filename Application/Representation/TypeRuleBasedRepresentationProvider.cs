namespace Goibhniu.Application;

/// <summary>
/// Provides a mechanism for generating client-facing representations of data based on a collection of type-matching
/// rules.
/// </summary>
/// <remarks>
/// The <see cref="TypeRuleBasedRepresentationProvider"/> operates by iterating through its defined rules to determine
/// if a representation for the given data can be produced. Each rule is applied in sequence, and the first rule capable
/// of creating a representation is used.
/// </remarks>
// ---
// NOTE: I know that this approach is probably overkill for such a small number of output types, but it was fun to
// implement it 🙃
internal abstract class TypeRuleBasedRepresentationProvider : IRepresentationProvider
{
  /// <summary>
  /// The collection of rules used for generating client-facing data representations.
  /// </summary>
  /// <remarks>
  /// Rules are evaluated sequentially to determine a suitable representation for the provided data. The first rule that
  /// successfully creates a representation is applied.
  /// </remarks>
  protected abstract IReadOnlyList<Rule> Rules { get; }

  /// <inheritdoc />
  public object? MakeRepresentation(object? data)
  {
    foreach (var rule in Rules)
      if (rule.TryMakeRepresentation(data, out var representation))
        return representation;
    return null;
  }
  
  /// <summary>
  /// A type-agnostic base class for type-matching representation rules. <see cref="Rule{T}"/> is used to implement the
  /// type-specific match and conversion logic.
  /// </summary>
  protected abstract class Rule
  {
    /// <summary>
    /// Creates a representation of the data, if the data has a matching type.
    /// </summary>
    /// <param name="data">The input data for which a representation is to be created. Can be null.</param>
    /// <param name="representation">The resulting representation. Can be null.</param>
    /// <returns>
    /// True if a suitable representation was successfully created for the provided data; otherwise, false.
    /// </returns>
    public abstract bool TryMakeRepresentation(object? data, out object? representation);
  }

  /// <summary>
  /// Type-specific implementation of <see cref="Rule"/> used in matching and transforming input data into a specific
  /// representation format.
  /// </summary>
  protected sealed class Rule<T> : Rule
  {
    private readonly Func<T, object?> makeTypedRepresentation;

    public Rule(Func<T, object?> makeTypedRepresentation)
    {
      this.makeTypedRepresentation = makeTypedRepresentation;
    }

    /// <inheritdoc />
    public override bool TryMakeRepresentation(object? data, out object? representation)
    {
      if (data is T typedData)
      {
        representation = makeTypedRepresentation(typedData);
        return true;
      }

      representation = null;
      return false;
    }
  }
}