namespace OPEX.Tests;

[TestClass]
public sealed class PopRandom : Tester
{
    [TestMethod]
    public void WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Dummy> source = null!;

        //Act
        var action = () => source.PopRandom();

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenSourceIsEmpty_Throw()
    {
        //Arrange
        var source = new List<Dummy>();

        //Act
        var action = () => source.PopRandom();

        //Assert
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [TestMethod]
    public void WhenSourceContainsOneItem_ReturnItem()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = new List<Dummy> { item };

        //Act
        var result = source.PopRandom();

        //Assert
        result.Should().Be(item);
    }

    [TestMethod]
    public void WhenSourceContainsOneItem_RemoveItem()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = new List<Dummy> { item };

        //Act
        source.PopRandom();

        //Assert
        source.Should().BeEmpty();
    }

    [TestMethod]
    public void WhenSourceContainsMultipleItems_ReturnAny()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var original = source.ToList();

        //Act
        var result = source.PopRandom();

        //Assert
        result.Should().BeOneOf(original);

    }

    [TestMethod]
    public void WhenSourceContainsMultipleItems_RemoveReturnedItem()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var original = source.ToList();

        //Act
        var result = source.PopRandom();

        //Assert
        source.Should().BeEquivalentTo(original.Where(x => x != result));
    }
}