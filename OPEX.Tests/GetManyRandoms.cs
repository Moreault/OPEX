namespace OPEX.Tests;

[TestClass]
public class GetManyRandoms
{
    [TestClass]
    public class WithArray : Tester
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            Dummy[] collection = null!;
            var numberOfElements = Fixture.Create<int>();

            //Act
            var action = () => collection.GetManyRandoms(numberOfElements);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenNumberOfElementsIsNegative_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();
            var numberOfElements = -Fixture.Create<int>();

            //Act
            var action = () => collection.GetManyRandoms(numberOfElements);

            //Assert
            action.Should().Throw<ArgumentException>().WithMessage(string.Format(Exceptions.CannotGetManyRandomsBecauseNumberNegative, numberOfElements));
        }

        [TestMethod]
        public void WhenNumberOfElementsIsZero_ReturnEmpty()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();
            var numberOfElements = 0;

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_ReturnEmpty()
        {
            //Arrange
            var collection = Array.Empty<Dummy>();
            var numberOfElements = Fixture.Create<int>();

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenNumberOfElementsIsOne_ReturnSingleItemFromCollection()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();
            var numberOfElements = 1;

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().HaveCount(1);
            collection.Should().Contain(result);
        }

        [TestMethod]
        public void WhenNumberOfElementsIsTheSameAsSizeOfCollection_ReturnEntireCollection()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();
            var numberOfElements = collection.Length;

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().BeEquivalentTo(collection);
        }

        [TestMethod]
        public void WhenNumberOfElementsIsGreaterThanSizeOfCollection_ReturnEntireCollection()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();
            var numberOfElements = collection.Length + Fixture.Create<int>();

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().BeEquivalentTo(collection);
        }

        [TestMethod]
        public void WhenNumberOfElementsIsSmallerThanSizeOfCollection_ReturnRandomElementsFromCollection()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>(15).ToArray();
            var numberOfElements = 5;

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().HaveCount(5);
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
            var numberOfElements = Fixture.Create<int>();

            //Act
            var action = () => collection.GetManyRandoms(numberOfElements);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenNumberOfElementsIsNegative_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToList();
            var numberOfElements = -Fixture.Create<int>();

            //Act
            var action = () => collection.GetManyRandoms(numberOfElements);

            //Assert
            action.Should().Throw<ArgumentException>().WithMessage(string.Format(Exceptions.CannotGetManyRandomsBecauseNumberNegative, numberOfElements));
        }

        [TestMethod]
        public void WhenNumberOfElementsIsZero_ReturnEmpty()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToList();
            var numberOfElements = 0;

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_ReturnEmpty()
        {
            //Arrange
            var collection = new List<Dummy>();
            var numberOfElements = Fixture.Create<int>();

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenNumberOfElementsIsOne_ReturnSingleItemFromCollection()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToList();
            var numberOfElements = 1;

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().HaveCount(1);
            collection.Should().Contain(result);
        }

        [TestMethod]
        public void WhenNumberOfElementsIsTheSameAsSizeOfCollection_ReturnEntireCollection()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToList();
            var numberOfElements = collection.Count;

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().BeEquivalentTo(collection);
        }

        [TestMethod]
        public void WhenNumberOfElementsIsGreaterThanSizeOfCollection_ReturnEntireCollection()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToList();
            var numberOfElements = collection.Count + Fixture.Create<int>();

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().BeEquivalentTo(collection);
        }

        [TestMethod]
        public void WhenNumberOfElementsIsSmallerThanSizeOfCollection_ReturnRandomElementsFromCollection()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>(15).ToList();
            var numberOfElements = 5;

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().HaveCount(5);
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
            var numberOfElements = Fixture.Create<int>();

            //Act
            var action = () => collection.GetManyRandoms(numberOfElements);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenNumberOfElementsIsNegative_Throw()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            var numberOfElements = -Fixture.Create<int>();

            //Act
            var action = () => collection.GetManyRandoms(numberOfElements);

            //Assert
            action.Should().Throw<ArgumentException>().WithMessage(string.Format(Exceptions.CannotGetManyRandomsBecauseNumberNegative, numberOfElements));
        }

        [TestMethod]
        public void WhenNumberOfElementsIsZero_ReturnEmpty()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            var numberOfElements = 0;

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_ReturnEmpty()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = new List<Dummy>();
            var numberOfElements = Fixture.Create<int>();

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenNumberOfElementsIsOne_ReturnSingleItemFromCollection()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            var numberOfElements = 1;

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().HaveCount(1);
            collection.Should().Contain(result);
        }

        [TestMethod]
        public void WhenNumberOfElementsIsTheSameAsSizeOfCollection_ReturnEntireCollection()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            var numberOfElements = collection.Count;

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().BeEquivalentTo(collection);
        }

        [TestMethod]
        public void WhenNumberOfElementsIsGreaterThanSizeOfCollection_ReturnEntireCollection()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            var numberOfElements = collection.Count + Fixture.Create<int>();

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().BeEquivalentTo(collection);
        }

        [TestMethod]
        public void WhenNumberOfElementsIsSmallerThanSizeOfCollection_ReturnRandomElementsFromCollection()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>(15).ToList();
            var numberOfElements = 5;

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().HaveCount(5);
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
            var numberOfElements = Fixture.Create<int>();

            //Act
            var action = () => collection.GetManyRandoms(numberOfElements);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenNumberOfElementsIsNegative_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToWriteOnlyList();
            var numberOfElements = -Fixture.Create<int>();

            //Act
            var action = () => collection.GetManyRandoms(numberOfElements);

            //Assert
            action.Should().Throw<ArgumentException>().WithMessage(string.Format(Exceptions.CannotGetManyRandomsBecauseNumberNegative, numberOfElements));
        }

        [TestMethod]
        public void WhenNumberOfElementsIsZero_ReturnEmpty()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToWriteOnlyList();
            var numberOfElements = 0;

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenCollectionIsEmpty_ReturnEmpty()
        {
            //Arrange
            var collection = new WriteOnlyList<Dummy>();
            var numberOfElements = Fixture.Create<int>();

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenNumberOfElementsIsOne_ReturnSingleItemFromCollection()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToWriteOnlyList();
            var numberOfElements = 1;

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().HaveCount(1);
            collection.Should().Contain(result);
        }

        [TestMethod]
        public void WhenNumberOfElementsIsTheSameAsSizeOfCollection_ReturnEntireCollection()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToWriteOnlyList();
            var numberOfElements = collection.Count;

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().BeEquivalentTo(collection);
        }

        [TestMethod]
        public void WhenNumberOfElementsIsGreaterThanSizeOfCollection_ReturnEntireCollection()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToWriteOnlyList();
            var numberOfElements = collection.Count + Fixture.Create<int>();

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().BeEquivalentTo(collection);
        }

        [TestMethod]
        public void WhenNumberOfElementsIsSmallerThanSizeOfCollection_ReturnRandomElementsFromCollection()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>(15).ToWriteOnlyList();
            var numberOfElements = 5;

            //Act
            var result = collection.GetManyRandoms(numberOfElements);

            //Assert
            result.Should().HaveCount(5);
            collection.Should().Contain(result);
        }
    }
}