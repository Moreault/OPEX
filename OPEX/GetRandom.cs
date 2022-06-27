namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static T? GetRandom<T>(this IEnumerable<T> collection)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (collection is IReadOnlyList<T> roList) return roList.GetRandom();
        if (collection is IList<T> list) return list.GetRandom();
        return collection.ToArray().GetRandom();
    }

    private static T? GetRandom<T>(this List<T> collection) => (collection as IList<T>).GetRandom();

    private static T? GetRandom<T>(this T[] collection) => (collection as IList<T>).GetRandom();

    private static T? GetRandom<T>(this IList<T> collection)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        var randomIndex = collection.GetRandomIndex();
        return randomIndex < 0 ? default : collection[randomIndex];
    }

    private static T? GetRandom<T>(this IReadOnlyList<T> collection)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        var randomIndex = collection.GetRandomIndex();
        return randomIndex < 0 ? default : collection[randomIndex];
    }
}