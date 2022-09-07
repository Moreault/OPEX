namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static IReadOnlyList<T> GetManyRandoms<T>(this IEnumerable<T> collection, int numberOfElements)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (numberOfElements < 0) throw new ArgumentException(string.Format(Exceptions.CannotGetManyRandomsBecauseNumberNegative, numberOfElements));
        var list = collection.ToList();

        numberOfElements = Math.Clamp(numberOfElements, 0, list.Count);
        if (numberOfElements == 0) return Array.Empty<T>();
        if (numberOfElements == list.Count) return list;

        var output = new List<T>();
        for (var i = 0; i < numberOfElements; i++)
        {
            var random = list.GetRandom()!;
            list.Remove(random);
            output.Add(random);
        }
        return output;
    }
}