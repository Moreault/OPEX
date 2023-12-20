namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Returns the index of the only occurence of item in collection or throws an exception if there are none or multiple.
    /// </summary>
    public static int SingleIndexOf<TSource>(this IEnumerable<TSource> source, TSource? item) => source.SingleIndexOf(x => Equals(x, item));

    /// <summary>
    /// Returns the index of the only occurence of lambda in collection or throws an exception if there are none or multiple.
    /// </summary>
    public static int SingleIndexOf<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> match)
    {
        return source.IndexesOf(match).Single();

        if (source == null) throw new ArgumentNullException(nameof(source));
        if (match == null) throw new ArgumentNullException(nameof(match));
        var list = source as IList<TSource> ?? source.ToArray();

        var isFound = false;
        var index = -1;
        for (var i = 0; i < list.Count; i++)
            if (match(list[i]))
            {
                if (isFound) throw new InvalidOperationException("Sequence contains more than one matching element");
                isFound = true;
                index = i;
            }

        if (index < 0) throw new InvalidOperationException("Sequence contains no matching element");
        return index;
    }
}