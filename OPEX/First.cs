namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    public static TSource First<TSource>(this IEnumerable<TSource> source, string customMessage)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        try
        {
            return source.First();
        }
        catch (InvalidOperationException innerException)
        {
            throw new InvalidOperationException(customMessage, innerException);
        }
    }

    public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, string customMessage)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        try
        {
            return source.First(predicate);
        }
        catch (InvalidOperationException innerException)
        {
            throw new InvalidOperationException(customMessage, innerException);
        }
    }
}