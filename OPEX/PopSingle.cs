namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Removes and returns a single item from collection or throws an exception if there are zero or multiple occurences.
    /// </summary>
    public static TSource PopSingle<TSource>(this IList<TSource> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (source.Count != 1) throw new InvalidOperationException("The collection does not contain exactly one element.");
        return source.PopAt(0);
    }

    /// <summary>
    /// Removes and returns the first occurence of item from collection.
    /// </summary>
    public static TSource PopSingle<TSource>(this IList<TSource> source, TSource item)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        var index = source.SingleIndexOf(item);
        return source.PopAt(index);
    }

    /// <summary>
    /// Removes and returns the first occurence of lambda from collection.
    /// </summary>
    public static TSource PopSingle<TSource>(this IList<TSource> source, Func<TSource, bool> match)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (match == null) throw new ArgumentNullException(nameof(match));
        var index = source.SingleIndexOf(match);
        return source.PopAt(index);
    }
}