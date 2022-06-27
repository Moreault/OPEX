namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static int LastIndexOf<T>(this T[] collection, T? item) => ((IList<T>)collection).LastIndexOf(item);
    public static int LastIndexOf<T>(this T[] collection, Func<T, bool> match) => ((IList<T>)collection).LastIndexOf(match);
    public static int LastIndexOf<T>(this IList<T> collection, T? item) => collection.LastIndexOf(x => Equals(x, item));

    public static int LastIndexOf<T>(this IList<T> collection, Func<T, bool> match)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (match == null) throw new ArgumentNullException(nameof(match));

        for (var i = collection.Count - 1; i >= 0; i--)
            if (match(collection[i]))
                return i;

        return -1;
    }

    public static int LastIndexOf<T>(this IReadOnlyList<T> collection, T? item) => collection.LastIndexOf(x => Equals(x, item));

    public static int LastIndexOf<T>(this IReadOnlyList<T> collection, Func<T, bool> match)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (match == null) throw new ArgumentNullException(nameof(match));

        for (var i = collection.Count - 1; i >= 0; i--)
            if (match(collection[i]))
                return i;

        return -1;
    }
}