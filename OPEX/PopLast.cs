namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Removes and returns the last item from collection.
    /// </summary>
    public static TSource PopLast<TSource>(this IList<TSource> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        return source.PopAt(source.LastIndex());
    }

    /// <summary>
    /// Removes and returns the last occurence of item from collection.
    /// </summary>
    public static TSource PopLast<TSource>(this IList<TSource> source, TSource item)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        var index = source.LastIndexOf(item);
        return source.PopAt(index);
    }

    /// <summary>
    /// Removes and returns the last occurence of lambda from collection.
    /// </summary>
    public static TSource PopLast<TSource>(this IList<TSource> source, Func<TSource, bool> match)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (match == null) throw new ArgumentNullException(nameof(match));
        var index = source.LastIndexOf(match);
        return source.PopAt(index);
    }
}