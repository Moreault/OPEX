namespace OPEX.Tests;

[TestClass]
public class Split : Tester
{
    [TestMethod]
    public void WhenSourceIsNull_Throw()
    {
        //Arrange
        IEnumerable<Dummy> source = null!;
        var match = Fixture.Create<Func<Dummy, bool>>();

        //Act
        var action = () => source.Split(match);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void WhenMatchIsNull_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        Func<Dummy, bool> match = null!;

        //Act
        var action = () => source.Split(match);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(match));
    }

    [TestMethod]
    public void WhenSourceIsEmpty_ReturnEmptyResult()
    {
        //Arrange
        var source = new List<Dummy>();

        //Act
        var result = source.Split(x => !string.IsNullOrWhiteSpace(x.Name));

        //Assert
        result.Should().BeEquivalentTo(new Splitted<Dummy>());
    }

    [TestMethod]
    public void WhenSourceIsNotEmptyButThereIsNoMatch_ReturnOnlyRemaining()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();

        //Act
        var result = source.Split(x => string.IsNullOrWhiteSpace(x.Name));

        //Assert
        result.Should().BeEquivalentTo(new Splitted<Dummy>
        {
            Remaining = source
        });
    }

    [TestMethod]
    public void WhenSourceIsNotEmptyAndThereAreMatches_SplitBetweenRemainingAndExcluded()
    {
        //Arrange
        var toRemain = Fixture.CreateMany<Dummy>().ToList();
        var toExclude = Fixture.Build<Dummy>().With(x => x.Level, -Fixture.Create<short>()).CreateMany().ToList();
        var source = toRemain.Concat(toExclude).ToList();

        //Act
        var result = source.Split(x => x.Level < 0);

        //Assert
        result.Should().BeEquivalentTo(new Splitted<Dummy>
        {
            Remaining = toRemain,
            Excluded = toExclude
        });
    }
}