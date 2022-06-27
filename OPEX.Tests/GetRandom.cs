namespace OPEX.Tests;

[TestClass]
public class GetRandom
{
    [TestClass]
    public class WithArray : Tester
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            Dummy[] collection = null!;

            //Act
            var action = () => collection.GetRandom();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_ReturnNull()
        {
            //Arrange
            var collection = Array.Empty<Dummy>();

            //Act
            var result = collection.GetRandom();

            //Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void WhenCollectionIsEmptyAndValueType_ReturnDefault()
        {
            //Arrange
            var collection = Array.Empty<int>();

            //Act
            var result = collection.GetRandom();

            //Assert
            result.Should().Be(default);
        }

        [TestMethod]
        public void WhenCollectionContainsOnlyOneItem_ReturnSingleItem()
        {
            //Arrange
            var collection = new[]
            {
                Fixture.Create<Dummy>()
            };

            //Act
            var result = collection.GetRandom();

            //Assert
            result.Should().Be(collection.Single());
        }

        [TestMethod]
        public void WhenCollectionContainsMultipleItems_ReturnAnyOfThem()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>(10).ToArray();

            //Act
            var result = collection.GetRandom()!;

            //Assert
            collection.Should().Contain(result);
        }
    }

    [TestClass]
    public class WithList : Tester
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            List<Dummy> collection = null!;

            //Act
            var action = () => collection.GetRandom();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_ReturnNull()
        {
            //Arrange
            var collection = new List<Dummy>();

            //Act
            var result = collection.GetRandom();

            //Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void WhenCollectionIsEmptyAndValueType_ReturnDefault()
        {
            //Arrange
            var collection = new List<int>();

            //Act
            var result = collection.GetRandom();

            //Assert
            result.Should().Be(default);
        }

        [TestMethod]
        public void WhenCollectionContainsOnlyOneItem_ReturnSingleItem()
        {
            //Arrange
            var collection = new List<Dummy>
            {
                Fixture.Create<Dummy>()
            };

            //Act
            var result = collection.GetRandom();

            //Assert
            result.Should().Be(collection.Single());
        }

        [TestMethod]
        public void WhenCollectionContainsMultipleItems_ReturnAnyOfThem()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>(10).ToList();

            //Act
            var result = collection.GetRandom()!;

            //Assert
            collection.Should().Contain(result);
        }
    }

    [TestClass]
    public class WithReadOnlyList : Tester
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = null!;

            //Act
            var action = () => collection.GetRandom();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_ReturnNull()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = new List<Dummy>();

            //Act
            var result = collection.GetRandom();

            //Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void WhenCollectionIsEmptyAndValueType_ReturnDefault()
        {
            //Arrange
            IReadOnlyList<int> collection = new List<int>();

            //Act
            var result = collection.GetRandom();

            //Assert
            result.Should().Be(default);
        }

        [TestMethod]
        public void WhenCollectionContainsOnlyOneItem_ReturnSingleItem()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = new List<Dummy>
            {
                Fixture.Create<Dummy>()
            };

            //Act
            var result = collection.GetRandom();

            //Assert
            result.Should().Be(collection.Single());
        }

        [TestMethod]
        public void WhenCollectionContainsMultipleItems_ReturnAnyOfThem()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>(10).ToList();

            //Act
            var result = collection.GetRandom()!;

            //Assert
            collection.Should().Contain(result);
        }
    }

    [TestClass]
    public class WithWriteOnlyList : Tester
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            WriteOnlyList<Dummy> collection = null!;

            //Act
            var action = () => collection.GetRandom();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_ReturnNull()
        {
            //Arrange
            var collection = new WriteOnlyList<Dummy>();

            //Act
            var result = collection.GetRandom();

            //Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void WhenCollectionIsEmptyAndValueType_ReturnDefault()
        {
            //Arrange
            var collection = new WriteOnlyList<int>();

            //Act
            var result = collection.GetRandom();

            //Assert
            result.Should().Be(default);
        }

        [TestMethod]
        public void WhenCollectionContainsOnlyOneItem_ReturnSingleItem()
        {
            //Arrange
            var collection = new WriteOnlyList<Dummy>
            {
                Fixture.Create<Dummy>()
            };

            //Act
            var result = collection.GetRandom();

            //Assert
            result.Should().Be(collection.Single());
        }

        [TestMethod]
        public void WhenCollectionContainsMultipleItems_ReturnAnyOfThem()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>(10).ToWriteOnlyList();

            //Act
            var result = collection.GetRandom()!;

            //Assert
            collection.Should().Contain(result);
        }
    }
}