namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static int LastIndex<T>(this IEnumerable<T> collection)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        var list = collection as IList<T> ?? collection.ToArray();
        return list.Count - 1;
    }
}