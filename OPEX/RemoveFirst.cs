namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static void RemoveFirst<T>(this IList<T> source, T item)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (source.IsFixedSize()) throw new NotSupportedException(string.Format(Exceptions.CannotUseMethodBecauseIsFixedSize, nameof(RemoveFirst)));

        var wasRemoved = source.Remove(item);
        if (!wasRemoved)
            throw new Exception(item is null ? Exceptions.NullCouldNotBeRemoved : string.Format(Exceptions.ItemCouldNotBeRemoved, item));
    }

    public static void RemoveFirst<T>(this IList<T> source, Func<T, bool> predicate)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));
        if (source.IsFixedSize()) throw new NotSupportedException(string.Format(Exceptions.CannotUseMethodBecauseIsFixedSize, nameof(RemoveFirst)));

        for (var index = 0; index < source.Count; index++)
        {
            var item = source[index];
            if (predicate(item))
            {
                source.RemoveAt(index);
                return;
            }
        }

        throw new Exception(Exceptions.PredicateItemCouldNotBeRemoved);
    }
}