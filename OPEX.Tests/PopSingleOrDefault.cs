using System.Text;

namespace OPEX.Tests;

[TestClass]
public sealed class PopSingleOrDefault : Tester
{
    [TestMethod]
    public void Parameterless_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Dummy> source = null!;

        //Act
        var action = () => source.PopSingleOrDefault();

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Parameterless_WhenSourceIsEmpty_ReturnNull()
    {
        //Arrange
        var source = new List<Dummy>();

        //Act
        var result = source.PopSingleOrDefault();

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    [DataRow(2)]
    [DataRow(3)]
    [DataRow(4)]
    public void ParameterlessWhenThereAreTwoOrMoreElementsInSource_Throw(int count)
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>(count).ToList();

        //Act
        var action = () => source.PopSingleOrDefault();

        //Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [TestMethod]
    public void Parameterless_WhenThereIsExactlyOneElementInSource_ReturnElement()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = new List<Dummy>
        {
            item
        };

        //Act
        var result = source.PopSingleOrDefault();

        //Assert
        result.Should().Be(item);
    }

    [TestMethod]
    public void Parameterless_WhenThereIsExactlyOneElementInSource_SourceShouldBeEmpty()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = new List<Dummy>
        {
            item
        };

        //Act
        source.PopSingleOrDefault();

        //Assert
        source.Should().BeEmpty();
    }

    [TestMethod]
    public void Item_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Dummy> source = null!;
        var item = Fixture.Create<Dummy>();

        //Act
        var action = () => source.PopSingleOrDefault(item);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Item_WhenSourceIsEmpty_ReturnNull()
    {
        //Arrange
        var source = new List<Dummy>();
        var item = Fixture.Create<Dummy>();

        //Act
        var result = source.PopSingleOrDefault(item);

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Item_WhenItemIsNotInSource_ReturnNull()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        var item = Fixture.Create<Dummy>();

        //Act
        var result = source.PopSingleOrDefault(item);

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Item_WhenThereAreMultipleOccurencesOfItemInSource_Throw()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = Fixture.CreateMany<Dummy>().Append(item).Append(item).ToShuffled().ToList();

        //Act
        var action = () => source.PopSingleOrDefault(item);

        //Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [TestMethod]
    public void Item_WhenThereIsExactlyOneOccurenceOfItemInSource_ReturnItem()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = Fixture.CreateMany<Dummy>().Append(item).ToShuffled().ToList();

        //Act
        var result = source.PopSingleOrDefault(item);

        //Assert
        result.Should().Be(item);
    }

    [TestMethod]
    public void Item_WhenThereIsExactlyOneOccurenceOfItemInSource_RemoveItem()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = Fixture.CreateMany<Dummy>().Append(item).ToShuffled().ToList();

        //Act
        source.PopSingleOrDefault(item);

        //Assert
        source.Should().BeEquivalentTo(source.Where(x => x != item));
    }

    [TestMethod]
    public void Predicate_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Dummy> source = null!;
        var match = Fixture.Create<Func<Dummy, bool>>();

        //Act
        var action = () => source.PopSingleOrDefault(match);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Predicate_WhenPredicateIsNull_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();
        Func<Dummy, bool> match = null!;

        //Act
        var action = () => source.PopSingleOrDefault(match);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(match));
    }

    [TestMethod]
    public void Predicate_WhenThereIsNoMatchInSource_Throw()
    {
        //Arrange
        var source = Fixture.CreateMany<Dummy>().ToList();

        //Act
        var result = source.PopSingleOrDefault(x => x.Id < 0);

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Predicate_WhenThereIsMoreThanOneMatchInSource_Throw()
    {
        //Arrange
        var name = Fixture.Create<string>();
        var item = Fixture.Build<Dummy>().With(x => x.Name, name).CreateMany();
        var source = Fixture.CreateMany<Dummy>().Concat(item).ToShuffled().ToList();

        //Act
        var action = () => source.PopSingleOrDefault(x => x.Name == name);

        //Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [TestMethod]
    public void Predicate_WhenThereIsExactlyOneMatchInSource_ReturnMatch()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = Fixture.CreateMany<Dummy>().Append(item).ToShuffled().ToList();

        //Act
        var result = source.PopSingleOrDefault(x => x.Name == item.Name);

        //Assert
        result.Should().Be(item);
    }

    [TestMethod]
    public void Predicate_WhenThereIsExactlyOneMatchInSource_RemoveMatchFromSource()
    {
        //Arrange
        var item = Fixture.Create<Dummy>();
        var source = Fixture.CreateMany<Dummy>().Append(item).ToShuffled().ToList();
        var original = source.ToList();

        //Act
        source.PopSingleOrDefault(x => x.Name == item.Name);

        //Assert
        source.Should().BeEquivalentTo(original.Where(x => x != item));
    }
}