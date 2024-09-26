namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Returns the only element of the sequence or a failed <see cref="Result{T}"/> if it is empty or if there is more than one occurence.
    /// </summary>
    public static Result<TSource> TryGetSingle<TSource>(this IEnumerable<TSource> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        using var enumerator = source.GetEnumerator();

        if (!enumerator.MoveNext()) return Result<TSource>.Failure();
        var result = enumerator.Current;
        if (enumerator.MoveNext()) return Result<TSource>.Failure();
        return Result<TSource>.Success(result);
    }

    /// <summary>
    /// Returns the only element of the sequence or a failed TryGetResult if it is empty or if there is more than one occurence.
    /// </summary>
    public static Result<TSource> TryGetSingle<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
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
                if (isFound) return Result<TSource>.Failure();
                occurence = enumerator.Current;
                isFound = true;
            }
        }

        return isFound ? Result<TSource>.Success(occurence) : Result<TSource>.Failure();
    }
}