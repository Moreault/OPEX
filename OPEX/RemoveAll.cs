namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Removes all occurences of item from collection.
    /// </summary>
    public static void RemoveAll<TSource>(this ICollection<TSource> source, TSource item)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        source.RemoveAll(x => Equals(x, item));
    }

    /// <summary>
    /// Removes all occurences of lambda from collection.
    /// </summary>
    public static void RemoveAll<TSource>(this ICollection<TSource> source, Func<TSource, bool> match)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (source is TSource[]) throw new NotSupportedException($"The {nameof(RemoveAll)} method does not support arrays");
        
        var occurences = source.Count(match);
        for (var i = 0; i < occurences; i++)
            source.Remove(source.First(match));
    }
}