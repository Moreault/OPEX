namespace OPEX.Tests;

[TestClass]
public class GetRandom
{
    [TestClass]
    public class WithArray : Tester
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            Dummy[] source = null!;

            //Act
            var action = () => source.GetRandom();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenSourceIsEmpty_ReturnNull()
        {
            //Arrange
            var source = Array.Empty<Dummy>();

            //Act
            var result = source.GetRandom();

            //Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void WhenSourceIsEmptyAndValueType_ReturnDefault()
        {
            //Arrange
            var source = Array.Empty<int>();

            //Act
            var result = source.GetRandom();

            //Assert
            result.Should().Be(default);
        }

        [TestMethod]
        public void WhenSourceContainsOnlyOneItem_ReturnSingleItem()
        {
            //Arrange
            var source = new[]
            {
                Fixture.Create<Dummy>()
            };

            //Act
            var result = source.GetRandom();

            //Assert
            result.Should().Be(source.Single());
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItems_ReturnAnyOfThem()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>(10).ToArray();

            //Act
            var result = source.GetRandom()!;

            //Assert
            source.Should().Contain(result);
        }
    }

    [TestClass]
    public class WithList : Tester
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            List<Dummy> source = null!;

            //Act
            var action = () => source.GetRandom();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenSourceIsEmpty_ReturnNull()
        {
            //Arrange
            var source = new List<Dummy>();

            //Act
            var result = source.GetRandom();

            //Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void WhenSourceIsEmptyAndValueType_ReturnDefault()
        {
            //Arrange
            var source = new List<int>();

            //Act
            var result = source.GetRandom();

            //Assert
            result.Should().Be(default);
        }

        [TestMethod]
        public void WhenSourceContainsOnlyOneItem_ReturnSingleItem()
        {
            //Arrange
            var source = new List<Dummy>
            {
                Fixture.Create<Dummy>()
            };

            //Act
            var result = source.GetRandom();

            //Assert
            result.Should().Be(source.Single());
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItems_ReturnAnyOfThem()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>(10).ToList();

            //Act
            var result = source.GetRandom()!;

            //Assert
            source.Should().Contain(result);
        }
    }

    [TestClass]
    public class WithReadOnlyList : Tester
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            IReadOnlyList<Dummy> source = null!;

            //Act
            var action = () => source.GetRandom();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenSourceIsEmpty_ReturnNull()
        {
            //Arrange
            IReadOnlyList<Dummy> source = new List<Dummy>();

            //Act
            var result = source.GetRandom();

            //Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void WhenSourceIsEmptyAndValueType_ReturnDefault()
        {
            //Arrange
            IReadOnlyList<int> source = new List<int>();

            //Act
            var result = source.GetRandom();

            //Assert
            result.Should().Be(default);
        }

        [TestMethod]
        public void WhenSourceContainsOnlyOneItem_ReturnSingleItem()
        {
            //Arrange
            IReadOnlyList<Dummy> source = new List<Dummy>
            {
                Fixture.Create<Dummy>()
            };

            //Act
            var result = source.GetRandom();

            //Assert
            result.Should().Be(source.Single());
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItems_ReturnAnyOfThem()
        {
            //Arrange
            IReadOnlyList<Dummy> source = Fixture.CreateMany<Dummy>(10).ToList();

            //Act
            var result = source.GetRandom()!;

            //Assert
            source.Should().Contain(result);
        }
    }

    [TestClass]
    public class WithWriteOnlyList : Tester
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            WriteOnlyList<Dummy> source = null!;

            //Act
            var action = () => source.GetRandom();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenSourceIsEmpty_ReturnNull()
        {
            //Arrange
            var source = new WriteOnlyList<Dummy>();

            //Act
            var result = source.GetRandom();

            //Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void WhenSourceIsEmptyAndValueType_ReturnDefault()
        {
            //Arrange
            var source = new WriteOnlyList<int>();

            //Act
            var result = source.GetRandom();

            //Assert
            result.Should().Be(default);
        }

        [TestMethod]
        public void WhenSourceContainsOnlyOneItem_ReturnSingleItem()
        {
            //Arrange
            var source = new WriteOnlyList<Dummy>
            {
                Fixture.Create<Dummy>()
            };

            //Act
            var result = source.GetRandom();

            //Assert
            result.Should().Be(source.Single());
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItems_ReturnAnyOfThem()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>(10).ToWriteOnlyList();

            //Act
            var result = source.GetRandom()!;

            //Assert
            source.Should().Contain(result);
        }
    }
}