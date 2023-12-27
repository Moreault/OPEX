namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Returns true if they are both null or contain the same elements regardless of order.
    /// </summary>
    public static bool UnorderedEqualOrNull<TSource>(this IEnumerable<TSource>? first, IEnumerable<TSource>? second, IEqualityComparer<TSource>? comparer = null)
    {
        if (first is null && second is null) return true;
        if (first is not null && second is null || first is null && second is not null) return false;

        var enumeratedSecond = second as IList<TSource> ?? second!.ToList();

        using var enumerator = first!.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (!enumeratedSecond.Contains(enumerator.Current, comparer))
                return false;
        }

        return true;
    }
}