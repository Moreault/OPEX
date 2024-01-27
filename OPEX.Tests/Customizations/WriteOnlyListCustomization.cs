namespace OPEX.Tests.Customizations;

[AutoCustomization]
public sealed class WriteOnlyListCustomization : ListCustomizationBase
{
    public override IEnumerable<Type> Types { get; } = [typeof(WriteOnlyList<>)];

    protected override object Convert<T>(IEnumerable<T> source) => source.ToWriteOnlyList();
}