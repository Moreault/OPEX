namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Whether or not the index is between 0 and source's last index.
    /// </summary>
    public static bool IsWithinRange<TSource>(this IEnumerable<TSource> source, int index)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        var list = source as IList<TSource> ?? source.ToArray();
        return index >= 0 && index <= list.LastIndex();
    }
}