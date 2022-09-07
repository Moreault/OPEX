namespace ToolBX.OPEX;

public record Splitted<T>
{
    /// <summary>
    /// What remains of the original items without the excluded items.
    /// </summary>
    public IReadOnlyList<T> Remaining { get; init; } = Array.Empty<T>();

    /// <summary>
    /// Items that were excluded from the split.
    /// </summary>
    public IReadOnlyList<T> Excluded { get; init; } = Array.Empty<T>();
}