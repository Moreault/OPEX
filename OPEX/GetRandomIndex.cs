namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static int GetRandomIndex<T>(this IEnumerable<T> collection)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        var list = collection as IList<T> ?? collection.ToArray();
        return list.Count == 0 ? -1 : new Random().Next(list.Count);
    }
}