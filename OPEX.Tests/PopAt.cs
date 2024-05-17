namespace OPEX.Tests;

[TestClass]
public class PopAt : Tester
{
    [TestMethod]
    public void WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Garbage> source = null!;
        var index = Dummy.Create<int>();

        //Act
        var action = () => source.PopAt(index);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenSourceIsEmpty_Throw()
    {
        //Arrange
        var source = new List<Garbage>();
        var index = Dummy.Create<int>();

        //Act
        var action = () => source.PopAt(index);

        //Assert
        action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName(nameof(index));
    }

    [TestMethod]
    public void WhenSourceIsNotEmptyButIndexIsLowerThanZero_Throw()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        var index = -Dummy.Create<int>();

        //Act
        var action = () => source.PopAt(index);

        //Assert
        action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName(nameof(index));
    }

    [TestMethod]
    public void WhenSourceIsNotEmptyButIndexIsBiggerThanLastIndex_Throw()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        var index = source.LastIndex() + 1;

        //Act
        var action = () => source.PopAt(index);

        //Assert
        action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName(nameof(index));
    }

    [TestMethod]
    public void WhenSourceIsNotEmptyAndIndexIsWithinRange_ReturnItem()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        var index = source.GetRandomIndex();
        var item = source[index];

        //Act
        var result = source.PopAt(index);

        //Assert
        result.Should().Be(item);
    }

    [TestMethod]
    public void WhenSourceIsNotEmptyAndIndexIsWithinRange_RemoveItemAtIndex()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        var original = source.ToList();
        var index = source.GetRandomIndex();
        var item = source[index];

        //Act
        source.PopAt(index);

        //Assert
        source.Should().BeEquivalentTo(original.Where(x => x != item));
    }


    [TestMethod]
    public void MultipleIndexes_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Garbage> source = null!;
        var indexes = Dummy.CreateMany<int>().ToArray();

        //Act
        var action = () => source.PopAt(indexes);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void MultipleIndexes_WhenIndexesNull_Throw()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        int[]? indexes = null;

        //Act
        var action = () => source.PopAt(indexes);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(indexes));
    }

    [TestMethod]
    public void MultipleIndexes_WhenSourceIsEmpty_Throw()
    {
        //Arrange
        var source = new List<Garbage>();
        var indexes = Dummy.CreateMany<int>().ToArray();

        //Act
        var action = () => source.PopAt(indexes);

        //Assert
        action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName(nameof(indexes));
    }

    [TestMethod]
    public void MultipleIndexes_WhenIndexesAreEmpty_ReturnEmpty()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        var indexes = Array.Empty<int>();

        //Act
        var result = source.PopAt(indexes);

        //Assert
        result.Should().BeEmpty();
    }


    [TestMethod]
    public void MultipleIndexes_WhenOneIsOutsideOfRange_Throw()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        var indexes = Dummy.CreateMany<int>().Append(source.LastIndex() + 1).ToShuffled().ToArray();

        //Act
        var action = () => source.PopAt(indexes);

        //Assert
        action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName(nameof(indexes));
    }

    [TestMethod]
    public void MultipleIndexes_WhenAllAreWithinRange_ReturnAll()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>(10).ToList();
        var original = source.ToList();
        var indexes = source.GetManyRandomIndexes(3).ToArray();

        //Act
        var result = source.PopAt(indexes);

        //Assert
        result.Should().BeEquivalentTo(indexes.Select(x => original[x]));
    }
}