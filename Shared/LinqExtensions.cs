namespace Goibhniu.Shared;

public static class LinqExtensions
{
  /// <summary>
  /// Returns the elements with the minimum and maximum key value in a sequence according to a specified key selector
  /// function.
  /// </summary>
  /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
  /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
  /// <param name="source">An <see cref="IEnumerable{T}"/> to return the minimum and maximum elements from.</param>
  /// <param name="keySelector">A function to extract a key from an element.</param>
  /// <returns>
  /// A tuple containing the element with the minimum key and the element with the maximum key in the sequence.
  /// </returns>
  /// <exception cref="InvalidOperationException">The source sequence is empty.</exception>
  /// <remarks>
  /// This method iterates the sequence exactly once. If multiple elements share the minimum or maximum key value, the
  /// first element encountered with that value is returned. The comparison is performed using the default comparer for
  /// <typeparamref name="TKey"/>.
  /// </remarks>
  public static (TSource Min, TSource Max) MinMaxBy<TSource, TKey>(
    this IEnumerable<TSource> source,
    Func<TSource, TKey> keySelector)
  {
    using var enumerator = source.GetEnumerator();

    if (!enumerator.MoveNext())
      throw new InvalidOperationException("Source contains no elements.");

    var minElement = enumerator.Current;
    var maxElement = enumerator.Current;
    var minKey = keySelector(minElement);
    var maxKey = keySelector(maxElement);

    var comparer = Comparer<TKey>.Default;

    while (enumerator.MoveNext())
    {
      var currentElement = enumerator.Current;
      var currentKey = keySelector(currentElement);

      if (currentKey is null)
        continue;
      
      if (comparer.Compare(currentKey, minKey) < 0)
      {
        minKey = currentKey;
        minElement = currentElement;
      }

      if (comparer.Compare(currentKey, maxKey) > 0)
      {
        maxKey = currentKey;
        maxElement = currentElement;
      }
    }

    return (minElement, maxElement);
  }
}