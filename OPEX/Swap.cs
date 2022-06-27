namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static void Swap<T>(this IList<T> collection, int currentIndex, int destinationIndex)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (currentIndex < 0 || currentIndex > collection.LastIndex()) throw new ArgumentOutOfRangeException(nameof(currentIndex));
        if (destinationIndex < 0 || destinationIndex > collection.LastIndex()) throw new ArgumentOutOfRangeException(nameof(destinationIndex));

        var currentItem = collection[currentIndex];
        var destinationItem = collection[destinationIndex];
        collection[currentIndex] = destinationItem;
        collection[destinationIndex] = currentItem;
    }
}