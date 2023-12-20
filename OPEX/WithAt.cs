namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Returns a new immutable collection with the specified elements inserted at index.
    /// </summary>
    public static IReadOnlyList<TSource> WithAt<TSource>(this IEnumerable<TSource> source, int index, params TSource[] items) => source.WithAt(index, items as IEnumerable<TSource>);

    /// <summary>
    /// Returns a new immutable collection with the specified elements inserted at index.
    /// </summary>
    public static IReadOnlyList<TSource> WithAt<TSource>(this IEnumerable<TSource> source, int index, IEnumerable<TSource> items)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (items == null) throw new ArgumentNullException(nameof(items));
        var content = source.ToList();
        content.InsertRange(index, items);
        return content.ToImmutableList();
    }
}