namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Removes and returns all items from collection.
    /// </summary>
    public static IReadOnlyList<TSource> PopAll<TSource>(this IList<TSource> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        var output = source.ToArray();
        source.Clear();
        return output;
    }

    /// <summary>
    /// Removes and returns all occurences of item from collection.
    /// </summary>
    public static IReadOnlyList<TSource> PopAll<TSource>(this IList<TSource> source, TSource item)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        var indexes = source.IndexesOf(item);
        return indexes.OrderByDescending(x => x).Select(source.PopAt).ToArray();
    }

    /// <summary>
    /// Removes and returns all occurences of lambda from collection.
    /// </summary>
    public static IReadOnlyList<TSource> PopAll<TSource>(this IList<TSource> source, Func<TSource, bool> match)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (match == null) throw new ArgumentNullException(nameof(match));
        var indexes = source.IndexesOf(match);
        return indexes.OrderByDescending(x => x).Select(source.PopAt).ToArray();
    }
}