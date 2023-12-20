namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Removes and returns multiple random items from the collection.
    /// </summary>  
    public static IReadOnlyList<TSource> PopManyRandoms<TSource>(this IList<TSource> source, int count)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
        var indexes = source.GetManyRandomIndexes(count);
        return source.PopAt(indexes);
    }
}