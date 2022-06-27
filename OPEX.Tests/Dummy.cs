namespace OPEX.Tests;

public record Dummy
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public long Experience { get; init; }
    public short Level { get; init; }
}