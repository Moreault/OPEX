namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Whether or not the index is between 0 and source's last index.
    /// </summary>
    public static bool IsWithinRange<T>(this IEnumerable<T> source, int index)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        var list = source as IList<T> ?? source.ToArray();
        return index >= 0 && index <= list.LastIndex();
    }
}