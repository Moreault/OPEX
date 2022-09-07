namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Splits the collection into two according to the predicate.
    /// </summary>
    public static Splitted<TSource> Split<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> match)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (match == null) throw new ArgumentNullException(nameof(match));

        var remaining = new List<TSource>();
        var excluded = new List<TSource>();

        foreach (var item in source)
        {
            if (match(item))
                excluded.Add(item);
            else
                remaining.Add(item);
        }

        return new Splitted<TSource>
        {
            Remaining = remaining,
            Excluded = excluded
        };
    }
}