namespace OPEX.Tests;

[TestClass]
public sealed class FirstIndexOfWithArrayOfDummyTests : FirstIndexOfTester<Garbage[]>
{

}

[TestClass]
public sealed class FirstIndexOfWithListOfDummyTests : FirstIndexOfTester<List<Garbage>>
{

}

[TestClass]
public sealed class FirstIndexOfWithWriteOnlyListOfDummyTests : FirstIndexOfTester<WriteOnlyList<Garbage>>
{

}

[TestClass]
public sealed class FirstIndexOfWithImmutableListOfDummyTests : FirstIndexOfTester<ImmutableList<Garbage>>
{

}

public abstract class FirstIndexOfTester<TCollection> : TestBase where TCollection : class, IList<Garbage>
{
    [TestMethod]
    public void WhenUsingItemAndCollectionIsNull_Throw()
    {
        //Arrange
        TCollection source = null!;
        var item = Dummy.Create<Garbage>();

        //Act
        var action = () => source.FirstIndexOf(item);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenUsingLambaAndCollectionIsNull_Throw()
    {
        //Arrange
        TCollection source = null!;
        var item = Dummy.Create<Func<Garbage, bool>>();

        //Act
        var action = () => source.FirstIndexOf(item);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenUsingLambaAndLambdaIsNull_Throw()
    {
        //Arrange
        var source = Dummy.Create<TCollection>();
        Func<Garbage, bool> match = null!;

        //Act
        var action = () => source.FirstIndexOf(match);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(match));
    }

    [TestMethod]
    public void WhenUsingItemAndItemIsNotInCollection_ReturnMinusOne()
    {
        //Arrange
        var source = Dummy.Create<TCollection>();

        //Act
        var result = source.FirstIndexOf(Dummy.Create<Garbage>());

        //Assert
        result.Should().Be(-1);
    }

    [TestMethod]
    public void WhenUsingLambdaAndItemIsNotInCollection_ReturnMinusOne()
    {
        //Arrange
        var source = Dummy.Create<TCollection>();

        //Act
        var result = source.FirstIndexOf(x => x.Name == Dummy.Create<string>());

        //Assert
        result.Should().Be(-1);
    }

    [TestMethod]
    public void WhenUsingItemAndItemIsInCollectionOnce_ReturnItemIndex()
    {
        //Arrange
        var source = Dummy.Create<TCollection>();
        var itemIndex = source.GetRandomIndex();
        var item = source[itemIndex];

        //Act
        var result = source.FirstIndexOf(item);

        //Assert
        result.Should().Be(itemIndex);
    }

    [TestMethod]
    public void WhenUsingLambaAndItemIsInCollectionOnce_ReturnIndex()
    {
        //Arrange
        var source = Dummy.Create<TCollection>();
        var itemIndex = source.GetRandomIndex();

        //Act
        var result = source.FirstIndexOf(x => x.Name == source[itemIndex].Name);

        //Assert
        result.Should().Be(itemIndex);
    }

    [TestMethod]
    public void WhenUsingItemAndItemIsInCollectionMultipleTimes_ReturnOnlyTheFirstOccurence()
    {
        //Arrange
        var item = Dummy.Create<Garbage>();
        var source = Dummy.CreateMany<Garbage>(3).Concat(item, item, item).To<TCollection, Garbage>();

        //Act
        var result = source.FirstIndexOf(item);

        //Assert
        result.Should().Be(3);
    }

    [TestMethod]
    public void WhenUsingLambdaAndItemIsInCollectionMultipleTimes_ReturnOnlyTheFirstOccurence()
    {
        //Arrange
        var item = Dummy.Create<Garbage>();
        var source = Dummy.CreateMany<Garbage>(3).Concat(item, item, item).To<TCollection, Garbage>();

        //Act
        var result = source.FirstIndexOf(x => x.Name == item.Name);

        //Assert
        result.Should().Be(3);
    }
}