namespace OPEX.Tests;

[TestClass]
public sealed class PopManyRandoms : Tester
{
    [TestMethod]
    public void WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Dummy> source = null!;
        var count = Fixture.Create<int>();

        //Act
        var action = () => source.PopManyRandoms(count);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenCountIsNegative_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var count = -Fixture.Create<int>();

        //Act
        var action = () => source.PopManyRandoms(count);

        //Assert
        action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName(nameof(count));
    }

    [TestMethod]
    public void WhenCountIsLargerThanSource_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var count = source.Count + Fixture.Create<int>();

        //Act
        var action = () => source.PopManyRandoms(count);

        //Assert
        action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName(nameof(count));
    }

    [TestMethod]
    public void WhenCountIsEqualToSource_ReturnAllSourceItems()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var count = source.Count;
        var original = source.ToList();

        //Act
        var result = source.PopManyRandoms(count);

        //Assert
        result.Should().BeEquivalentTo(original);
    }

    [TestMethod]
    public void WhenCountIsEqualToSource_RemoveAllItemsFromSource()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var count = source.Count;

        //Act
        source.PopManyRandoms(count);

        //Assert
        source.Should().BeEmpty();
    }

    [TestMethod]
    public void WhenCountIsLessThanSource_ReturnMultipleRandomItemsFromSource()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>(10).ToList();
        var count = 5;
        var original = source.ToList();

        //Act
        var result = source.PopManyRandoms(count);

        //Assert
        result.Should().HaveCount(count);
        original.Should().Contain(result);
    }

    [TestMethod]
    public void WhenCountIsLessThanSource_RemoveThoseItemsFromSource()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>(10).ToList();
        var count = 5;
        var original = source.ToList();

        //Act
        var result = source.PopManyRandoms(count);

        //Assert
        source.Should().HaveCount(original.Count - count);
        source.Should().NotContain(result);
    }

}