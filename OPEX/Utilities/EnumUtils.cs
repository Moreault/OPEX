namespace ToolBX.OPEX.Utilities;

public static class EnumUtils
{
    public static IReadOnlyList<T> ToList<T>() where T : Enum => ((T[])Enum.GetValues(typeof(T))).ToList();
}