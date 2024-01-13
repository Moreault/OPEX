namespace OPEX.Tests;

[TestClass]
public sealed class FirstIndexOfWithArrayOfDummyTests : FirstIndexOfTester<Dummy[]>
{

}

[TestClass]
public sealed class FirstIndexOfWithListOfDummyTests : FirstIndexOfTester<List<Dummy>>
{

}

[TestClass]
public sealed class FirstIndexOfWithWriteOnlyListOfDummyTests : FirstIndexOfTester<WriteOnlyList<Dummy>>
{

}

[TestClass]
public sealed class FirstIndexOfWithImmutableListOfDummyTests : FirstIndexOfTester<ImmutableList<Dummy>>
{

}

public abstract class FirstIndexOfTester<TCollection> : Tester where TCollection : class, IList<Dummy>
{
    [TestMethod]
    public void WhenUsingItemAndCollectionIsNull_Throw()
    {
        //Arrange
        TCollection source = null!;
        var item = Fixture.Create<Dummy>();

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
        var item = Fixture.Create<Func<Dummy, bool>>();

        //Act
        var action = () => source.FirstIndexOf(item);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenUsingLambaAndLambdaIsNull_Throw()
    {
        //Arrange
        var source = Fixture.Create<TCollection>();
        Func<Dummy, bool> match = null!;

        //Act
        var action = () => source.FirstIndexOf(match);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(match));
    }

    [TestMethod]
    public void WhenUsingItemAndItemIsNotInCollection_ReturnMinusOne()
    {
        //Arrange
        var source = Fixture.Create<TCollection>();

        //Act
        var result = source.FirstIndexOf(Fixture.Create<Dummy>());

        //Assert
        result.Should().Be(-1);
    }

    [TestMethod]
    public void WhenUsingLambdaAndItemIsNotInCollection_ReturnMinusOne()
    {
        //Arrange
        var source = Fixture.Create<TCollection>();

        //Act
        var result = source.FirstIndexOf(x => x.Name == Fixture.Create<string>());

        //Assert
        result.Should().Be(-1);
    }

    [TestMethod]
    public void WhenUsingItemAndItemIsInCollectionOnce_ReturnItemIndex()
    {
        //Arrange
        var source = Fixture.Create<TCollection>();
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
        var source = Fixture.Create<TCollection>();
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
        var item = Fixture.Create<Dummy>();
        var source = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).To<TCollection, Dummy>();

        //Act
        var result = source.FirstIndexOf(item);

        //Assert
        result.Should().Be(3);
    }

    [TestMethod]
    public void WhenUsingLambdaAndItemIsInCollectionMultipleTimes_ReturnOnlyTheFirstOccurence()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).To<TCollection, Dummy>();

        //Act
        var result = source.FirstIndexOf(x => x.Name == item.Name);

        //Assert
        result.Should().Be(3);
    }
}