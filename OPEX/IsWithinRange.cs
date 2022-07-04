namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Whether or not the index is between 0 and source's last index.
    /// </summary>
    public static bool IsWithinRange<T>(this T[] source, int index) => (source as IList<T>).IsWithinRange(index);

    /// <summary>
    /// Whether or not the index is between 0 and source's last index.
    /// </summary>
    public static bool IsWithinRange<T>(this List<T> source, int index) => (source as IList<T>).IsWithinRange(index);

    /// <summary>
    /// Whether or not the index is between 0 and source's last index.
    /// </summary>
    public static bool IsWithinRange<T>(this IList<T> source, int index)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        return index >= 0 && index <= source.LastIndex();
    }

    /// <summary>
    /// Whether or not the index is between 0 and source's last index.
    /// </summary>
    public static bool IsWithinRange<T>(this IReadOnlyList<T> source, int index)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        return index >= 0 && index <= source.LastIndex();
    }
}