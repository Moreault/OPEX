namespace ToolBX.OPEX.Utilities;

public static class EnumUtils
{
    public static IReadOnlyList<T> ToList<T>() where T : struct, Enum => Enum.GetValues<T>().ToList();
}