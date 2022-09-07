namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static IReadOnlyList<int> IndexesOf<T>(this IEnumerable<T> collection, T? item) => collection.IndexesOf(x => Equals(x, item));

    public static IReadOnlyList<int> IndexesOf<T>(this IEnumerable<T> collection, Func<T, bool> match)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (match == null) throw new ArgumentNullException(nameof(match));
        var list = collection as IList<T> ?? collection.ToArray();

        if (list.Count == 0)
            return Array.Empty<int>();

        var output = new List<int>();
        for (var i = 0; i < list.Count; i++)
            if (match(list[i]))
                output.Add(i);

        return output;
    }
}