namespace OPEX.Tests;

[TestClass]
public sealed class TryPopAt : TestBase
{
    [TestMethod]
    public void SingleIndex_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Garbage> source = null!;
        var index = Dummy.Create<int>();

        //Act
        var action = () => source.TryPopAt(index);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void SingleIndex_WhenSourceIsEmpty_ReturnFailure()
    {
        //Arrange
        var source = new List<Garbage>();
        var index = Dummy.Create<int>();

        //Act
        var result = source.TryPopAt(index);

        //Assert
        result.Should().BeEquivalentTo(Result<Garbage>.Failure());
    }

    [TestMethod]
    public void SingleIndex_WhenSourceIsNotEmptyButIndexIsLowerThanZero_ReturnFailure()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        var index = -Dummy.Create<int>();

        //Act
        var result = source.TryPopAt(index);

        //Assert
        result.Should().BeEquivalentTo(Result<Garbage>.Failure());
    }

    [TestMethod]
    public void SingleIndex_WhenSourceIsNotEmptyButIndexIsBiggerThanLastIndex_ReturnFailure()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        var index = source.LastIndex() + 1;

        //Act
        var result = source.TryPopAt(index);

        //Assert
        result.Should().BeEquivalentTo(Result<Garbage>.Failure());
    }

    [TestMethod]
    public void SingleIndex_WhenIsWithinRange_ReturnItem()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        var original = source.ToList();
        var index = source.GetRandomIndex();

        //Act
        var result = source.TryPopAt(index);

        //Assert
        result.Should().BeEquivalentTo(Result<Garbage>.Success(original[index]));
    }

    [TestMethod]
    public void Multiple_WhenSourceIsNull_Throw()
    {
        //Arrange
        Garbage[] source = null!;
        var indexes = Dummy.CreateMany<int>().ToList();

        //Act
        var action = () => source.TryPopAt(indexes);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Multiple_WhenIndexesIsNull_Throw()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToArray();

        //Act
        var action = () => source.TryPopAt(null!);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName("indexes");
    }

    [TestMethod]
    public void Multiple_WhenIndexIsOutOfRange_ReturnEmpty()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        var indexes = new[] { source.LastIndex() + 1 };

        //Act
        var result = source.TryPopAt(indexes);

        //Assert
        result.Should().BeEquivalentTo(new List<Result<Garbage>>
        {
            Result<Garbage>.Failure()
        });
    }

}