namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static IReadOnlyList<int> IndexesOf<T>(this T[] collection, T? item) => ((IList<T>)collection).IndexesOf(x => Equals(x, item));
    public static IReadOnlyList<int> IndexesOf<T>(this T[] collection, Func<T, bool> match) => ((IList<T>)collection).IndexesOf(match);
    public static IReadOnlyList<int> IndexesOf<T>(this List<T> collection, T? item) => ((IList<T>)collection).IndexesOf(x => Equals(x, item));
    public static IReadOnlyList<int> IndexesOf<T>(this List<T> collection, Func<T, bool> match) => ((IList<T>)collection).IndexesOf(match);
    public static IReadOnlyList<int> IndexesOf<T>(this IList<T> collection, T? item) => collection.IndexesOf(x => Equals(x, item));

    public static IReadOnlyList<int> IndexesOf<T>(this IList<T> collection, Func<T, bool> match)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (match == null) throw new ArgumentNullException(nameof(match));

        if (collection.Count == 0)
            return Array.Empty<int>();

        var output = new List<int>();
        for (var i = 0; i < collection.Count; i++)
            if (match(collection[i]))
                output.Add(i);

        return output;
    }

    public static IReadOnlyList<int> IndexesOf<T>(this IReadOnlyList<T> collection, T? item) => collection.IndexesOf(x => Equals(x, item));

    public static IReadOnlyList<int> IndexesOf<T>(this IReadOnlyList<T> collection, Func<T, bool> match)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (match == null) throw new ArgumentNullException(nameof(match));

        if (collection.Count == 0) 
            return Array.Empty<int>();

        var output = new List<int>();
        for (var i = 0; i < collection.Count; i++)
            if (match(collection[i]))
                output.Add(i);

        return output;
    }
}