using System.Text.Json;
using ToolBX.Reflection4Humans.Extensions;

namespace OPEX.Tests;

public abstract class TestBase
{
    protected Dummy Dummy { get; private set; } = null!;

    protected JsonSerializerOptions JsonSerializerOptions => _jsonSerializerOptions.Value;
    private Lazy<JsonSerializerOptions> _jsonSerializerOptions = null!;

    [TestInitialize]
    public void TestInitializeBase()
    {
        Dummy = new();
        _jsonSerializerOptions = new(() => new JsonSerializerOptions());
        TestInitialize();
    }

    protected virtual void TestInitialize()
    {

    }
}

public abstract class RecordTestBase<T> : TestBase where T : class
{
    [TestMethod]
    public void WhenUsingPrivateCloningConstructor_ThenCloneObject()
    {
        //Arrange
        var instance = Dummy.Create<T>();

        //This constructor for a sealed record is private but it's protected for a non-sealed record
        var constructor = typeof(T).GetSingleConstructor(x => (x.IsPrivate || x.IsProtected()) && x.IsInstance() && x.HasParameters<T>());

        //Act
        var result = (T)constructor.Invoke(new object?[] { instance });

        //Assert
        Assert.IsFalse(ReferenceEquals(instance, result));
    }

    [TestMethod]
    public void Ensure_ValueEquality() => Ensure.ValueEquality<T>(JsonSerializerOptions);

    [TestMethod]
    public void Ensure_ConsistentHashCode() => Ensure.ValueHashCode<T>(JsonSerializerOptions);
}
