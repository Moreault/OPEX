namespace ToolBX.OPEX;

public sealed record Splitted<T>
{
    /// <summary>
    /// What remains of the original items without the excluded items.
    /// </summary>
    public IReadOnlyList<T> Remaining { get; init; } = Array.Empty<T>();

    /// <summary>
    /// Items that were excluded from the split.
    /// </summary>
    public IReadOnlyList<T> Excluded { get; init; } = Array.Empty<T>();

    public bool Equals(Splitted<T>? other) => other != null && Remaining.SequenceEqualOrNull(other.Remaining) && Excluded.SequenceEqualOrNull(other.Excluded);

    public override int GetHashCode()
    {
        var hash = new HashCode();

        hash.Add(Remaining.Count);
        foreach (var item in Remaining)
            hash.Add(item);

        hash.Add(Excluded.Count);
        foreach (var item in Excluded)
            hash.Add(item);

        return hash.ToHashCode();
    }
}