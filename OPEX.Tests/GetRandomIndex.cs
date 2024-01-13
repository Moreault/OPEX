namespace OPEX.Tests;

[TestClass]
public sealed class GetRandomIndexWithArrayOfDummyTests : GetRandomIndexTester<Dummy[]>
{

}

[TestClass]
public sealed class GetRandomIndexWithListOfDummyTests : GetRandomIndexTester<List<Dummy>>
{

}

[TestClass]
public sealed class GetRandomIndexWithWriteOnlyListOfDummyTests : GetRandomIndexTester<WriteOnlyList<Dummy>>
{

}

[TestClass]
public sealed class GetRandomIndexWithImmutableListOfDummyTests : GetRandomIndexTester<ImmutableList<Dummy>>
{

}

public abstract class GetRandomIndexTester<TCollection> : Tester where TCollection : class, IEnumerable<Dummy>
{
    [TestMethod]
    public void WhenSourceIsNull_Throw()
    {
        //Arrange
        TCollection source = null!;

        //Act
        var action = () => source.GetRandomIndex();

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenSourceContainsItems_ReturnRandomIndexWithinBoundaries()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>(15).To<TCollection, Dummy>();

        //Act
        var results = new List<int>();
        for (var i = 0; i < 100; i++)
            results.Add(source.GetRandomIndex());

        //Assert
        results.Should().OnlyContain(x => x >= 0 && x <= 14);
    }

    [TestMethod]
    public void WhenSourceIsEmpty_ReturnMinusOne()
    {
        //Arrange
        var source = new List<Dummy>().To<TCollection, Dummy>();

        //Act
        var result = source.GetRandomIndex();

        //Assert
        result.Should().Be(-1);
    }
}