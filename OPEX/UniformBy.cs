namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Returns any of the duplicate occurences in collection.
    /// </summary>
    public static TSource? UniformByOrDefault<TSource, TProperty>(this IEnumerable<TSource> source, Func<TSource, TProperty> selector, IEqualityComparer<TProperty>? comparer = null)
    {
        return source.TryGetUniformBy(selector, comparer).Value;
    }

    /// <summary>
    /// Returns any of the duplicate occurences in collection.
    /// </summary>
    public static TSource UniformBy<TSource, TProperty>(this IEnumerable<TSource> source, Func<TSource, TProperty> selector, IEqualityComparer<TProperty>? comparer = null)
    {
        var result = source.TryGetUniformBy(selector, comparer);
        if (!result.IsSuccess) throw new InvalidOperationException(Exceptions.CannotUseUniformOnEmptyCollection);
        return result.Value!;
    }

    private static Result<TSource> TryGetUniformBy<TSource, TProperty>(this IEnumerable<TSource> source, Func<TSource, TProperty> selector, IEqualityComparer<TProperty>? comparer = null)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (selector == null) throw new ArgumentNullException(nameof(selector));
        comparer ??= EqualityComparer<TProperty>.Default;

        using var enumerator = source.GetEnumerator();

        if (!enumerator.MoveNext())
            return Result<TSource>.Failure();

        var first = enumerator.Current;
        var firstKey = selector(enumerator.Current);

        while (enumerator.MoveNext())
        {
            if (!comparer.Equals(firstKey, selector(enumerator.Current)))
                throw new InvalidOperationException(Exceptions.UniformFoundNonDuplicates);
        }

        return Result<TSource>.Success(first);
    }
}