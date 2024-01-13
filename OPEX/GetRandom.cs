namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Returns a random element from collection.
    /// </summary>
    public static TSource GetRandom<TSource>(this IEnumerable<TSource> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        var list = source as IList<TSource> ?? source.ToArray();
        var randomIndex = list.GetRandomIndex();
        return randomIndex < 0 ? default! : list[randomIndex];
    }
}