namespace ToolBX.OPEX;

public static partial class CollectionExtensions
{
    /// <summary>
    /// Returns any of the duplicate occurences in collection.
    /// </summary>
    public static TSource Uniform<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource>? comparer = null) => source.Uniform(x => x, comparer);

    /// <summary>
    /// Returns any of the duplicate occurences in collection or default if there are no occurences.
    /// </summary>
    public static TSource? UniformOrDefault<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource>? comparer = null) => source.UniformOrDefault(x => x, comparer);

    /// <summary>
    /// Returns any of the duplicate occurences in collection.
    /// </summary>
    public static TProperty Uniform<TSource, TProperty>(this IEnumerable<TSource> source, Func<TSource, TProperty> selector, IEqualityComparer<TProperty>? comparer = null)
    {
        var result = source.TryGetUniform(selector, comparer);
        if (!result.IsSuccess) throw new InvalidOperationException(Exceptions.CannotUseUniformOnEmptyCollection);
        return result.Value!;
    }

    /// <summary>
    /// Returns any of the duplicate occurences in collection or default if there are no occurences.
    /// </summary>
    public static TProperty? UniformOrDefault<TSource, TProperty>(this IEnumerable<TSource> source, Func<TSource, TProperty> selector, IEqualityComparer<TProperty>? comparer = null) => source.TryGetUniform(selector, comparer).Value;

    private static TryGetResult<TProperty> TryGetUniform<TSource, TProperty>(this IEnumerable<TSource> source, Func<TSource, TProperty> selector, IEqualityComparer<TProperty>? comparer = null)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (selector == null) throw new ArgumentNullException(nameof(selector));
        comparer ??= EqualityComparer<TProperty>.Default;

        using var enumerator = source.GetEnumerator();

        if (!enumerator.MoveNext())
            return TryGetResult<TProperty>.Failure;

        var first = selector(enumerator.Current);

        while (enumerator.MoveNext())
        {
            if (!comparer.Equals(first, selector(enumerator.Current)))
                throw new InvalidOperationException(Exceptions.UniformFoundDuplicates);
        }

        return new TryGetResult<TProperty>(true, first);
    }
}