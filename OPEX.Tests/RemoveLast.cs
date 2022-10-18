namespace OPEX.Tests;

[TestClass]
public class RemoveLast
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
            var action = () => collection.RemoveLast(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambdaAndCollectionIsNull_Throw()
        {
            //Arrange
            Dummy[] collection = null!;
            var match = Fixture.Create<Func<Dummy, bool>>();

            //Act
            var action = () => collection.RemoveLast(match);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambdaAndLambdaIsNull_Throw()
        {
            //Arrange
            var collection = Fixture.Create<Dummy[]>();
            Func<Dummy, bool> match = null!;

            //Act
            var action = () => collection.RemoveLast(match);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void Always_Throw()
        {
            //Arrange
            var collection = Fixture.Create<Dummy[]>();
            var match = Fixture.Create<Func<Dummy, bool>>();

            //Act
            var action = () => collection.RemoveLast(match);

            //Assert
            action.Should().Throw<NotSupportedException>().WithMessage(string.Format(Exceptions.CannotUseMethodBecauseIsFixedSize, nameof(ToolBX.OPEX.CollectionExtensions.RemoveLast)));
        }

        [TestMethod]
        public void WhenUsingItemWithArray_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToArray();
            var item = Fixture.Create<Dummy>();

            //Act
            var action = () => collection.RemoveLast(item);

            //Assert
            action.Should().Throw<NotSupportedException>().WithMessage(string.Format(Exceptions.CannotUseMethodBecauseIsFixedSize, nameof(ToolBX.OPEX.CollectionExtensions.RemoveLast)));
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
            var action = () => collection.RemoveLast(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambdaAndCollectionIsNull_Throw()
        {
            //Arrange
            List<Dummy> collection = null!;
            var match = Fixture.Create<Func<Dummy, bool>>();

            //Act
            var action = () => collection.RemoveLast(match);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenUsingLambdaAndLambdaIsNull_Throw()
        {
            //Arrange
            var collection = Fixture.Create<List<Dummy>>();
            Func<Dummy, bool> match = null!;

            //Act
            var action = () => collection.RemoveLast(match);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsNullAndThereIsNoNullInCollection_Throw()
        {
            //Arrange
            var collection = Fixture.Create<List<Dummy>>();
            Dummy item = null!;

            //Act
            var action = () => collection.RemoveLast(item);

            //Assert
            action.Should().Throw<Exception>().WithMessage(Exceptions.NullCouldNotBeRemoved);
        }

        [TestMethod]
        public void WhenUsingItemAndItemNotFoundInCollection_Throw()
        {
            //Arrange
            var collection = Fixture.Create<List<Dummy>>();
            var item = Fixture.Create<Dummy>();

            //Act
            var action = () => collection.RemoveLast(item);

            //Assert
            action.Should().Throw<Exception>().WithMessage(string.Format(Exceptions.ItemCouldNotBeRemoved, item));
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemNotFoundInCollection_Throw()
        {
            //Arrange
            var collection = Fixture.Create<List<Dummy>>();

            //Act
            var action = () => collection.RemoveLast(x => x.Name == Fixture.Create<string>());

            //Assert
            action.Should().Throw<Exception>().WithMessage(Exceptions.PredicateItemCouldNotBeRemoved);
        }

        [TestMethod]
        public void WhenUsingItemAndThereIsOneOccurenceOfItem_RemoveItem()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToList();
            var original = collection.ToList();
            var item = collection[1];

            //Act
            collection.RemoveLast(item);

            //Assert
            collection.Should().BeEquivalentTo(new List<Dummy>
            {
                original[0], original[2]
            });
        }

        [TestMethod]
        public void WhenUsingLambdaAndThereIsOneOccurenceOfItem_RemoveItem()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>().ToList();
            var original = collection.ToList();
            var item = collection[1];

            //Act
            collection.RemoveLast(x => x.Name == item.Name);

            //Assert
            collection.Should().BeEquivalentTo(new List<Dummy>
            {
                original[0], original[2]
            });
        }

        [TestMethod]
        public void WhenUsingItemAndThereAreMultipleOccurencesOfItem_RemoveTheLastOne()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            var collection = new List<Dummy>
            {
                Fixture.Create<Dummy>(),
                item,
                item,
                Fixture.Create<Dummy>(),
                item
            };
            var original = collection.ToList();

            //Act
            collection.RemoveLast(item);

            //Assert
            collection.Should().BeEquivalentTo(new List<Dummy>
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
            var item = Fixture.Create<Dummy>();
            var collection = new List<Dummy>
            {
                Fixture.Create<Dummy>(),
                item,
                item,
                Fixture.Create<Dummy>(),
                item
            };
            var original = collection.ToList();

            //Act
            collection.RemoveLast(x => x.Name == item.Name);

            //Assert
            collection.Should().BeEquivalentTo(new List<Dummy>
            {
                original[0],
                item,
                item,
                original[3]
            });
        }
    }
}