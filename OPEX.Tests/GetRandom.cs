namespace OPEX.Tests;

[TestClass]
public sealed class GetRandomWithArrayOfDummyTests : GetRandomTester<Dummy[]>
{

}

[TestClass]
public sealed class GetRandomWithListOfDummyTests : GetRandomTester<List<Dummy>>
{

}

[TestClass]
public sealed class GetRandomWithWriteOnlyListOfDummyTests : GetRandomTester<WriteOnlyList<Dummy>>
{

}

[TestClass]
public sealed class GetRandomWithImmutableListOfDummyTests : GetRandomTester<ImmutableList<Dummy>>
{

}

public abstract class GetRandomTester<TCollection> : Tester where TCollection : class, IEnumerable<Dummy>
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
        var source = new List<Dummy>().To<TCollection, Dummy>();

        //Act
        var result = source.GetRandom();

        //Assert
        result.Should().BeNull();
    }


    [TestMethod]
    public void WhenSourceContainsOnlyOneItem_ReturnSingleItem()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>(1).To<TCollection, Dummy>();

        //Act
        var result = source.GetRandom();

        //Assert
        result.Should().Be(source.Single());
    }

    [TestMethod]
    public void WhenSourceContainsMultipleItems_ReturnAnyOfThem()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>(10).To<TCollection, Dummy>();

        //Act
        var result = source.GetRandom()!;

        //Assert
        source.Should().Contain(result);
    }
}