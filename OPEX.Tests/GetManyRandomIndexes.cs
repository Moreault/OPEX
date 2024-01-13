namespace OPEX.Tests;

[TestClass]
public sealed class GetManyRandomIndexesWithArrayOfDummyTests : GetManyRandomIndexesTester<Dummy[]>
{

}

[TestClass]
public sealed class GetManyRandomIndexesWithListOfDummyTests : GetManyRandomIndexesTester<List<Dummy>>
{

}

[TestClass]
public sealed class GetManyRandomIndexesWithWriteOnlyListOfDummyTests : GetManyRandomIndexesTester<WriteOnlyList<Dummy>>
{

}

[TestClass]
public sealed class GetManyRandomIndexesWithImmutableListOfDummyTests : GetManyRandomIndexesTester<ImmutableList<Dummy>>
{

}

public abstract class GetManyRandomIndexesTester<TCollection> : Tester where TCollection : class, IList<Dummy>
{
    [TestMethod]
    public void Get_WhenSourceIsNull_Throw()
    {
        //Arrange
        TCollection source = null!;

        //Act
        var action = () => source.GetManyRandomIndexes(Fixture.Create<int>());

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Get_WhenCountIsNegative_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().To<TCollection, Dummy>();
        var count = -Fixture.Create<int>();

        //Act
        var action = () => source.GetManyRandomIndexes(count);

        //Assert
        action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName(nameof(count));
    }

    [TestMethod]
    public void Get_WhenCountIsLargerThanCollection_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().To<TCollection, Dummy>();
        var count = source.Count + Fixture.Create<int>();

        //Act
        var action = () => source.GetManyRandomIndexes(count);

        //Assert
        action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName(nameof(count));
    }

    [TestMethod]
    public void Get_WhenCountIsEqualToCollectionSize_ReturnAllIndexes()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().To<TCollection, Dummy>();
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
        var source = Fixture.CreateMany<Dummy>(10).To<TCollection, Dummy>();

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
        var action = () => source.TryGetManyRandomIndexes(Fixture.Create<int>());

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void TryGet_WhenCountIsNegative_ReturnEmpty()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().To<TCollection, Dummy>();
        var count = -Fixture.Create<int>();

        //Act
        var result = source.TryGetManyRandomIndexes(count);

        //Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void TryGet_WhenCountIsLargerThanCollection_ReturnAllIndexes()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().To<TCollection, Dummy>();
        var count = source.Count + Fixture.Create<int>();

        //Act
        var result = source.TryGetManyRandomIndexes(count);

        //Assert
        result.Should().BeEquivalentTo(Enumerable.Range(0, source.Count));
    }

    [TestMethod]
    public void TryGet_WhenCountIsEqualToCollectionSize_ReturnAllIndexes()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().To<TCollection, Dummy>();
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
        var source = Fixture.CreateMany<Dummy>(10).To<TCollection, Dummy>();

        //Act
        var result = source.TryGetManyRandomIndexes(count);

        //Assert
        result.Should().HaveCount(count);
        result.Should().OnlyHaveUniqueItems();
        result.Should().OnlyContain(i => i >= 0 && i < source.Count);
    }
}