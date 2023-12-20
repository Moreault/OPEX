namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static void TryRemoveFirst<TSource>(this IList<TSource> source, TSource item)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        source.Remove(item);
    }

    public static void TryRemoveFirst<TSource>(this IList<TSource> source, Func<TSource, bool> match)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (match == null) throw new ArgumentNullException(nameof(match));
        if (source.IsFixedSize()) throw new NotSupportedException(string.Format(Exceptions.CannotUseMethodBecauseIsFixedSize, nameof(TryRemoveFirst)));

        for (var index = 0; index < source.Count; index++)
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