namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static void TryRemoveFirst<T>(this IList<T> collection, T item)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        collection.Remove(item);
    }

    public static void TryRemoveFirst<T>(this IList<T> collection, Func<T, bool> match)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (match == null) throw new ArgumentNullException(nameof(match));
        if (collection.IsFixedSize()) throw new NotSupportedException(string.Format(Exceptions.CannotUseMethodBecauseIsFixedSize, nameof(TryRemoveFirst)));

        for (var index = 0; index < collection.Count; index++)
        {
            var item = collection[index];
            if (match(item))
            {
                collection.RemoveAt(index);
                return;
            }
        }
    }
}