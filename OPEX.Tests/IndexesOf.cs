namespace OPEX.Tests;

[TestClass]
public class IndexesOf
{
    [TestClass]
    public class WithArray : Tester
    {
        [TestMethod]
        public void WhenUsingItemAndCollectionIsNull_Throw()
        {
            //Arrange
            Dummy[] collection = null!;

            //Act
            var action = () => collection.IndexesOf(Fixture.Create<Dummy>());

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambdaAndCollectionIsNull_Throw()
        {
            //Arrange
            Dummy[] collection = null!;

            //Act
            var action = () => collection.IndexesOf(Fixture.Create<Func<Dummy, bool>>());

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambdaAndLambdaIsNull_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();
            Func<Dummy, bool> match = null!;

            //Act
            var action = () => collection.IndexesOf(match);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void WhenUsingItemAndCollectionIsEmpty_ReturnEmpty()
        {
            //Arrange
            var collection = Array.Empty<Dummy>();
            var item = Fixture.Create<Dummy>();

            //Act
            var result = collection.IndexesOf(item);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenUsingLambdaAndCollectionIsEmpty_ReturnEmpty()
        {
            //Arrange
            var collection = Array.Empty<Dummy>();
            var item = Fixture.Create<Dummy>();

            //Act
            var result = collection.IndexesOf(x => x.Name == item.Name);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsNotInCollection_ReturnEmpty()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();
            var item = Fixture.Create<Dummy>();

            //Act
            var result = collection.IndexesOf(item);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsNotInCollection_ReturnEmpty()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();
            var item = Fixture.Create<Dummy>();

            //Act
            var result = collection.IndexesOf(x => x.Name == item.Name);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenUsingItemAndThereIsOneCorrespondingItem_ReturnSingleItem()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();
            var itemIndex = collection.GetRandomIndex();
            var item = collection[itemIndex];

            //Act
            var result = collection.IndexesOf(item);

            //Assert
            result.Should().BeEquivalentTo(new List<int> { itemIndex });
        }

        [TestMethod]
        public void WhenUsingLambdaAndThereIsOneCorrespondingItem_ReturnSingleItem()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();
            var itemIndex = collection.GetRandomIndex();
            var item = collection[itemIndex];

            //Act
            var result = collection.IndexesOf(x => x.Name == item.Name);

            //Assert
            result.Should().BeEquivalentTo(new List<int> { itemIndex });
        }

        [TestMethod]
        public void WhenUsingItemAndThereAreMultipleOccurences_ReturnAllOccurences()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            var collection = Fixture.CreateMany<Dummy>().Concat(item, Fixture.Create<Dummy>(), item, item, Fixture.Create<Dummy>(), Fixture.Create<Dummy>()).ToArray();

            //Act
            var result = collection.IndexesOf(item);

            //Assert
            result.Should().BeEquivalentTo(new List<int> { 3, 5, 6 });
        }

        [TestMethod]
        public void WhenUsingLambdaAndThereAreMultipleOccurences_ReturnAllOccurences()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            var collection = Fixture.CreateMany<Dummy>().Concat(item, Fixture.Create<Dummy>(), item, item, Fixture.Create<Dummy>(), Fixture.Create<Dummy>()).ToArray();

            //Act
            var result = collection.IndexesOf(x => x.Name == item.Name);

            //Assert
            result.Should().BeEquivalentTo(new List<int> { 3, 5, 6 });
        }
    }

