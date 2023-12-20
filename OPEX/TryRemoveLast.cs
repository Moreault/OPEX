namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static void TryRemoveLast<TSource>(this IList<TSource> source, TSource? item)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        source.TryRemoveLast(x => Equals(x, item));
    }

    public static void TryRemoveLast<TSource>(this IList<TSource> source, Func<TSource, bool> match)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (match == null) throw new ArgumentNullException(nameof(match));
        if (source.IsFixedSize()) throw new NotSupportedException(string.Format(Exceptions.CannotUseMethodBecauseIsFixedSize, nameof(TryRemoveLast)));

        for (var index = source.Count - 1; index >= 0; index--)
        {
            var item = source[index];
            if (match(item))
            {
                source.RemoveAt(index);
                return;
            }
        }

    }
}