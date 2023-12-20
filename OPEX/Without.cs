namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Returns a new immutable collection without the specified elements.
    /// </summary>
    public static IReadOnlyList<TSource> Without<TSource>(this IEnumerable<TSource> source, params TSource[] items) => source.Without(items as IEnumerable<TSource>);

    /// <summary>
    /// Returns a new immutable collection without the specified elements.
    /// </summary>
    public static IReadOnlyList<TSource> Without<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> items)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (items == null) throw new ArgumentNullException(nameof(items));
        return source.Where(x => !items.Contains(x)).ToImmutableList();
    }

    /// <summary>
    /// Returns a new immutable collection without the specified elements.
    /// </summary>
    public static IReadOnlyList<TSource> Without<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));
        return source.Where(x => !predicate(x)).ToImmutableList();
    }
}