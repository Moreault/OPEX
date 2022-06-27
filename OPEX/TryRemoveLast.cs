namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static void TryRemoveLast<T>(this IList<T> collection, T? item)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        collection.TryRemoveLast(x => Equals(x, item));
    }

    public static void TryRemoveLast<T>(this IList<T> collection, Func<T, bool> match)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (match == null) throw new ArgumentNullException(nameof(match));
        if (collection.IsFixedSize()) throw new NotSupportedException(string.Format(Exceptions.CannotUseMethodBecauseIsFixedSize, nameof(TryRemoveLast)));

        for (var index = collection.Count - 1; index >= 0; index--)
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