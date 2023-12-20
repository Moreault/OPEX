namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Removes and returns item at index.
    /// </summary>
    public static TSource PopAt<TSource>(this IList<TSource> source, int index)
    {
        var result = source.TryPopAt(index);
        if (!result.IsSuccess) throw new ArgumentOutOfRangeException(nameof(index));
        return result.Value;
    }

    /// <summary>
    /// Removes and returns all items at indexes.
    /// </summary>
    public static IReadOnlyList<TSource> PopAt<TSource>(this IList<TSource> source, params int[] indexes) => source.PopAt((IEnumerable<int>)indexes);

    /// <summary>
    /// Removes and returns all items at indexes.
    /// </summary>
    public static IReadOnlyList<T> PopAt<T>(this IList<T> source, IEnumerable<int> indexes)
    {
        var result = source.TryPopAt(indexes);
        if (result.Any(x => !x.IsSuccess)) throw new ArgumentOutOfRangeException(nameof(indexes));
        return result.Select(x => x.Value).ToArray();
    }
}