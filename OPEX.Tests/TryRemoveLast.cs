namespace OPEX.Tests;

[TestClass]
public class TryRemoveLast
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
            var action = () => source.TryRemoveLast(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambdaAndCollectionIsNull_Throw()
        {
            //Arrange
            Garbage[] source = null!;
            var match = Dummy.Create<Func<Garbage, bool>>();

            //Act
            var action = () => source.TryRemoveLast(match);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambdaAndLambdaIsNull_Throw()
        {
            //Arrange
            var source = Dummy.Create<Garbage[]>();
            Func<Garbage, bool> match = null!;

            //Act
            var action = () => source.TryRemoveLast(match);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void Always_Throw()
        {
            //Arrange
            var source = Dummy.Create<Garbage[]>();
            var match = Dummy.Create<Func<Garbage, bool>>();

            //Act
            var action = () => source.TryRemoveLast(match);

            //Assert
            action.Should().Throw<NotSupportedException>().WithMessage(string.Format(Exceptions.CannotUseMethodBecauseIsFixedSize, nameof(ToolBX.OPEX.CollectionExtensions.TryRemoveLast)));
        }
    }

    [TestClass]
    public class WithList : Tester
    {
        [TestMethod]
        public void WhenUsingItemAndCollectionIsNull_Throw()
        {
            //Arrange
            List<Garbage> source = null!;
            var item = Dummy.Create<Garbage>();

            //Act
            var action = () => source.TryRemoveLast(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambdaAndCollectionIsNull_Throw()
        {
            //Arrange
            List<Garbage> source = null!;
            var match = Dummy.Create<Func<Garbage, bool>>();

            //Act
            var action = () => source.TryRemoveLast(match);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambdaAndLambdaIsNull_Throw()
        {
            //Arrange
            var source = Dummy.Create<List<Garbage>>();
            Func<Garbage, bool> match = null!;

            //Act
            var action = () => source.TryRemoveLast(match);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsNullAndThereIsNoNullInCollection_Throw()
        {
            //Arrange
            var source = Dummy.Create<List<Garbage>>();
            Garbage item = null!;

            //Act
            var action = () => source.TryRemoveLast(item);

            //Assert
            action.Should().NotThrow();
        }

        [TestMethod]
        public void WhenUsingItemAndItemNotFoundInCollection_Throw()
        {
            //Arrange
            var source = Dummy.Create<List<Garbage>>();
            var item = Dummy.Create<Garbage>();

            //Act
            var action = () => source.TryRemoveLast(item);

            //Assert
            action.Should().NotThrow();
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemNotFoundInCollection_Throw()
        {
            //Arrange
            var source = Dummy.Create<List<Garbage>>();

            //Act
            var action = () => source.TryRemoveLast(x => x.Name == Dummy.Create<string>());

            //Assert
            action.Should().NotThrow();
        }

        [TestMethod]
        public void WhenUsingItemAndThereIsOneOccurenceOfItem_RemoveItem()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToList();
            var original = source.ToList();
            var item = source[1];

            //Act
            source.TryRemoveLast(item);

            //Assert
            source.Should().BeEquivalentTo(new List<Garbage>
            {
                original[0], original[2]
            });
        }

        [TestMethod]
        public void WhenUsingLambdaAndThereIsOneOccurenceOfItem_RemoveItem()
        {
            //Arrange
            var source = Dummy.CreateMany<Garbage>().ToList();
            var original = source.ToList();
            var item = source[1];

            //Act
            source.TryRemoveLast(x => x.Name == item.Name);

            //Assert
            source.Should().BeEquivalentTo(new List<Garbage>
            {
                original[0], original[2]
            });
        }

        [TestMethod]
        public void WhenUsingItemAndThereAreMultipleOccurencesOfItem_RemoveTheLastOne()
        {
            //Arrange
            var item = Dummy.Create<Garbage>();
            var source = new List<Garbage>
            {
                Dummy.Create<Garbage>(),
                item,
                item,
                Dummy.Create<Garbage>(),
                item
            };
            var original = source.ToList();

            //Act
            source.TryRemoveLast(item);

            //Assert
            source.Should().BeEquivalentTo(new List<Garbage>
            {
                original[0],
                item,
                item,
                original[3]
            });
        }

        [TestMethod]
        public void WhenUsingLambdaAndThereAreMultipleOccurencesOfItem_RemoveTheLastOne()
        {
            //Arrange
            var item = Dummy.Create<Garbage>();
            var source = new List<Garbage>
            {
                Dummy.Create<Garbage>(),
                item,
                item,
                Dummy.Create<Garbage>(),
                item
            };
            var original = source.ToList();

            //Act
            source.TryRemoveLast(x => x.Name == item.Name);

            //Assert
            source.Should().BeEquivalentTo(new List<Garbage>
            {
                original[0],
                item,
                item,
                original[3]
            });
        }
    }
}