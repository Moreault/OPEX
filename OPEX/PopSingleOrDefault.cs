namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Removes and returns the only element of a collection, or a default value if the collection is empty;
    /// </summary>
    public static TSource? PopSingleOrDefault<TSource>(this IList<TSource> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (source.Count > 1) throw new InvalidOperationException("The collection contains more than one element");
        return source.Any() ? source.TryPopAt(0).Value : default;
    }

    /// <summary>
    /// Removes and returns the only element of a collection that satisfies a specified condition or a default value if no such element exists;
    /// </summary>
    public static TSource? PopSingleOrDefault<TSource>(this IList<TSource> source, TSource item)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        return source.PopSingleOrDefault(x => Equals(x, item));
    }

    /// <summary>
    /// Removes and returns the only element of a collection that satisfies a specified condition or a default value if no such element exists;
    /// </summary>
    public static TSource? PopSingleOrDefault<TSource>(this IList<TSource> source, Func<TSource, bool> match)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (match == null) throw new ArgumentNullException(nameof(match));
        var indexes = source.IndexesOf(match);
        if (indexes.Count > 1) throw new InvalidOperationException("The collection contains more than one element");
        var index = indexes.TryGetSingle();
        return index.IsSuccess ? source.TryPopAt(index.Value).Value : default;
    }
}