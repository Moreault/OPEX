namespace OPEX.Tests;

[TestClass]
public sealed class SplittedTests : RecordTester<Splitted<Dummy>>
{
    [TestMethod]
    public void ValueEquality() => Ensure.ValueEquality<Splitted<Dummy>>();

    [TestMethod]
    public void ConsistentHashCode() => Ensure.ConsistentHashCode<Splitted<Dummy>>();
}