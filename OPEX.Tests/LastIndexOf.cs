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
            Garbage[] source = null!;
            var item = Dummy.Create<Garbage>();

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambaAndCollectionIsNull_Throw()
        {
            //Arrange
            Garbage[] source = null!;
            var item = Dummy.Create<Func<Garbage, bool>>();

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambaAndLambdaIsNull_Throw()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToArray();
            Func<Garbage, bool> item = null!;

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToArray();

            //Act
            var result = source.LastIndexOf(Dummy.Create<Garbage>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToArray();

            //Act
            var result = source.LastIndexOf(x => x.Name == Dummy.Create<string>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionOnce_ReturnItemIndex()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToArray();
            var itemIndex = source.GetRandomIndex();
            var item = source[itemIndex];

            //Act
            var result = source.LastIndexOf(item);

            //Assert
            result.Should().Be(itemIndex);
        }

        [TestMethod]
        public void WhenUsingLambaAndItemIsInCollectionOnce_ReturnIndex()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToArray();
            var itemIndex = source.GetRandomIndex();

            //Act
            var result = source.LastIndexOf(x => x.Name == source[itemIndex].Name);

            //Assert
            result.Should().Be(itemIndex);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionMultipleTimes_ReturnOnlyTheLastOccurence()
        {
            //Arrange
            var item = Dummy.Create<Garbage>();
            var source = Dummy.CreateMany<Garbage>(3).Concat(item, item, item).ToArray();

            //Act
            var result = source.LastIndexOf(item);

            //Assert
            result.Should().Be(5);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsInCollectionMultipleTimes_ReturnOnlyTheLastOccurence()
        {
            //Arrange
            var item = Dummy.Create<Garbage>();
            var source = Dummy.CreateMany<Garbage>(3).Concat(item, item, item).ToArray();

            //Act
            var result = source.LastIndexOf(x => x.Name == item.Name);

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
            IList<Garbage> source = null!;
            var item = Dummy.Create<Garbage>();

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambaAndCollectionIsNull_Throw()
        {
            //Arrange
            IList<Garbage> source = null!;
            var item = Dummy.Create<Func<Garbage, bool>>();

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambaAndLambdaIsNull_Throw()
        {
            //Arrange
            IList<Garbage> source = Dummy.CreateMany<Garbage>().ToList();
            Func<Garbage, bool> item = null!;

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            IList<Garbage> source = Dummy.CreateMany<Garbage>().ToList();

            //Act
            var result = source.LastIndexOf(Dummy.Create<Garbage>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            IList<Garbage> source = Dummy.CreateMany<Garbage>().ToList();

            //Act
            var result = source.LastIndexOf(x => x.Name == Dummy.Create<string>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionOnce_ReturnItemIndex()
        {
            //Arrange
            IList<Garbage> source = Dummy.CreateMany<Garbage>().ToList();
            var itemIndex = source.GetRandomIndex();
            var item = source[itemIndex];

            //Act
            var result = source.LastIndexOf(item);

            //Assert
            result.Should().Be(itemIndex);
        }

        [TestMethod]
        public void WhenUsingLambaAndItemIsInCollectionOnce_ReturnIndex()
        {
            //Arrange
            IList<Garbage> source = Dummy.CreateMany<Garbage>().ToList();
            var itemIndex = source.GetRandomIndex();

            //Act
            var result = source.LastIndexOf(x => x.Name == source[itemIndex].Name);

            //Assert
            result.Should().Be(itemIndex);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionMultipleTimes_ReturnOnlyTheLastOccurence()
        {
            //Arrange
            var item = Dummy.Create<Garbage>();
            IList<Garbage> source = Dummy.CreateMany<Garbage>(3).Concat(item, item, item).ToList();

            //Act
            var result = source.LastIndexOf(item);

            //Assert
            result.Should().Be(5);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsInCollectionMultipleTimes_ReturnOnlyTheLastOccurence()
        {
            //Arrange
            var item = Dummy.Create<Garbage>();
            IList<Garbage> source = Dummy.CreateMany<Garbage>(3).Concat(item, item, item).ToList();

            //Act
            var result = source.LastIndexOf(x => x.Name == item.Name);

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
            WriteOnlyList<Garbage> source = null!;
            var item = Dummy.Create<Garbage>();

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambaAndCollectionIsNull_Throw()
        {
            //Arrange
            WriteOnlyList<Garbage> source = null!;
            var item = Dummy.Create<Func<Garbage, bool>>();

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambaAndLambdaIsNull_Throw()
        {
            //Arrange
            WriteOnlyList<Garbage> source = Dummy.CreateMany<Garbage>().ToWriteOnlyList();
            Func<Garbage, bool> item = null!;

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToWriteOnlyList();

            //Act
            var result = source.LastIndexOf(Dummy.Create<Garbage>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToWriteOnlyList();

            //Act
            var result = source.LastIndexOf(x => x.Name == Dummy.Create<string>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionOnce_ReturnItemIndex()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToWriteOnlyList();
            var itemIndex = source.GetRandomIndex();
            var item = source[itemIndex];

            //Act
            var result = source.LastIndexOf(item);

            //Assert
            result.Should().Be(itemIndex);
        }

        [TestMethod]
        public void WhenUsingLambaAndItemIsInCollectionOnce_ReturnIndex()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToWriteOnlyList();
            var itemIndex = source.GetRandomIndex();

            //Act
            var result = source.LastIndexOf(x => x.Name == source[itemIndex].Name);

            //Assert
            result.Should().Be(itemIndex);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionMultipleTimes_ReturnOnlyTheLastOccurence()
        {
            //Arrange
            var item = Dummy.Create<Garbage>();
            var source = Dummy.CreateMany<Garbage>(3).Concat(item, item, item).ToWriteOnlyList();

            //Act
            var result = source.LastIndexOf(item);

            //Assert
            result.Should().Be(5);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsInCollectionMultipleTimes_ReturnOnlyTheLastOccurence()
        {
            //Arrange
            var item = Dummy.Create<Garbage>();
            var source = Dummy.CreateMany<Garbage>(3).Concat(item, item, item).ToWriteOnlyList();

            //Act
            var result = source.LastIndexOf(x => x.Name == item.Name);

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
            IReadOnlyList<Garbage> source = null!;
            var item = Dummy.Create<Garbage>();

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambaAndCollectionIsNull_Throw()
        {
            //Arrange
            IReadOnlyList<Garbage> source = null!;
            var item = Dummy.Create<Func<Garbage, bool>>();

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambaAndLambdaIsNull_Throw()
        {
            //Arrange
            IReadOnlyList<Garbage> source = Dummy.CreateMany<Garbage>().ToList();
            Func<Garbage, bool> item = null!;

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            IReadOnlyList<Garbage> source = Dummy.CreateMany<Garbage>().ToList();

            //Act
            var result = source.LastIndexOf(Dummy.Create<Garbage>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            IReadOnlyList<Garbage> sourec = Dummy.CreateMany<Garbage>().ToList();

            //Act
            var result = sourec.LastIndexOf(x => x.Name == Dummy.Create<string>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionOnce_ReturnItemIndex()
        {
            //Arrange
            IReadOnlyList<Garbage> source = Dummy.CreateMany<Garbage>().ToList();
            var itemIndex = source.GetRandomIndex();
            var item = source[itemIndex];

            //Act
            var result = source.LastIndexOf(item);

            //Assert
            result.Should().Be(itemIndex);
        }

        [TestMethod]
        public void WhenUsingLambaAndItemIsInCollectionOnce_ReturnIndex()
        {
            //Arrange
            IReadOnlyList<Garbage> source = Dummy.CreateMany<Garbage>().ToList();
            var itemIndex = source.GetRandomIndex();

            //Act
            var result = source.LastIndexOf(x => x.Name == source[itemIndex].Name);

            //Assert
            result.Should().Be(itemIndex);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionMultipleTimes_ReturnOnlyTheLastOccurence()
        {
            //Arrange
            var item = Dummy.Create<Garbage>();
            IReadOnlyList<Garbage> source = Dummy.CreateMany<Garbage>(3).Concat(item, item, item).ToList();

            //Act
            var result = source.LastIndexOf(item);

            //Assert
            result.Should().Be(5);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsInCollectionMultipleTimes_ReturnOnlyTheLastOccurence()
        {
            //Arrange
            var item = Dummy.Create<Garbage>();
            IReadOnlyList<Garbage> source = Dummy.CreateMany<Garbage>(3).Concat(item, item, item).ToList();

            //Act
            var result = source.LastIndexOf(x => x.Name == item.Name);

            //Assert
            result.Should().Be(5);
        }
    }
}