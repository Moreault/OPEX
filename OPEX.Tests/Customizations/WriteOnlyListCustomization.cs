namespace OPEX.Tests.Customizations;

[AutoCustomization]
public sealed class WriteOnlyListCustomization : ListCustomizationBase
{
    protected override IEnumerable<Type> Types { get; } = [typeof(WriteOnlyList<>)];

    protected override object Convert<T>(IEnumerable<T> source) => source.ToWriteOnlyList();
}