namespace OPEX.Tests;

[TestClass]
public sealed class SplittedTests : RecordTester<Splitted<Dummy>>
{
    [TestMethod]
    public void Ensure_ValueEquality() => Ensure.ValueEquality<Splitted<Dummy>>();

    [TestMethod]
    public void Ensure_ValueHashCode() => Ensure.ValueHashCode<Splitted<Dummy>>();
}