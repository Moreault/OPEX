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
            Dummy[] source = null!;
            var item = Fixture.Create<Dummy>();

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambaAndCollectionIsNull_Throw()
        {
            //Arrange
            Dummy[] source = null!;
            var item = Fixture.Create<Func<Dummy, bool>>();

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambaAndLambdaIsNull_Throw()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToArray();
            Func<Dummy, bool> item = null!;

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToArray();

            //Act
            var result = source.LastIndexOf(Fixture.Create<Dummy>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToArray();

            //Act
            var result = source.LastIndexOf(x => x.Name == Fixture.Create<string>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionOnce_ReturnItemIndex()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToArray();
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
            var source = Fixture.CreateMany<Dummy>().ToArray();
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
            var item = Fixture.Create<Dummy>();
            var source = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToArray();

            //Act
            var result = source.LastIndexOf(item);

            //Assert
            result.Should().Be(5);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsInCollectionMultipleTimes_ReturnOnlyTheLastOccurence()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            var source = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToArray();

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
            IList<Dummy> source = null!;
            var item = Fixture.Create<Dummy>();

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambaAndCollectionIsNull_Throw()
        {
            //Arrange
            IList<Dummy> source = null!;
            var item = Fixture.Create<Func<Dummy, bool>>();

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambaAndLambdaIsNull_Throw()
        {
            //Arrange
            IList<Dummy> source = Fixture.CreateMany<Dummy>().ToList();
            Func<Dummy, bool> item = null!;

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            IList<Dummy> source = Fixture.CreateMany<Dummy>().ToList();

            //Act
            var result = source.LastIndexOf(Fixture.Create<Dummy>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            IList<Dummy> source = Fixture.CreateMany<Dummy>().ToList();

            //Act
            var result = source.LastIndexOf(x => x.Name == Fixture.Create<string>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionOnce_ReturnItemIndex()
        {
            //Arrange
            IList<Dummy> source = Fixture.CreateMany<Dummy>().ToList();
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
            IList<Dummy> source = Fixture.CreateMany<Dummy>().ToList();
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
            var item = Fixture.Create<Dummy>();
            IList<Dummy> source = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToList();

            //Act
            var result = source.LastIndexOf(item);

            //Assert
            result.Should().Be(5);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsInCollectionMultipleTimes_ReturnOnlyTheLastOccurence()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            IList<Dummy> source = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToList();

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
            WriteOnlyList<Dummy> source = null!;
            var item = Fixture.Create<Dummy>();

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambaAndCollectionIsNull_Throw()
        {
            //Arrange
            WriteOnlyList<Dummy> source = null!;
            var item = Fixture.Create<Func<Dummy, bool>>();

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambaAndLambdaIsNull_Throw()
        {
            //Arrange
            WriteOnlyList<Dummy> source = Fixture.CreateMany<Dummy>().ToWriteOnlyList();
            Func<Dummy, bool> item = null!;

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToWriteOnlyList();

            //Act
            var result = source.LastIndexOf(Fixture.Create<Dummy>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToWriteOnlyList();

            //Act
            var result = source.LastIndexOf(x => x.Name == Fixture.Create<string>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionOnce_ReturnItemIndex()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToWriteOnlyList();
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
            var source = Fixture.CreateMany<Dummy>().ToWriteOnlyList();
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
            var item = Fixture.Create<Dummy>();
            var source = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToWriteOnlyList();

            //Act
            var result = source.LastIndexOf(item);

            //Assert
            result.Should().Be(5);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsInCollectionMultipleTimes_ReturnOnlyTheLastOccurence()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            var source = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToWriteOnlyList();

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
            IReadOnlyList<Dummy> source = null!;
            var item = Fixture.Create<Dummy>();

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambaAndCollectionIsNull_Throw()
        {
            //Arrange
            IReadOnlyList<Dummy> source = null!;
            var item = Fixture.Create<Func<Dummy, bool>>();

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambaAndLambdaIsNull_Throw()
        {
            //Arrange
            IReadOnlyList<Dummy> source = Fixture.CreateMany<Dummy>().ToList();
            Func<Dummy, bool> item = null!;

            //Act
            var action = () => source.LastIndexOf(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            IReadOnlyList<Dummy> source = Fixture.CreateMany<Dummy>().ToList();

            //Act
            var result = source.LastIndexOf(Fixture.Create<Dummy>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsNotInCollection_ReturnMinusOne()
        {
            //Arrange
            IReadOnlyList<Dummy> sourec = Fixture.CreateMany<Dummy>().ToList();

            //Act
            var result = sourec.LastIndexOf(x => x.Name == Fixture.Create<string>());

            //Assert
            result.Should().Be(-1);
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsInCollectionOnce_ReturnItemIndex()
        {
            //Arrange
            IReadOnlyList<Dummy> source = Fixture.CreateMany<Dummy>().ToList();
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
            IReadOnlyList<Dummy> source = Fixture.CreateMany<Dummy>().ToList();
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
            var item = Fixture.Create<Dummy>();
            IReadOnlyList<Dummy> source = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToList();

            //Act
            var result = source.LastIndexOf(item);

            //Assert
            result.Should().Be(5);
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemIsInCollectionMultipleTimes_ReturnOnlyTheLastOccurence()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            IReadOnlyList<Dummy> source = Fixture.CreateMany<Dummy>(3).Concat(item, item, item).ToList();

            //Act
            var result = source.LastIndexOf(x => x.Name == item.Name);

            //Assert
            result.Should().Be(5);
        }
    }
}