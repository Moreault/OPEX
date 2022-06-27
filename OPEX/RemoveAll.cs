namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Removes all occurences of item from collection.
    /// </summary>
    public static void RemoveAll<T>(this ICollection<T> collection, T item)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        collection.RemoveAll(x => Equals(x, item));
    }

    /// <summary>
    /// Removes all occurences of lambda from collection.
    /// </summary>
    public static void RemoveAll<T>(this ICollection<T> collection, Func<T, bool> match)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (collection is T[]) throw new NotSupportedException($"The {nameof(RemoveAll)} method does not support arrays");
        
        var occurences = collection.Count(match);
        for (var i = 0; i < occurences; i++)
            collection.Remove(collection.First(match));
    }
}