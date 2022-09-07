namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static T? GetRandom<T>(this IEnumerable<T> collection)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        var list = collection as IList<T> ?? collection.ToArray();
        var randomIndex = list.GetRandomIndex();
        return randomIndex < 0 ? default : list[randomIndex];
    }
}