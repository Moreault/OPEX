namespace OPEX.Tests;

[TestClass]
public class Uniform
{
    [TestClass]
    public class Parameterless : Tester
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            IEnumerable<string> source = null!;

            //Act
            var action = () => source.Uniform();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenThereIsMoreThanOneOccurence_Throw()
        {
            //Arrange
            var same = Fixture.Create<string>();
            var source = new List<string>
            {
                same,
                Fixture.Create<string>(),
                same,
            };

            //Act
            var action = () => source.Uniform();

            //Assert
            action.Should().Throw<InvalidOperationException>().WithMessage(Exceptions.UniformFoundNonDuplicates);
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_Throw()
        {
            //Arrange

            //Act
            var action = () => new List<string>().Uniform();

            //Assert
            action.Should().Throw<InvalidOperationException>().WithMessage(Exceptions.CannotUseUniformOnEmptyCollection);
        }

        [TestMethod]
        public void WhenAllItemsHaveTheSameProperty_ReturnUniqueValue()
        {
            //Arrange
            var value = Fixture.Create<string>();
            var values = new List<string> { value, value, value };

            //Act
            var result = values.Uniform();

            //Assert
            result.Should().Be(value);
        }
    }

    [TestClass]
    public class ParameterlessOrDefault : Tester
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            IEnumerable<string> source = null!;

            //Act
            var action = () => source.UniformOrDefault();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenThereIsMoreThanOneOccurence_Throw()
        {
            //Arrange
            var same = Fixture.Create<string>();
            var source = new List<string>
            {
                same,
                Fixture.Create<string>(),
                same,
            };

            //Act
            var action = () => source.UniformOrDefault();

            //Assert
            action.Should().Throw<InvalidOperationException>().WithMessage(Exceptions.UniformFoundNonDuplicates);
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_ReturnDefault()
        {
            //Arrange

            //Act
            var result = new List<string>().UniformOrDefault();

            //Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void WhenAllItemsHaveTheSameProperty_ReturnUniqueValue()
        {
            //Arrange
            var value = Fixture.Create<string>();
            var values = new List<string> { value, value, value };

            //Act
            var result = values.UniformOrDefault();

            //Assert
            result.Should().Be(value);
        }
    }

    [TestClass]
    public class Selector : Tester
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            IEnumerable<Dummy> source = null!;
            var selector = Fixture.Create<Func<Dummy, bool>>();

            //Act
            var action = () => source.Uniform(selector);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WheSelectorIsNull_Throw()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToList();
            Func<Dummy, bool> selector = null!;

            //Act
            var action = () => source.Uniform(selector);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(selector));
        }

        [TestMethod]
        public void WhenThereIsMoreThanOneOccurence_Throw()
        {
            //Arrange
            var id = Fixture.Create<int>();
            var source = new List<Dummy>
            {
                Fixture.Build<Dummy>().With(x => x.Id, id).Create(),
                Fixture.Create<Dummy>(),
                Fixture.Build<Dummy>().With(x => x.Id, id).Create(),
            };

            //Act
            var action = () => source.Uniform(x => x.Id == id);

            //Assert
            action.Should().Throw<InvalidOperationException>().WithMessage(Exceptions.UniformFoundNonDuplicates);
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_Throw()
        {
            //Arrange

            //Act
            var action = () => new List<Dummy>().Uniform(x => x.Id);

            //Assert
            action.Should().Throw<InvalidOperationException>().WithMessage(Exceptions.CannotUseUniformOnEmptyCollection);
        }

        [TestMethod]
        public void WhenAllItemsHaveTheSameProperty_ReturnUniqueValue()
        {
            //Arrange
            var values = Fixture.Build<Dummy>().With(x => x.Name, "Roger").CreateMany();

            //Act
            var result = values.Uniform(x => x.Name);

            //Assert
            result.Should().Be("Roger");
        }
    }

    [TestClass]
    public class SelectorOrDefault : Tester
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            IEnumerable<Dummy> source = null!;
            var selector = Fixture.Create<Func<Dummy, bool>>();

            //Act
            var action = () => source.Uniform(selector);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WheSelectorIsNull_Throw()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToList();
            Func<Dummy, bool> selector = null!;

            //Act
            var action = () => source.Uniform(selector);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(selector));
        }

        [TestMethod]
        public void WhenThereIsMoreThanOneOccurence_Throw()
        {
            //Arrange
            var id = Fixture.Create<int>();
            var source = new List<Dummy>
            {
                Fixture.Build<Dummy>().With(x => x.Id, id).Create(),
                Fixture.Create<Dummy>(),
                Fixture.Build<Dummy>().With(x => x.Id, id).Create(),
            };

            //Act
            var action = () => source.Uniform(x => x.Id == id);

            //Assert
            action.Should().Throw<InvalidOperationException>().WithMessage(Exceptions.UniformFoundNonDuplicates);
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_ReturnDefault()
        {
            //Arrange

            //Act
            var result = new List<Dummy>().UniformOrDefault(x => x.Id);

            //Assert
            result.Should().Be(0);
        }

        [TestMethod]
        public void WhenAllItemsHaveTheSameProperty_ReturnUniqueValue()
        {
            //Arrange
            var values = Fixture.Build<Dummy>().With(x => x.Name, "Roger").CreateMany();

            //Act
            var result = values.Uniform(x => x.Name);

            //Assert
            result.Should().Be("Roger");
        }
    }
}