namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Returns a random element from collection.
    /// </summary>
    public static T GetRandom<T>(this IEnumerable<T> source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        var list = source as IList<T> ?? source.ToArray();
        var randomIndex = list.GetRandomIndex();
        return randomIndex < 0 ? default! : list[randomIndex];
    }
}