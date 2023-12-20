namespace OPEX.Tests;

[TestClass]
public sealed class WithAt : Tester
{
    [TestMethod]
    public void WhenSourceIsNull_Throw()
    {
        //Arrange
        IReadOnlyList<Dummy> source = null!;

        //Act
        var action = () => source.WithAt(0);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenItemsIsNull_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        
        //Act
        var action = () => source.WithAt(0, (IEnumerable<Dummy>)null!);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName("items");
    }

    [TestMethod]
    public void WhenIndexIsNegative_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var index = -Fixture.Create<int>();

        //Act
        var action = () => source.WithAt(index);

        //Assert
        action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName(nameof(index));
    }

    [TestMethod]
    public void WhenIndexIsLargerThanLastPlusOne_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var index = source.LastIndex() + 2;
        var item = Fixture.Create<Dummy>();

        //Act
        var action = () => source.WithAt(index, item);

        //Assert
        action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName(nameof(index));
    }

    [TestMethod]
    public void WhenIndexIsLastPlusOne_ReturnWithItemsAddedAtTheEnd()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var index = source.LastIndex() + 1;
        var item = Fixture.Create<Dummy>();

        //Act
        var result = source.WithAt(index, item);

        //Assert
        result.Should().BeEquivalentTo(source.Append(item));
    }

    [TestMethod]
    public void WhenIndexIsWithinRange_AddItemsAtIndex()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>(5).ToList();
        var index = source.GetRandomIndex();
        var items = Fixture.CreateMany<Dummy>().ToArray();

        //Act
        var result = source.WithAt(index, items);

        //Assert
        result.Should().BeEquivalentTo(source.Take(index).Concat(items).Concat(source.Skip(index)));
    }
}