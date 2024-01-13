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
    }
}