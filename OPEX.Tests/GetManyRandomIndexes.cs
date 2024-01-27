namespace OPEX.Tests;

[TestClass]
public sealed class GetManyRandomIndexesWithArrayOfDummyTests : GetManyRandomIndexesTester<Garbage[]>
{

}

[TestClass]
public sealed class GetManyRandomIndexesWithListOfDummyTests : GetManyRandomIndexesTester<List<Garbage>>
{

}

[TestClass]
public sealed class GetManyRandomIndexesWithWriteOnlyListOfDummyTests : GetManyRandomIndexesTester<WriteOnlyList<Garbage>>
{

}

[TestClass]
public sealed class GetManyRandomIndexesWithImmutableListOfDummyTests : GetManyRandomIndexesTester<ImmutableList<Garbage>>
{

}

public abstract class GetManyRandomIndexesTester<TCollection> : TestBase where TCollection : class, IList<Garbage>
{
    [TestMethod]
    public void Get_WhenSourceIsNull_Throw()
    {
        //Arrange
        TCollection source = null!;

        //Act
        var action = () => source.GetManyRandomIndexes(Dummy.Create<int>());

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Get_WhenCountIsNegative_Throw()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().To<TCollection, Garbage>();
        var count = -Dummy.Create<int>();

        //Act
        var action = () => source.GetManyRandomIndexes(count);

        //Assert
        action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName(nameof(count));
    }

    [TestMethod]
    public void Get_WhenCountIsLargerThanCollection_Throw()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().To<TCollection, Garbage>();
        var count = source.Count + Dummy.Create<int>();

        //Act
        var action = () => source.GetManyRandomIndexes(count);

        //Assert
        action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName(nameof(count));
    }

    [TestMethod]
    public void Get_WhenCountIsEqualToCollectionSize_ReturnAllIndexes()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().To<TCollection, Garbage>();
        var count = source.Count;

        //Act
        var result = source.GetManyRandomIndexes(count);

        //Assert
        result.Should().BeEquivalentTo(Enumerable.Range(0, source.Count));
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(4)]
    [DataRow(6)]
    [DataRow(8)]
    public void Get_WhenCountIsLessThanCollectionSize_ReturnRandomIndexes(int count)
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>(10).To<TCollection, Garbage>();

        //Act
        var result = source.GetManyRandomIndexes(count);

        //Assert
        result.Should().HaveCount(count);
        result.Should().OnlyHaveUniqueItems();
        result.Should().OnlyContain(i => i >= 0 && i < source.Count);
    }

    [TestMethod]
    public void TryGet_WhenSourceIsNull_Throw()
    {
        //Arrange
        TCollection source = null!;

        //Act
        var action = () => source.TryGetManyRandomIndexes(Dummy.Create<int>());

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void TryGet_WhenCountIsNegative_ReturnEmpty()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().To<TCollection, Garbage>();
        var count = -Dummy.Create<int>();

        //Act
        var result = source.TryGetManyRandomIndexes(count);

        //Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void TryGet_WhenCountIsLargerThanCollection_ReturnAllIndexes()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().To<TCollection, Garbage>();
        var count = source.Count + Dummy.Create<int>();

        //Act
        var result = source.TryGetManyRandomIndexes(count);

        //Assert
        result.Should().BeEquivalentTo(Enumerable.Range(0, source.Count));
    }

    [TestMethod]
    public void TryGet_WhenCountIsEqualToCollectionSize_ReturnAllIndexes()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().To<TCollection, Garbage>();
        var count = source.Count;

        //Act
        var result = source.TryGetManyRandomIndexes(count);

        //Assert
        result.Should().BeEquivalentTo(Enumerable.Range(0, source.Count));
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(4)]
    [DataRow(6)]
    [DataRow(8)]
    public void TryGet_WhenCountIsLessThanCollectionSize_ReturnRandomIndexes(int count)
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>(10).To<TCollection, Garbage>();

        //Act
        var result = source.TryGetManyRandomIndexes(count);

        //Assert
        result.Should().HaveCount(count);
        result.Should().OnlyHaveUniqueItems();
        result.Should().OnlyContain(i => i >= 0 && i < source.Count);
    }
}