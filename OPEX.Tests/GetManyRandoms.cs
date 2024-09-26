namespace OPEX.Tests;

[TestClass]
public sealed class GetManyRandomsithArrayOfDummyTests : GetManyRandomsTester<Garbage[]>
{

}

[TestClass]
public sealed class GetManyRandomsWithListOfDummyTests : GetManyRandomsTester<List<Garbage>>
{

}

[TestClass]
public sealed class GetManyRandomsWithWriteOnlyListOfDummyTests : GetManyRandomsTester<WriteOnlyList<Garbage>>
{

}

[TestClass]
public sealed class GetManyRandomsWithImmutableListOfDummyTests : GetManyRandomsTester<ImmutableList<Garbage>>
{

}

public abstract class GetManyRandomsTester<TCollection> : Tester where TCollection : class, IEnumerable<Garbage>
{
    [TestMethod]
    public void WhenCollectionIsNull_Throw()
    {
        //Arrange
        TCollection source = null!;
        var numberOfElements = Dummy.Create<int>();

        //Act
        var action = () => source.GetManyRandoms(numberOfElements);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenNumberOfElementsIsNegative_Throw()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().To<TCollection, Garbage>();
        var numberOfElements = -Dummy.Create<int>();

        //Act
        var action = () => source.GetManyRandoms(numberOfElements);

        //Assert
        action.Should().Throw<ArgumentException>().WithMessage(string.Format(Exceptions.CannotGetManyRandomsBecauseNumberNegative, numberOfElements));
    }

    [TestMethod]
    public void WhenNumberOfElementsIsZero_ReturnEmpty()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().To<TCollection, Garbage>();
        var numberOfElements = 0;

        //Act
        var result = source.GetManyRandoms(numberOfElements);

        //Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void WhenCollectionIsEmpty_ReturnEmpty()
    {
        //Arrange
        var source = new List<Garbage>().To<TCollection, Garbage>();
        var numberOfElements = Dummy.Create<int>();

        //Act
        var result = source.GetManyRandoms(numberOfElements);

        //Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void WhenNumberOfElementsIsOne_ReturnSingleItemFromCollection()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().To<TCollection, Garbage>();
        var numberOfElements = 1;

        //Act
        var result = source.GetManyRandoms(numberOfElements);

        //Assert
        result.Should().HaveCount(1);
        source.Should().Contain(result);
    }

    [TestMethod]
    public void WhenNumberOfElementsIsTheSameAsSizeOfCollection_ReturnEntireCollection()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().To<TCollection, Garbage>();
        var numberOfElements = source.Count();

        //Act
        var result = source.GetManyRandoms(numberOfElements);

        //Assert
        result.Should().BeEquivalentTo(source);
    }

    [TestMethod]
    public void WhenNumberOfElementsIsGreaterThanSizeOfCollection_ReturnEntireCollection()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().To<TCollection, Garbage>();
        var numberOfElements = source.Count() + Dummy.Create<int>();

        //Act
        var result = source.GetManyRandoms(numberOfElements);

        //Assert
        result.Should().BeEquivalentTo(source);
    }

    [TestMethod]
    public void WhenNumberOfElementsIsSmallerThanSizeOfCollection_ReturnRandomElementsFromCollection()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>(15).To<TCollection, Garbage>();
        var numberOfElements = 5;

        //Act
        var result = source.GetManyRandoms(numberOfElements);

        //Assert
        result.Should().HaveCount(5);
        source.Should().Contain(result);
    }
}