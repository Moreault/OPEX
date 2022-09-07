namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static int FirstIndexOf<T>(this IEnumerable<T> collection, T? item) => collection.FirstIndexOf(x => Equals(x, item));

    public static int FirstIndexOf<T>(this IEnumerable<T> collection, Func<T, bool> match)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (match == null) throw new ArgumentNullException(nameof(match));
        var list = collection as IList<T> ?? collection.ToArray();

        for (var i = 0; i < list.Count; i++)
            if (match(list[i]))
                return i;

        return -1;
    }
}