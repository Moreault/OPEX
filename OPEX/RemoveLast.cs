namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static void RemoveLast<T>(this IList<T> collection, T? item)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (collection.IsFixedSize()) throw new NotSupportedException(string.Format(Exceptions.CannotUseMethodBecauseIsFixedSize, nameof(RemoveLast)));

        for (var index = collection.Count - 1; index >= 0; index--)
        {
            if (Equals(collection[index], item))
            {
                collection.RemoveAt(index);
                return;
            }
        }
        throw new Exception(item is null ? Exceptions.NullCouldNotBeRemoved : string.Format(Exceptions.ItemCouldNotBeRemoved, item));
    }

    public static void RemoveLast<T>(this IList<T> collection, Func<T, bool> match)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (match == null) throw new ArgumentNullException(nameof(match));
        if (collection.IsFixedSize()) throw new NotSupportedException(string.Format(Exceptions.CannotUseMethodBecauseIsFixedSize, nameof(RemoveLast)));

        for (var index = collection.Count - 1; index >= 0; index--)
        {
            var item = collection[index];
            if (match(item))
            {
                collection.RemoveAt(index);
                return;
            }
        }

        throw new Exception(Exceptions.PredicateItemCouldNotBeRemoved);
    }
}