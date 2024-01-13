namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Returns the first element of the sequence or a failed TryGetResult if it's empty.
    /// </summary>
    public static Result<TSource> TryGetFirst<TSource>(this IEnumerable<TSource> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        using var enumerator = source.GetEnumerator();
        return enumerator.MoveNext() ? Result<TSource>.Success(enumerator.Current) : Result<TSource>.Failure();
    }

    /// <summary>
    /// Returns the first element of the sequence or a failed TryGetResult if it's empty.
    /// </summary>
    public static Result<TSource> TryGetFirst<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));

        using var enumerator = source.GetEnumerator();

        while (enumerator.MoveNext())
        {
            if (predicate.Invoke(enumerator.Current))
                return  Result<TSource>.Success(enumerator.Current);
        }

        return Result<TSource>.Failure();;
    }
}