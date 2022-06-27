namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static int LastIndex<T>(this T[] collection) => LastIndex((IList<T>)collection);

    public static int LastIndex<T>(this List<T> collection) => LastIndex((IList<T>)collection);

    public static int LastIndex<T>(this IList<T> collection)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (collection is IReadOnlyList<T> list)
            return list.LastIndex();
        return collection.ToArray().LastIndex();
    }

    public static int LastIndex<T>(this IReadOnlyList<T> collection)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        return collection.Count - 1;
    }
}