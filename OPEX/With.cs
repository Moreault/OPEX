namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Returns a new immutable collection with the specified elements.
    /// </summary>
    public static IReadOnlyList<TSource> With<TSource>(this IEnumerable<TSource> source, params TSource?[] items) => source.With(items as IEnumerable<TSource?>);

    /// <summary>
    /// Returns a new immutable collection with the specified elements.
    /// </summary>
    public static IReadOnlyList<TSource> With<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource?> items)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        return source.Concat(items!).ToImmutableList();
    }
}