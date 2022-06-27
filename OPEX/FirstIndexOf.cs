namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static int FirstIndexOf<T>(this T[] collection, T? item) => ((IList<T>)collection).FirstIndexOf(item);
    public static int FirstIndexOf<T>(this T[] collection, Func<T, bool> match) => ((IList<T>)collection).FirstIndexOf(match);
    public static int FirstIndexOf<T>(this List<T> collection, T? item) => ((IList<T>)collection).FirstIndexOf(item);
    public static int FirstIndexOf<T>(this List<T> collection, Func<T, bool> match) => ((IList<T>)collection).FirstIndexOf(match);

    public static int FirstIndexOf<T>(this IList<T> collection, T? item) => collection.FirstIndexOf(x => Equals(x, item));

    public static int FirstIndexOf<T>(this IList<T> collection, Func<T, bool> match)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (match == null) throw new ArgumentNullException(nameof(match));

        for (var i = 0; i < collection.Count; i++)
            if (match(collection[i]))
                return i;

        return -1;
    }

    public static int FirstIndexOf<T>(this IReadOnlyList<T> collection, T? item) => collection.FirstIndexOf(x => Equals(x, item));

    public static int FirstIndexOf<T>(this IReadOnlyList<T> collection, Func<T, bool> match)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (match == null) throw new ArgumentNullException(nameof(match));

        for (var i = 0; i < collection.Count; i++)
            if (match(collection[i]))
                return i;

        return -1;
    }
}