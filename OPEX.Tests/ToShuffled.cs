namespace OPEX.Tests;

[TestClass]
public sealed class ToShuffledWithArray : ToShuffledTester<Dummy[]>
{


}

[TestClass]
public sealed class ToShuffledWithList : ToShuffledTester<List<Dummy>>
{


}

public abstract class ToShuffledTester<TCollection> : Tester where TCollection : class, IEnumerable<Dummy>
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
        var source = new List<Dummy>().To<TCollection, Dummy>();

        //Act
        var result = source.ToShuffled();

        //Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void WhenSourceIsNotEmpty_ReturnEquivalentCollectionButInDifferentOrder()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>(10).ToList().To<TCollection, Dummy>();

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
        var source = Fixture.CreateMany<Dummy>(10).ToList().To<TCollection, Dummy>();
        var original = source.ToList();

        //Act
        source.ToShuffled();

        //Assert
        source.Should().ContainInOrder(original);
    }
}