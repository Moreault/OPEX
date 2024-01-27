namespace OPEX.Tests;

[TestClass]
public class Uniform
{
    [TestClass]
    public class Parameterless : TestBase
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
            var same = Dummy.Create<string>();
            var source = new List<string>
            {
                same,
                Dummy.Create<string>(),
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
            var value = Dummy.Create<string>();
            var values = new List<string> { value, value, value };

            //Act
            var result = values.Uniform();

            //Assert
            result.Should().Be(value);
        }
    }

    [TestClass]
    public class ParameterlessOrDefault : TestBase
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
            var same = Dummy.Create<string>();
            var source = new List<string>
            {
                same,
                Dummy.Create<string>(),
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
            var value = Dummy.Create<string>();
            var values = new List<string> { value, value, value };

            //Act
            var result = values.UniformOrDefault();

            //Assert
            result.Should().Be(value);
        }
    }

    [TestClass]
    public class Selector : TestBase
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            IEnumerable<Garbage> source = null!;
            var selector = Dummy.Create<Func<Garbage, bool>>();

            //Act
            var action = () => source.Uniform(selector);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WheSelectorIsNull_Throw()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToList();
            Func<Garbage, bool> selector = null!;

            //Act
            var action = () => source.Uniform(selector);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(selector));
        }

        [TestMethod]
        public void WhenThereIsMoreThanOneOccurence_Throw()
        {
            //Arrange
            var id = Dummy.Create<int>();
            var source = new List<Garbage>
            {
                Dummy.Build<Garbage>().With(x => x.Id, id).Create(),
                Dummy.Create<Garbage>(),
                Dummy.Build<Garbage>().With(x => x.Id, id).Create(),
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
            var action = () => new List<Garbage>().Uniform(x => x.Id);

            //Assert
            action.Should().Throw<InvalidOperationException>().WithMessage(Exceptions.CannotUseUniformOnEmptyCollection);
        }

        [TestMethod]
        public void WhenAllItemsHaveTheSameProperty_ReturnUniqueValue()
        {
            //Arrange
            var values = Dummy.Build<Garbage>().With(x => x.Name, "Roger").CreateMany();

            //Act
            var result = values.Uniform(x => x.Name);

            //Assert
            result.Should().Be("Roger");
        }
    }

    [TestClass]
    public class SelectorOrDefault : TestBase
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            IEnumerable<Garbage> source = null!;
            var selector = Dummy.Create<Func<Garbage, bool>>();

            //Act
            var action = () => source.Uniform(selector);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WheSelectorIsNull_Throw()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToList();
            Func<Garbage, bool> selector = null!;

            //Act
            var action = () => source.Uniform(selector);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(selector));
        }

        [TestMethod]
        public void WhenThereIsMoreThanOneOccurence_Throw()
        {
            //Arrange
            var id = Dummy.Create<int>();
            var source = new List<Garbage>
            {
                Dummy.Build<Garbage>().With(x => x.Id, id).Create(),
                Dummy.Create<Garbage>(),
                Dummy.Build<Garbage>().With(x => x.Id, id).Create(),
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
            var result = new List<Garbage>().UniformOrDefault(x => x.Id);

            //Assert
            result.Should().Be(0);
        }

        [TestMethod]
        public void WhenAllItemsHaveTheSameProperty_ReturnUniqueValue()
        {
            //Arrange
            var values = Dummy.Build<Garbage>().With(x => x.Name, "Roger").CreateMany();

            //Act
            var result = values.Uniform(x => x.Name);

            //Assert
            result.Should().Be("Roger");
        }
    }
}