namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Returns a new immutable collection without the element at the specified index.
    /// </summary>
    public static IReadOnlyList<TSource> WithoutAt<TSource>(this IEnumerable<TSource> source, int index)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        var content = source.ToList();
        content.RemoveAt(index);
        return content.ToImmutableList();
    }
}