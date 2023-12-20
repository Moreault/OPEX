namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static void RemoveLast<TSource>(this IList<TSource> source, TSource? item)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (source.IsFixedSize()) throw new NotSupportedException(string.Format(Exceptions.CannotUseMethodBecauseIsFixedSize, nameof(RemoveLast)));

        for (var index = source.Count - 1; index >= 0; index--)
        {
            if (Equals(source[index], item))
            {
                source.RemoveAt(index);
                return;
            }
        }
        throw new Exception(item is null ? Exceptions.NullCouldNotBeRemoved : string.Format(Exceptions.ItemCouldNotBeRemoved, item));
    }

    public static void RemoveLast<TSource>(this IList<TSource> source, Func<TSource, bool> match)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (match == null) throw new ArgumentNullException(nameof(match));
        if (source.IsFixedSize()) throw new NotSupportedException(string.Format(Exceptions.CannotUseMethodBecauseIsFixedSize, nameof(RemoveLast)));

        for (var index = source.Count - 1; index >= 0; index--)
        {
            var item = source[index];
            if (match(item))
            {
                source.RemoveAt(index);
                return;
            }
        }

        throw new Exception(Exceptions.PredicateItemCouldNotBeRemoved);
    }
}