    [TestClass]
    public class WithList : Tester
    {
        [TestMethod]
        public void WhenUsingItemAndCollectionIsNull_Throw()
        {
            //Arrange
            List<Dummy> collection = null!;

            //Act
            var action = () => collection.IndexesOf(Fixture.Create<Dummy>());

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambdaAndCollectionIsNull_Throw()
        {
            //Arrange
            List<Dummy> collection = null!;

            //Act
            var action = () => collection.IndexesOf(Fixture.Create<Func<Dummy, bool>>());

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambdaAndLambdaIsNull_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToList();
            Func<Dummy, bool> match = null!;

            //Act
            var action = () => collection.IndexesOf(match);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void WhenUsingItemAndCollectionIsEmpty_ReturnEmpty()
        {
            //Arrange
            var collection = new List<Dummy>();
            var item = Fixture.Create<Dummy>();

            //Act
            var result = collection.IndexesOf(item);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenUsingLambdaAndCollectionIsEmpty_ReturnEmpty()
        {
            //Arrange
            var collection = new List<Dummy>();
            var item = Fixture.Create<Dummy>();

            //Act
            var result = collection.IndexesOf(x => x.Name == item.Name);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsNotInCollection_ReturnEmpty()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToList();
            var item = Fixture.Create<Dummy>();

            //Act
            var result = collection.IndexesOf(item);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsNotInCollection_ReturnEmpty()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToList();
            var item = Fixture.Create<Dummy>();

            //Act
            var result = collection.IndexesOf(x => x.Name == item.Name);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenUsingItemAndThereIsOneCorrespondingItem_ReturnSingleItem()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToList();
            var itemIndex = collection.GetRandomIndex();
            var item = collection[itemIndex];

            //Act
            var result = collection.IndexesOf(item);

            //Assert
            result.Should().BeEquivalentTo(new List<int> { itemIndex });
        }

        [TestMethod]
        public void WhenUsingLambdaAndThereIsOneCorrespondingItem_ReturnSingleItem()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToList();
            var itemIndex = collection.GetRandomIndex();
            var item = collection[itemIndex];

            //Act
            var result = collection.IndexesOf(x => x.Name == item.Name);

            //Assert
            result.Should().BeEquivalentTo(new List<int> { itemIndex });
        }

        [TestMethod]
        public void WhenUsingItemAndThereAreMultipleOccurences_ReturnAllOccurences()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            var collection = Fixture.CreateMany<Dummy>().Concat(item, Fixture.Create<Dummy>(), item, item, Fixture.Create<Dummy>(), Fixture.Create<Dummy>()).ToList();

            //Act
            var result = collection.IndexesOf(item);

            //Assert
            result.Should().BeEquivalentTo(new List<int> { 3, 5, 6 });
        }

        [TestMethod]
        public void WhenUsingLambdaAndThereAreMultipleOccurences_ReturnAllOccurences()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            var collection = Fixture.CreateMany<Dummy>().Concat(item, Fixture.Create<Dummy>(), item, item, Fixture.Create<Dummy>(), Fixture.Create<Dummy>()).ToList();

            //Act
            var result = collection.IndexesOf(x => x.Name == item.Name);

            //Assert
            result.Should().BeEquivalentTo(new List<int> { 3, 5, 6 });
        }
    }

    [TestClass]
    public class WithWriteOnlyList : Tester
    {
        [TestMethod]
        public void WhenUsingItemAndCollectionIsNull_Throw()
        {
            //Arrange
            WriteOnlyList<Dummy> collection = null!;

            //Act
            var action = () => collection.IndexesOf(Fixture.Create<Dummy>());

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambdaAndCollectionIsNull_Throw()
        {
            //Arrange
            WriteOnlyList<Dummy> collection = null!;

            //Act
            var action = () => collection.IndexesOf(Fixture.Create<Func<Dummy, bool>>());

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambdaAndLambdaIsNull_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToWriteOnlyList();
            Func<Dummy, bool> match = null!;

            //Act
            var action = () => collection.IndexesOf(match);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void WhenUsingItemAndCollectionIsEmpty_ReturnEmpty()
        {
            //Arrange
            var collection = new WriteOnlyList<Dummy>();
            var item = Fixture.Create<Dummy>();

            //Act
            var result = collection.IndexesOf(item);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenUsingLambdaAndCollectionIsEmpty_ReturnEmpty()
        {
            //Arrange
            var collection = new WriteOnlyList<Dummy>();
            var item = Fixture.Create<Dummy>();

            //Act
            var result = collection.IndexesOf(x => x.Name == item.Name);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsNotInCollection_ReturnEmpty()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToWriteOnlyList();
            var item = Fixture.Create<Dummy>();

            //Act
            var result = collection.IndexesOf(item);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsNotInCollection_ReturnEmpty()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToWriteOnlyList();
            var item = Fixture.Create<Dummy>();

            //Act
            var result = collection.IndexesOf(x => x.Name == item.Name);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenUsingItemAndThereIsOneCorrespondingItem_ReturnSingleItem()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToWriteOnlyList();
            var itemIndex = collection.GetRandomIndex();
            var item = collection[itemIndex];

            //Act
            var result = collection.IndexesOf(item);

            //Assert
            result.Should().BeEquivalentTo(new List<int> { itemIndex });
        }

        [TestMethod]
        public void WhenUsingLambdaAndThereIsOneCorrespondingItem_ReturnSingleItem()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToWriteOnlyList();
            var itemIndex = collection.GetRandomIndex();
            var item = collection[itemIndex];

            //Act
            var result = collection.IndexesOf(x => x.Name == item.Name);

            //Assert
            result.Should().BeEquivalentTo(new List<int> { itemIndex });
        }

        [TestMethod]
        public void WhenUsingItemAndThereAreMultipleOccurences_ReturnAllOccurences()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            var collection = Fixture.CreateMany<Dummy>().Concat(item, Fixture.Create<Dummy>(), item, item, Fixture.Create<Dummy>(), Fixture.Create<Dummy>()).ToWriteOnlyList();

            //Act
            var result = collection.IndexesOf(item);

            //Assert
            result.Should().BeEquivalentTo(new List<int> { 3, 5, 6 });
        }

        [TestMethod]
        public void WhenUsingLambdaAndThereAreMultipleOccurences_ReturnAllOccurences()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            var collection = Fixture.CreateMany<Dummy>().Concat(item, Fixture.Create<Dummy>(), item, item, Fixture.Create<Dummy>(), Fixture.Create<Dummy>()).ToWriteOnlyList();

            //Act
            var result = collection.IndexesOf(x => x.Name == item.Name);

            //Assert
            result.Should().BeEquivalentTo(new List<int> { 3, 5, 6 });
        }
    }

