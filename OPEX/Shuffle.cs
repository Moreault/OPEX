namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Reorganizes the collection in a random order.
    /// </summary>
    public static void Shuffle<T>(this IList<T> collection)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        var n = collection.Count;
        var random = new Random();
        while (n > 1)
        {
            n--;
            var k = random.Next(n + 1);
            collection.Swap(n, k);
        }
    }
}