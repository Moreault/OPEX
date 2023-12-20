namespace OPEX.Tests;

[TestClass]
public sealed class TryPopAt : Tester
{
    [TestMethod]
    public void SingleIndex_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Dummy> source = null!;
        var index = Fixture.Create<int>();

        //Act
        var action = () => source.TryPopAt(index);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void SingleIndex_WhenSourceIsEmpty_ReturnFailure()
    {
        //Arrange
        var source = new List<Dummy>();
        var index = Fixture.Create<int>();

        //Act
        var result = source.TryPopAt(index);

        //Assert
        result.Should().BeEquivalentTo(Result<Dummy>.Failure());
    }

    [TestMethod]
    public void SingleIndex_WhenSourceIsNotEmptyButIndexIsLowerThanZero_ReturnFailure()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var index = -Fixture.Create<int>();

        //Act
        var result = source.TryPopAt(index);

        //Assert
        result.Should().BeEquivalentTo(Result<Dummy>.Failure());
    }

    [TestMethod]
    public void SingleIndex_WhenSourceIsNotEmptyButIndexIsBiggerThanLastIndex_ReturnFailure()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var index = source.LastIndex() + 1;

        //Act
        var result = source.TryPopAt(index);

        //Assert
        result.Should().BeEquivalentTo(Result<Dummy>.Failure());
    }

    [TestMethod]
    public void SingleIndex_WhenIsWithinRange_ReturnItem()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var original = source.ToList();
        var index = source.GetRandomIndex();

        //Act
        var result = source.TryPopAt(index);

        //Assert
        result.Should().BeEquivalentTo(Result<Dummy>.Success(original[index]));
    }

    [TestMethod]
    public void Multiple_WhenSourceIsNull_Throw()
    {
        //Arrange
        Dummy[] source = null!;
        var indexes = Fixture.CreateMany<int>().ToList();

        //Act
        var action = () => source.TryPopAt(indexes);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Multiple_WhenIndexesIsNull_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToArray();

        //Act
        var action = () => source.TryPopAt(null!);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName("indexes");
    }

    [TestMethod]
    public void Multiple_WhenIndexIsOutOfRange_ReturnEmpty()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var indexes = new[] { source.LastIndex() + 1 };

        //Act
        var result = source.TryPopAt(indexes);

        //Assert
        result.Should().BeEquivalentTo(new List<Result<Dummy>>
        {
            Result<Dummy>.Failure()
        });
    }

}