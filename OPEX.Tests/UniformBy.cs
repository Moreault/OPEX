namespace OPEX.Tests;

[TestClass]
public class UniformBy
{
    [TestClass]
    public class OrDefault : Tester
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            IEnumerable<Dummy> source = null!;
            var selector = Fixture.Create<Func<Dummy, bool>>();

            //Act
            var action = () => source.UniformByOrDefault(selector);

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
            var action = () => source.UniformByOrDefault(selector);

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
            var action = () => source.UniformByOrDefault(x => x.Id == id);

            //Assert
            action.Should().Throw<InvalidOperationException>().WithMessage(Exceptions.UniformFoundDuplicates);
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_ReturnDefault()
        {
            //Arrange

            //Act
            var result = new List<Dummy>().UniformByOrDefault(x => x.Id);

            //Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void WhenAllItemsHaveTheSameProperty_ReturnUniqueValue()
        {
            //Arrange
            var values = Fixture.Build<Dummy>().With(x => x.Name, "Roger").CreateMany().ToList();

            //Act
            var result = values.UniformByOrDefault(x => x.Name);

            //Assert
            result.Should().Be(values.First());
        }
    }

    [TestClass]
    public class NonDefault : Tester
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            IEnumerable<Dummy> source = null!;
            var selector = Fixture.Create<Func<Dummy, bool>>();

            //Act
            var action = () => source.UniformBy(selector);

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
            var action = () => source.UniformBy(selector);

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
            var action = () => source.UniformBy(x => x.Id == id);

            //Assert
            action.Should().Throw<InvalidOperationException>().WithMessage(Exceptions.UniformFoundDuplicates);
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_Throw()
        {
            //Arrange

            //Act
            var action = () => new List<Dummy>().UniformBy(x => x.Id);

            //Assert
            action.Should().Throw<InvalidOperationException>().WithMessage(Exceptions.CannotUseUniformOnEmptyCollection);
        }

        [TestMethod]
        public void WhenAllItemsHaveTheSameProperty_ReturnUniqueValue()
        {
            //Arrange
            var values = Fixture.Build<Dummy>().With(x => x.Name, "Roger").CreateMany().ToList();

            //Act
            var result = values.UniformBy(x => x.Name);

            //Assert
            result.Should().Be(values.First());
        }
    }
}