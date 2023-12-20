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

        var remaining = source.ToList();
        var excluded = remaining.PopAll(match);

        return new Splitted<TSource>
        {
            Remaining = remaining,
            Excluded = excluded
        };
    }
}