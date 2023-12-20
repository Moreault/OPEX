﻿namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Removes and returns the first item from collection.
    /// </summary>
    public static TSource? PopFirstOrDefault<TSource>(this IList<TSource> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        return source.TryPopAt(0).Value;
    }

    /// <summary>
    /// Removes and returns the first occurence of item from collection.
    /// </summary>
    public static TSource? PopFirstOrDefault<TSource>(this IList<TSource> source, TSource item)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        var index = source.FirstIndexOf(item);
        return source.TryPopAt(index).Value;
    }

    /// <summary>
    /// Removes and returns the first occurence of lambda from collection.
    /// </summary>
    public static TSource? PopFirstOrDefault<TSource>(this IList<TSource> source, Func<TSource, bool> match)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (match == null) throw new ArgumentNullException(nameof(match));
        var index = source.FirstIndexOf(match);
        return source.TryPopAt(index).Value;
    }
}