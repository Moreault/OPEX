namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Attempts to remove and return item at index.
    /// </summary>
    public static Result<TSource> TryPopAt<TSource>(this IList<TSource> source, int index)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (!source.IsWithinRange(index))
            return Result<TSource>.Failure();

        var item = source[index];
        source.RemoveAt(index);
        return Result<TSource>.Success(item);
    }

    /// <summary>
    /// Attempts to remove and return all items at indexes.
    /// </summary>
    public static IReadOnlyList<Result<TSource>> TryPopAt<TSource>(this IList<TSource> source, params int[] indexes) => source.TryPopAt((IEnumerable<int>)indexes);

    /// <summary>
    /// Attempts to remove and return all items at indexes.
    /// </summary>
    public static IReadOnlyList<Result<TSource>> TryPopAt<TSource>(this IList<TSource> source, IEnumerable<int> indexes)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (indexes == null) throw new ArgumentNullException(nameof(indexes));
        return indexes.OrderByDescending(x => x).Select(source.TryPopAt).ToArray();
    }
}