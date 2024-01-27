namespace OPEX.Tests;

[TestClass]
public sealed class ToShuffledWithArray : ToShuffledTester<Garbage[]>
{


}

[TestClass]
public sealed class ToShuffledWithList : ToShuffledTester<List<Garbage>>
{


}

public abstract class ToShuffledTester<TCollection> : TestBase where TCollection : class, IEnumerable<Garbage>
{
    [TestMethod]
    public void WhenSourceIsNull_Throw()
    {
        //Arrange
        TCollection source = null!;

        //Act
        var action = () => source.ToShuffled();

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenSourceIsEmpty_ReturnEmpty()
    {
        //Arrange
        var source = new List<Garbage>().To<TCollection, Garbage>();

        //Act
        var result = source.ToShuffled();

        //Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void WhenSourceIsNotEmpty_ReturnEquivalentCollectionButInDifferentOrder()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>(10).ToList().To<TCollection, Garbage>();

        //Act
        var result = source.ToShuffled();

        //Assert
        result.Should().BeEquivalentTo(source);
        result.Should().NotContainInOrder(source);
    }

    [TestMethod]
    public void WhenSourceIsNotEmpty_SourceOrderShouldNotBeModified()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>(10).ToList().To<TCollection, Garbage>();
        var original = source.ToList();

        //Act
        source.ToShuffled();

        //Assert
        source.Should().ContainInOrder(original);
    }
}