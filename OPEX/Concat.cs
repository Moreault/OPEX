namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Returns the concatenation of two sequences.
    /// </summary>
    public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> source, params TSource[] items) => source.Concat((IEnumerable<TSource>)items);
}