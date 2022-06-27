namespace OPEX.Tests;

[TestClass]
public class FirstIndexOf
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
            var action = () => collection.FirstIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambaAndCollectionIsNull_Throw()
        {
            //Arrange
            Dummy[] collection = null!;
            var match = Fixture.Create<Func<Dummy, bool>>();

            //Act
            var action = () => collection.FirstIndexOf(match);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambaAndLambdaIsNull_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();
            Func<Dummy, bool> match = null!;

            //Act
            var action = () => collection.FirstIndexOf(match);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();

            //Act
            var result = collection.FirstIndexOf(Fixture.Create<Dummy>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();

            //Act
            var result = collection.FirstIndexOf(x => x.Name == Fixture.Create<string>());

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
            var result = collection.FirstIndexOf(item);

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
            var result = collection.FirstIndexOf(x => x.Name == collection[itemIndex].Name);

            //Assert
            result.Should().Be(itemIndex);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionMultipleTimes_ReturnOnlyTheFirstOccurence()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            var collection = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToArray();

            //Act
            var result = collection.FirstIndexOf(item);

            //Assert
            result.Should().Be(3);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsInCollectionMultipleTimes_ReturnOnlyTheFirstOccurence()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            var collection = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToArray();

            //Act
            var result = collection.FirstIndexOf(x => x.Name == item.Name);

            //Assert
            result.Should().Be(3);
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
            var item = Fixture.Create<Dummy>();

            //Act
            var action = () => collection.FirstIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambaAndCollectionIsNull_Throw()
        {
            //Arrange
            List<Dummy> collection = null!;
            var item = Fixture.Create<Func<Dummy, bool>>();

            //Act
            var action = () => collection.FirstIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambaAndLambdaIsNull_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToList();
            Func<Dummy, bool> match = null!;

            //Act
            var action = () => collection.FirstIndexOf(match);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToList();

            //Act
            var result = collection.FirstIndexOf(Fixture.Create<Dummy>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToList();

            //Act
            var result = collection.FirstIndexOf(x => x.Name == Fixture.Create<string>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionOnce_ReturnItemIndex()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToList();
            var itemIndex = collection.GetRandomIndex();
            var item = collection[itemIndex];

            //Act
            var result = collection.FirstIndexOf(item);

            //Assert
            result.Should().Be(itemIndex);
        }

        [TestMethod]
        public void WhenUsingLambaAndItemIsInCollectionOnce_ReturnIndex()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToList();
            var itemIndex = collection.GetRandomIndex();

            //Act
            var result = collection.FirstIndexOf(x => x.Name == collection[itemIndex].Name);

            //Assert
            result.Should().Be(itemIndex);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionMultipleTimes_ReturnOnlyTheFirstOccurence()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            var collection = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToList();

            //Act
            var result = collection.FirstIndexOf(item);

            //Assert
            result.Should().Be(3);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsInCollectionMultipleTimes_ReturnOnlyTheFirstOccurence()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            var collection = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToList();

            //Act
            var result = collection.FirstIndexOf(x => x.Name == item.Name);

            //Assert
            result.Should().Be(3);
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
            var action = () => collection.FirstIndexOf(item);

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
            var action = () => collection.FirstIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambaAndLambdaIsNull_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToWriteOnlyList();
            Func<Dummy, bool> match = null!;

            //Act
            var action = () => collection.FirstIndexOf(match);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToWriteOnlyList();

            //Act
            var result = collection.FirstIndexOf(Fixture.Create<Dummy>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToWriteOnlyList();

            //Act
            var result = collection.FirstIndexOf(x => x.Name == Fixture.Create<string>());

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
            var result = collection.FirstIndexOf(item);

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
            var result = collection.FirstIndexOf(x => x.Name == collection[itemIndex].Name);

            //Assert
            result.Should().Be(itemIndex);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionMultipleTimes_ReturnOnlyTheFirstOccurence()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            var collection = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToWriteOnlyList();

            //Act
            var result = collection.FirstIndexOf(item);

            //Assert
            result.Should().Be(3);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsInCollectionMultipleTimes_ReturnOnlyTheFirstOccurence()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            var collection = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToWriteOnlyList();

            //Act
            var result = collection.FirstIndexOf(x => x.Name == item.Name);

            //Assert
            result.Should().Be(3);
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
            var action = () => collection.FirstIndexOf(item);

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
            var action = () => collection.FirstIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambaAndLambdaIsNull_Throw()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();
            Func<Dummy, bool> match = null!;

            //Act
            var action = () => collection.FirstIndexOf(match);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();

            //Act
            var result = collection.FirstIndexOf(Fixture.Create<Dummy>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>().ToList();

            //Act
            var result = collection.FirstIndexOf(x => x.Name == Fixture.Create<string>());

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
            var result = collection.FirstIndexOf(item);

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
            var result = collection.FirstIndexOf(x => x.Name == collection[itemIndex].Name);

            //Assert
            result.Should().Be(itemIndex);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionMultipleTimes_ReturnOnlyTheFirstOccurence()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToList();

            //Act
            var result = collection.FirstIndexOf(item);

            //Assert
            result.Should().Be(3);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsInCollectionMultipleTimes_ReturnOnlyTheFirstOccurence()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToList();

            //Act
            var result = collection.FirstIndexOf(x => x.Name == item.Name);

            //Assert
            result.Should().Be(3);
        }
    }
}