namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Returns the last element of the sequence or a failed TryGetResult if it's empty.
    /// </summary>
    public static TryGetResult<TSource> TryGetLast<TSource>(this IEnumerable<TSource> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        if (source is IReadOnlyList<TSource> list)
            return list.Any() ? new TryGetResult<TSource>(list[list.LastIndex()]) : TryGetResult<TSource>.Failure;

        using var enumerator = source.GetEnumerator();

        var isFound = false;
        TSource last = default!;
        while (enumerator.MoveNext())
        {
            isFound = true;
            last = enumerator.Current;
        }

        return isFound ? new TryGetResult<TSource>(last) : TryGetResult<TSource>.Failure;
    }

    /// <summary>
    /// Returns the last element of the sequence or a failed TryGetResult if it's empty.
    /// </summary>
    public static TryGetResult<TSource> TryGetLast<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));

        if (source is IReadOnlyList<TSource> list)
        {
            for (var i = list.LastIndex(); i > 0; i--)
            {
                if (predicate(list[i])) return new TryGetResult<TSource>(list[i]);
            }
            return TryGetResult<TSource>.Failure;
        }

        using var enumerator = source.GetEnumerator();

        var isFound = false;
        TSource last = default!;
        while (enumerator.MoveNext())
        {
            if (predicate.Invoke(enumerator.Current))
            {
                isFound = true;
                last = enumerator.Current;
            };
        }

        return isFound ? new TryGetResult<TSource>(last) : TryGetResult<TSource>.Failure;
    }
}