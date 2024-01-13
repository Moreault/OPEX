using ToolBX.Eloquentest.Extensions;

namespace OPEX.Tests;

[TestClass]
public class PopAll : Tester
{
    [TestMethod]
    public void Parameterless_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Dummy> source = null!;

        //Act
        var action = () => source.PopAll();

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Parameterless_WhenSourceIsEmpty_ReturnEmpty()
    {
        //Arrange
        var source = new List<Dummy>();

        //Act
        var result = source.PopAll();

        //Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void Parameterless_WhenSourceIsEmpty_SourceShouldStillBeEmpty()
    {
        //Arrange
        var source = new List<Dummy>();

        //Act
        source.PopAll();

        //Assert
        source.Should().BeEmpty();
    }

    [TestMethod]
    public void Parameterless_WhenSourceContainsItems_ReturnAllItems()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var original = source.ToList();

        //Act
        var result = source.PopAll();

        //Assert
        result.Should().BeEquivalentTo(original);
    }

    [TestMethod]
    public void Parameterless_WhenSourceContainsItems_RemoveAllItems()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();

        //Act
        source.PopAll();

        //Assert
        source.Should().BeEmpty();
    }

    [TestMethod]
    public void ItemOverload_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Dummy> source = null!;

        //Act
        var action = () => source.PopAll((Dummy)null!);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void ItemOverload_WhenItemNotInSource_ReturnEmpty()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var item = Fixture.Create<Dummy>();

        //Act
        var result = source.PopAll(item);

        //Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void ItemOverload_WhenItemIsInSource_ReturnItem()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = Fixture.CreateMany<Dummy>(5).Append(item).ToShuffled().ToList();

        //Act
        var result = source.PopAll(item);

        //Assert
        result.Should().BeEquivalentTo(new List<Dummy> { item });
    }

    [TestMethod]
    public void ItemOverload_WhenItemIsInSource_RemoveFromSource()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = Fixture.CreateMany<Dummy>(5).Append(item).ToShuffled().ToList();

        //Act
        source.PopAll(item);

        //Assert
        source.Should().NotContain(item);
    }

    [TestMethod]
    public void ItemOverload_WhenItemIsInSourceMultipleTimes_ReturnAllOccurences()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = Fixture.CreateMany<Dummy>(5).Append(item).Append(item).Append(item).ToShuffled().ToList();

        //Act
        var result = source.PopAll(item);

        //Assert
        result.Should().BeEquivalentTo(new List<Dummy> { item, item, item });
    }

    [TestMethod]
    public void ItemOverload_WhenItemIsInSource_RemoveAllOccurencesFromSource()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = Fixture.CreateMany<Dummy>(5).Append(item).Append(item).Append(item).ToShuffled().ToList();

        //Act
        source.PopAll(item);

        //Assert
        source.Should().NotContain(item);
    }

    [TestMethod]
    public void LambdaOverload_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Dummy> source = null!;
        Func<Dummy, bool> predicate = x => x.Name == Fixture.Create<string>();

        //Act
        var action = () => source.PopAll(predicate);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void LambdaOverload_WhenMatchIsNull_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        Func<Dummy, bool> match = null!;

        //Act
        var action = () => source.PopAll(match);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(match));
    }

    [TestMethod]
    public void LambdaOverload_WhenNoMatchInSource_ReturnEmpty()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();

        //Act
        var result = source.PopAll(x => x.Name == Fixture.Create<string>());

        //Assert
        result.Should().BeEmpty();
    }

    [TestMethod]
    public void LambdaOverload_WhenNoMatchInSource_DoNotModifySource()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var original = source.ToList();

        //Act
        source.PopAll(x => x.Name == Fixture.Create<string>());

        //Assert
        source.Should().BeEquivalentTo(original);
    }

    [TestMethod]
    public void LambdaOverload_WhenMultipleMatchesInSource_ReturnAllMatches()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>(3).ToList();
        var name = Fixture.Create<string>();
        var items = Fixture.Build<Dummy>().With(x => x.Name, name).CreateMany().ToList();
        source.AddRange(items);

        //Act
        var result = source.PopAll(x => x.Name == name);

        //Assert
        result.Should().BeEquivalentTo(items);
    }

    [TestMethod]
    public void LambdaOverload_WhenMultipleMatchesInSource_RemoveFromSource()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>(3).ToList();
        var name = Fixture.Create<string>();
        var items = Fixture.Build<Dummy>().With(x => x.Name, name).CreateMany().ToList();
        source.AddRange(items);

        //Act
        source.PopAll(x => x.Name == name);

        //Assert
        source.Should().NotContain(items);
    }
}