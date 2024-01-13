namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Returns a new equivalent collection reorganized in a random order.
    /// </summary>
    public static IReadOnlyList<T> ToShuffled<T>(this IEnumerable<T> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        var list = source.ToArray();
        list.Shuffle();
        return list;
    }
}