    [TestClass]
    public class WithReadOnlyList : Tester
    {
        [TestMethod]
        public void WhenUsingItemAndCollectionIsNull_Throw()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = null!;

            //Act
            var action = () => collection.IndexesOf(Fixture.Create<Dummy>());

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambdaAndCollectionIsNull_Throw()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = null!;

            //Act
            var action = () => collection.IndexesOf(Fixture.Create<Func<Dummy, bool>>());

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambdaAndLambdaIsNull_Throw()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            Func<Dummy, bool> match = null!;

            //Act
            var action = () => collection.IndexesOf(match);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void WhenUsingItemAndCollectionIsEmpty_ReturnEmpty()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = new List<Dummy>();
            var item = Fixture.Create<Dummy>();

            //Act
            var result = collection.IndexesOf(item);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenUsingLambdaAndCollectionIsEmpty_ReturnEmpty()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = new List<Dummy>();
            var item = Fixture.Create<Dummy>();

            //Act
            var result = collection.IndexesOf(x => x.Name == item.Name);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsNotInCollection_ReturnEmpty()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            var item = Fixture.Create<Dummy>();

            //Act
            var result = collection.IndexesOf(item);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsNotInCollection_ReturnEmpty()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            var item = Fixture.Create<Dummy>();

            //Act
            var result = collection.IndexesOf(x => x.Name == item.Name);

            //Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void WhenUsingItemAndThereIsOneCorrespondingItem_ReturnSingleItem()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            var itemIndex = collection.GetRandomIndex();
            var item = collection[itemIndex];

            //Act
            var result = collection.IndexesOf(item);

            //Assert
            result.Should().BeEquivalentTo(new List<int> { itemIndex });
        }

        [TestMethod]
        public void WhenUsingLambdaAndThereIsOneCorrespondingItem_ReturnSingleItem()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            var itemIndex = collection.GetRandomIndex();
            var item = collection[itemIndex];

            //Act
            var result = collection.IndexesOf(x => x.Name == item.Name);

            //Assert
            result.Should().BeEquivalentTo(new List<int> { itemIndex });
        }

        [TestMethod]
        public void WhenUsingItemAndThereAreMultipleOccurences_ReturnAllOccurences()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>().Concat(item, Fixture.Create<Dummy>(), item, item, Fixture.Create<Dummy>(), Fixture.Create<Dummy>()).ToList();

            //Act
            var result = collection.IndexesOf(item);

            //Assert
            result.Should().BeEquivalentTo(new List<int> { 3, 5, 6 });
        }

        [TestMethod]
        public void WhenUsingLambdaAndThereAreMultipleOccurences_ReturnAllOccurences()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>().Concat(item, Fixture.Create<Dummy>(), item, item, Fixture.Create<Dummy>(), Fixture.Create<Dummy>()).ToList();

            //Act
            var result = collection.IndexesOf(x => x.Name == item.Name);

            //Assert
            result.Should().BeEquivalentTo(new List<int> { 3, 5, 6 });
        }
    }
}