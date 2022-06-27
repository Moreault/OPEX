namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static IReadOnlyList<T> GetManyRandoms<T>(this IEnumerable<T> collection, int numberOfElements)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (collection is IReadOnlyList<T> roList) return roList.GetManyRandoms(numberOfElements);
        if (collection is IList<T> list) return list.GetManyRandoms(numberOfElements);
        return collection.ToArray().GetManyRandoms(numberOfElements);
    }

    private static IReadOnlyList<T> GetManyRandoms<T>(this T[] collection, int numberOfElements) => ((IReadOnlyList<T>)collection).GetManyRandoms(numberOfElements);

    private static IReadOnlyList<T> GetManyRandoms<T>(this List<T> collection, int numberOfElements) => ((IReadOnlyList<T>)collection).GetManyRandoms(numberOfElements);

    private static IReadOnlyList<T> GetManyRandoms<T>(this IList<T> collection, int numberOfElements)
    {
        if (collection is T[] arr) return arr.GetManyRandoms(numberOfElements);
        if (collection is List<T> list) return list.GetManyRandoms(numberOfElements);
        return collection.ToArray().GetManyRandoms(numberOfElements);
    }

    private static IReadOnlyList<T> GetManyRandoms<T>(this IReadOnlyList<T> collection, int numberOfElements)
    {
        if (numberOfElements < 0) throw new ArgumentException(string.Format(Exceptions.CannotGetManyRandomsBecauseNumberNegative, numberOfElements));

        numberOfElements = Math.Clamp(numberOfElements, 0, collection.Count);
        if (numberOfElements == 0) return Array.Empty<T>();
        if (numberOfElements == collection.Count) return collection;

        var remaining = collection.ToList();

        var output = new List<T>();
        for (var i = 0; i < numberOfElements; i++)
        {
            var random = remaining.GetRandom()!;
            remaining.Remove(random);
            output.Add(random);
        }
        return output;
    }
}