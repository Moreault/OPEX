using System.Text;

namespace OPEX.Tests;

[TestClass]
public sealed class PopSingleOrDefault : Tester
{
    [TestMethod]
    public void Parameterless_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Garbage> source = null!;

        //Act
        var action = () => source.PopSingleOrDefault();

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Parameterless_WhenSourceIsEmpty_ReturnNull()
    {
        //Arrange
        var source = new List<Garbage>();

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
        var source = Dummy.CreateMany<Garbage>(count).ToList();

        //Act
        var action = () => source.PopSingleOrDefault();

        //Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [TestMethod]
    public void Parameterless_WhenThereIsExactlyOneElementInSource_ReturnElement()
    {
        //Arrange
        var item = Dummy.Create<Garbage>();
        var source = new List<Garbage>
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
        var item = Dummy.Create<Garbage>();
        var source = new List<Garbage>
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
        IList<Garbage> source = null!;
        var item = Dummy.Create<Garbage>();

        //Act
        var action = () => source.PopSingleOrDefault(item);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Item_WhenSourceIsEmpty_ReturnNull()
    {
        //Arrange
        var source = new List<Garbage>();
        var item = Dummy.Create<Garbage>();

        //Act
        var result = source.PopSingleOrDefault(item);

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Item_WhenItemIsNotInSource_ReturnNull()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        var item = Dummy.Create<Garbage>();

        //Act
        var result = source.PopSingleOrDefault(item);

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Item_WhenThereAreMultipleOccurencesOfItemInSource_Throw()
    {
        //Arrange
        var item = Dummy.Create<Garbage>();
        var source = Dummy.CreateMany<Garbage>().Append(item).Append(item).ToShuffled().ToList();

        //Act
        var action = () => source.PopSingleOrDefault(item);

        //Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [TestMethod]
    public void Item_WhenThereIsExactlyOneOccurenceOfItemInSource_ReturnItem()
    {
        //Arrange
        var item = Dummy.Create<Garbage>();
        var source = Dummy.CreateMany<Garbage>().Append(item).ToShuffled().ToList();

        //Act
        var result = source.PopSingleOrDefault(item);

        //Assert
        result.Should().Be(item);
    }

    [TestMethod]
    public void Item_WhenThereIsExactlyOneOccurenceOfItemInSource_RemoveItem()
    {
        //Arrange
        var item = Dummy.Create<Garbage>();
        var source = Dummy.CreateMany<Garbage>().Append(item).ToShuffled().ToList();

        //Act
        source.PopSingleOrDefault(item);

        //Assert
        source.Should().BeEquivalentTo(source.Where(x => x != item));
    }

    [TestMethod]
    public void Predicate_WhenSourceIsNull_Throw()
    {
        //Arrange
        IList<Garbage> source = null!;
        var match = Dummy.Create<Func<Garbage, bool>>();

        //Act
        var action = () => source.PopSingleOrDefault(match);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
    }

    [TestMethod]
    public void Predicate_WhenPredicateIsNull_Throw()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();
        Func<Garbage, bool> match = null!;

        //Act
        var action = () => source.PopSingleOrDefault(match);

        //Assert
        action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(match));
    }

    [TestMethod]
    public void Predicate_WhenThereIsNoMatchInSource_Throw()
    {
        //Arrange
        var source = Dummy.CreateMany<Garbage>().ToList();

        //Act
        var result = source.PopSingleOrDefault(x => x.Id < 0);

        //Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public void Predicate_WhenThereIsMoreThanOneMatchInSource_Throw()
    {
        //Arrange
        var name = Dummy.Create<string>();
        var item = Dummy.Build<Garbage>().With(x => x.Name, name).CreateMany();
        var source = Dummy.CreateMany<Garbage>().Concat(item).ToShuffled().ToList();

        //Act
        var action = () => source.PopSingleOrDefault(x => x.Name == name);

        //Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [TestMethod]
    public void Predicate_WhenThereIsExactlyOneMatchInSource_ReturnMatch()
    {
        //Arrange
        var item = Dummy.Create<Garbage>();
        var source = Dummy.CreateMany<Garbage>().Append(item).ToShuffled().ToList();

        //Act
        var result = source.PopSingleOrDefault(x => x.Name == item.Name);

        //Assert
        result.Should().Be(item);
    }

    [TestMethod]
    public void Predicate_WhenThereIsExactlyOneMatchInSource_RemoveMatchFromSource()
    {
        //Arrange
        var item = Dummy.Create<Garbage>();
        var source = Dummy.CreateMany<Garbage>().Append(item).ToShuffled().ToList();
        var original = source.ToList();

        //Act
        source.PopSingleOrDefault(x => x.Name == item.Name);

        //Assert
        source.Should().BeEquivalentTo(original.Where(x => x != item));
    }
}