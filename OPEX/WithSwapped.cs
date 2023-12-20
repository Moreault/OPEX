namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Returns a new immutable collection with the elements at the specified index swapped.
    /// </summary>
    public static IReadOnlyList<TSource> WithSwapped<TSource>(this IEnumerable<TSource> source, int currentIndex, int destinationIndex)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        var content = source.ToList();
        content.Swap(currentIndex, destinationIndex);
        return content.ToImmutableList();
    }
}