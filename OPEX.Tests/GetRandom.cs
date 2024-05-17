namespace OPEX.Tests;

[TestClass]
public sealed class GetRandomWithArrayOfDummyTests : GetRandomTester<Garbage[]>
{

}

[TestClass]
public sealed class GetRandomWithListOfDummyTests : GetRandomTester<List<Garbage>>
{

}

[TestClass]
public sealed class GetRandomWithWriteOnlyListOfDummyTests : GetRandomTester<WriteOnlyList<Garbage>>
{

}

[TestClass]
public sealed class GetRandomWithImmutableListOfDummyTests : GetRandomTester<ImmutableList<Garbage>>
{

}

public abstract class GetRandomTester<TCollection> : Tester where TCollection : class, IEnumerable<Garbage>
{
    [TestMethod]
    public void WhenSourceIsNull_Throw()
    {
        //Arrange
        TCollection source = null!;

        //Act
        var action = () => source.GetRandom();

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenSourceIsEmpty_ReturnNull()
    {
        //Arrange
        var source = new List<Garbage>().To<TCollection, Garbage>();

        //Act
        var result = source.GetRandom();

        //Assert
        result.Should().BeNull();
    }


    [TestMethod]
    public void WhenSourceContainsOnlyOneItem_ReturnSingleItem()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>(1).To<TCollection, Garbage>();

        //Act
        var result = source.GetRandom();

        //Assert
        result.Should().Be(source.Single());
    }

    [TestMethod]
    public void WhenSourceContainsMultipleItems_ReturnAnyOfThem()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>(10).To<TCollection, Garbage>();

        //Act
        var result = source.GetRandom()!;

        //Assert
        source.Should().Contain(result);
    }
}