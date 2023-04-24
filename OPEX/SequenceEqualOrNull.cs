namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Returns true if they are both null or contain the same elements.
    /// </summary>
    public static bool SequenceEqualOrNull<TSource>(this IEnumerable<TSource>? first, IEnumerable<TSource>? second, IEqualityComparer<TSource>? comparer = null) where TSource : class
    {
        if (first is null && second is null) return true;
        if (first is not null && second is null || first is null && second is not null) return false;
        return first!.SequenceEqual(second!, comparer);
    }
}