namespace OPEX.Tests;

[TestClass]
public sealed class ToStack : Tester
{
    [TestMethod]
    public void WhenIsNull_Throw()
    {
        //Arrange
        IEnumerable<Garbage> source = null!;

        //Act
        var action = () => source.ToStack();

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenIsEmpty_ReturnEmptyStack()
    {
        //Arrange
        var source = Array.Empty<Garbage>();

        //Act
        var result = source.ToStack();

        //Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void WhenContainsItems_ReturnInSameOrder()
    {
        //Arrange
        var source = Dummy.Create<List<Garbage>>();

        //Act
        var result = source.ToStack();

        //Assert
        result.Should().BeEquivalentTo(source);
    }
}