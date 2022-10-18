namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Returns the only element of the sequence or a failed TryGetResult if it is empty or if there is more than one occurence.
    /// </summary>
    public static TryGetResult<TSource> TryGetSingle<TSource>(this IEnumerable<TSource> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        using var enumerator = source.GetEnumerator();

        if (!enumerator.MoveNext()) return TryGetResult<TSource>.Failure;
        var result = enumerator.Current;
        if (enumerator.MoveNext()) return TryGetResult<TSource>.Failure;
        return new TryGetResult<TSource>(result);
    }

    /// <summary>
    /// Returns the only element of the sequence or a failed TryGetResult if it is empty or if there is more than one occurence.
    /// </summary>
    public static TryGetResult<TSource> TryGetSingle<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));

        using var enumerator = source.GetEnumerator();

        var isFound = false;
        TSource occurence = default!;
        while (enumerator.MoveNext())
        {
            if (predicate.Invoke(enumerator.Current))
            {
                //TODO Message
                if (isFound) return TryGetResult<TSource>.Failure;
                occurence = enumerator.Current;
                isFound = true;
            }
        }

        return isFound ? new TryGetResult<TSource>(true, occurence) : TryGetResult<TSource>.Failure;
    }
}