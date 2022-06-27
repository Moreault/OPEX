namespace OPEX.Tests;

[TestClass]
public class LastIndexOf
{
    [TestClass]
    public class WithArray : Tester
    {
        [TestMethod]
        public void WhenUsingItemAndCollectionIsNull_Throw()
        {
            //Arrange
            Dummy[] collection = null!;
            var item = Fixture.Create<Dummy>();

            //Act
            var action = () => collection.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambaAndCollectionIsNull_Throw()
        {
            //Arrange
            Dummy[] collection = null!;
            var item = Fixture.Create<Func<Dummy, bool>>();

            //Act
            var action = () => collection.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambaAndLambdaIsNull_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();
            Func<Dummy, bool> item = null!;

            //Act
            var action = () => collection.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();

            //Act
            var result = collection.LastIndexOf(Fixture.Create<Dummy>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();

            //Act
            var result = collection.LastIndexOf(x => x.Name == Fixture.Create<string>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionOnce_ReturnItemIndex()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();
            var itemIndex = collection.GetRandomIndex();
            var item = collection[itemIndex];

            //Act
            var result = collection.LastIndexOf(item);

            //Assert
            result.Should().Be(itemIndex);
        }

        [TestMethod]
        public void WhenUsingLambaAndItemIsInCollectionOnce_ReturnIndex()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();
            var itemIndex = collection.GetRandomIndex();

            //Act
            var result = collection.LastIndexOf(x => x.Name == collection[itemIndex].Name);

            //Assert
            result.Should().Be(itemIndex);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionMultipleTimes_ReturnOnlyTheLastOccurence()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            var collection = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToArray();

            //Act
            var result = collection.LastIndexOf(item);

            //Assert
            result.Should().Be(5);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsInCollectionMultipleTimes_ReturnOnlyTheLastOccurence()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            var collection = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToArray();

            //Act
            var result = collection.LastIndexOf(x => x.Name == item.Name);

            //Assert
            result.Should().Be(5);
        }
    }

    [TestClass]
    public class WithIList : Tester
    {
        [TestMethod]
        public void WhenUsingItemAndCollectionIsNull_Throw()
        {
            //Arrange
            IList<Dummy> collection = null!;
            var item = Fixture.Create<Dummy>();

            //Act
            var action = () => collection.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambaAndCollectionIsNull_Throw()
        {
            //Arrange
            IList<Dummy> collection = null!;
            var item = Fixture.Create<Func<Dummy, bool>>();

            //Act
            var action = () => collection.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambaAndLambdaIsNull_Throw()
        {
            //Arrange
            IList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            Func<Dummy, bool> item = null!;

            //Act
            var action = () => collection.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            IList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();

            //Act
            var result = collection.LastIndexOf(Fixture.Create<Dummy>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            IList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();

            //Act
            var result = collection.LastIndexOf(x => x.Name == Fixture.Create<string>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionOnce_ReturnItemIndex()
        {
            //Arrange
            IList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            var itemIndex = collection.GetRandomIndex();
            var item = collection[itemIndex];

            //Act
            var result = collection.LastIndexOf(item);

            //Assert
            result.Should().Be(itemIndex);
        }

        [TestMethod]
        public void WhenUsingLambaAndItemIsInCollectionOnce_ReturnIndex()
        {
            //Arrange
            IList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            var itemIndex = collection.GetRandomIndex();

            //Act
            var result = collection.LastIndexOf(x => x.Name == collection[itemIndex].Name);

            //Assert
            result.Should().Be(itemIndex);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionMultipleTimes_ReturnOnlyTheLastOccurence()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            IList<Dummy> collection = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToList();

            //Act
            var result = collection.LastIndexOf(item);

            //Assert
            result.Should().Be(5);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsInCollectionMultipleTimes_ReturnOnlyTheLastOccurence()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            IList<Dummy> collection = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToList();

            //Act
            var result = collection.LastIndexOf(x => x.Name == item.Name);

            //Assert
            result.Should().Be(5);
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
            var item = Fixture.Create<Dummy>();

            //Act
            var action = () => collection.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambaAndCollectionIsNull_Throw()
        {
            //Arrange
            WriteOnlyList<Dummy> collection = null!;
            var item = Fixture.Create<Func<Dummy, bool>>();

            //Act
            var action = () => collection.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambaAndLambdaIsNull_Throw()
        {
            //Arrange
            WriteOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>().ToWriteOnlyList();
            Func<Dummy, bool> item = null!;

            //Act
            var action = () => collection.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToWriteOnlyList();

            //Act
            var result = collection.LastIndexOf(Fixture.Create<Dummy>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToWriteOnlyList();

            //Act
            var result = collection.LastIndexOf(x => x.Name == Fixture.Create<string>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionOnce_ReturnItemIndex()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToWriteOnlyList();
            var itemIndex = collection.GetRandomIndex();
            var item = collection[itemIndex];

            //Act
            var result = collection.LastIndexOf(item);

            //Assert
            result.Should().Be(itemIndex);
        }

        [TestMethod]
        public void WhenUsingLambaAndItemIsInCollectionOnce_ReturnIndex()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToWriteOnlyList();
            var itemIndex = collection.GetRandomIndex();

            //Act
            var result = collection.LastIndexOf(x => x.Name == collection[itemIndex].Name);

            //Assert
            result.Should().Be(itemIndex);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionMultipleTimes_ReturnOnlyTheLastOccurence()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            var collection = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToWriteOnlyList();

            //Act
            var result = collection.LastIndexOf(item);

            //Assert
            result.Should().Be(5);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsInCollectionMultipleTimes_ReturnOnlyTheLastOccurence()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            var collection = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToWriteOnlyList();

            //Act
            var result = collection.LastIndexOf(x => x.Name == item.Name);

            //Assert
            result.Should().Be(5);
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
            var item = Fixture.Create<Dummy>();

            //Act
            var action = () => collection.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambaAndCollectionIsNull_Throw()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = null!;
            var item = Fixture.Create<Func<Dummy, bool>>();

            //Act
            var action = () => collection.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambaAndLambdaIsNull_Throw()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            Func<Dummy, bool> item = null!;

            //Act
            var action = () => collection.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();

            //Act
            var result = collection.LastIndexOf(Fixture.Create<Dummy>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();

            //Act
            var result = collection.LastIndexOf(x => x.Name == Fixture.Create<string>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionOnce_ReturnItemIndex()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            var itemIndex = collection.GetRandomIndex();
            var item = collection[itemIndex];

            //Act
            var result = collection.LastIndexOf(item);

            //Assert
            result.Should().Be(itemIndex);
        }

        [TestMethod]
        public void WhenUsingLambaAndItemIsInCollectionOnce_ReturnIndex()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            var itemIndex = collection.GetRandomIndex();

            //Act
            var result = collection.LastIndexOf(x => x.Name == collection[itemIndex].Name);

            //Assert
            result.Should().Be(itemIndex);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionMultipleTimes_ReturnOnlyTheLastOccurence()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToList();

            //Act
            var result = collection.LastIndexOf(item);

            //Assert
            result.Should().Be(5);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsInCollectionMultipleTimes_ReturnOnlyTheLastOccurence()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToList();

            //Act
            var result = collection.LastIndexOf(x => x.Name == item.Name);

            //Assert
            result.Should().Be(5);
        }
    }
}