namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static int GetRandomIndex<T>(this T[] collection) => ((IList<T>)collection).GetRandomIndex();
    public static int GetRandomIndex<T>(this List<T> collection) => ((IList<T>)collection).GetRandomIndex();

    public static int GetRandomIndex<T>(this IList<T> collection)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        return collection.Count == 0 ? -1 : new Random().Next(collection.Count);
    }

    public static int GetRandomIndex<T>(this IReadOnlyList<T> collection)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        return collection.Count == 0 ? -1 : new Random().Next(collection.Count);
    }
}