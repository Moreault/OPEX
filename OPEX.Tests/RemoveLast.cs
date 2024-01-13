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
            Dummy[] source = null!;
            var item = Fixture.Create<Dummy>();

            //Act
            var action = () => source.RemoveLast(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambdaAndCollectionIsNull_Throw()
        {
            //Arrange
            Dummy[] source = null!;
            var match = Fixture.Create<Func<Dummy, bool>>();

            //Act
            var action = () => source.RemoveLast(match);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambdaAndLambdaIsNull_Throw()
        {
            //Arrange
            var source = Fixture.Create<Dummy[]>();
            Func<Dummy, bool> match = null!;

            //Act
            var action = () => source.RemoveLast(match);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void Always_Throw()
        {
            //Arrange
            var source = Fixture.Create<Dummy[]>();
            var match = Fixture.Create<Func<Dummy, bool>>();

            //Act
            var action = () => source.RemoveLast(match);

            //Assert
            action.Should().Throw<NotSupportedException>().WithMessage(string.Format(Exceptions.CannotUseMethodBecauseIsFixedSize, nameof(ToolBX.OPEX.CollectionExtensions.RemoveLast)));
        }

        [TestMethod]
        public void WhenUsingItemWithArray_Throw()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToArray();
            var item = Fixture.Create<Dummy>();

            //Act
            var action = () => source.RemoveLast(item);

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
            List<Dummy> source = null!;
            var item = Fixture.Create<Dummy>();

            //Act
            var action = () => source.RemoveLast(item);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambdaAndCollectionIsNull_Throw()
        {
            //Arrange
            List<Dummy> source = null!;
            var match = Fixture.Create<Func<Dummy, bool>>();

            //Act
            var action = () => source.RemoveLast(match);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenUsingLambdaAndLambdaIsNull_Throw()
        {
            //Arrange
            var source = Fixture.Create<List<Dummy>>();
            Func<Dummy, bool> match = null!;

            //Act
            var action = () => source.RemoveLast(match);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("match");
        }

        [TestMethod]
        public void WhenUsingItemAndItemIsNullAndThereIsNoNullInCollection_Throw()
        {
            //Arrange
            var source = Fixture.Create<List<Dummy>>();
            Dummy item = null!;

            //Act
            var action = () => source.RemoveLast(item);

            //Assert
            action.Should().Throw<Exception>().WithMessage(Exceptions.NullCouldNotBeRemoved);
        }

        [TestMethod]
        public void WhenUsingItemAndItemNotFoundInCollection_Throw()
        {
            //Arrange
            var source = Fixture.Create<List<Dummy>>();
            var item = Fixture.Create<Dummy>();

            //Act
            var action = () => source.RemoveLast(item);

            //Assert
            action.Should().Throw<Exception>().WithMessage(string.Format(Exceptions.ItemCouldNotBeRemoved, item));
        }

        [TestMethod]
        public void WhenUsingLambdaAndItemNotFoundInCollection_Throw()
        {
            //Arrange
            var source = Fixture.Create<List<Dummy>>();

            //Act
            var action = () => source.RemoveLast(x => x.Name == Fixture.Create<string>());

            //Assert
            action.Should().Throw<Exception>().WithMessage(Exceptions.PredicateItemCouldNotBeRemoved);
        }

        [TestMethod]
        public void WhenUsingItemAndThereIsOneOccurenceOfItem_RemoveItem()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToList();
            var original = source.ToList();
            var item = source[1];

            //Act
            source.RemoveLast(item);

            //Assert
            source.Should().BeEquivalentTo(new List<Dummy>
            {
                original[0], original[2]
            });
        }

        [TestMethod]
        public void WhenUsingLambdaAndThereIsOneOccurenceOfItem_RemoveItem()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToList();
            var original = source.ToList();
            var item = source[1];

            //Act
            source.RemoveLast(x => x.Name == item.Name);

            //Assert
            source.Should().BeEquivalentTo(new List<Dummy>
            {
                original[0], original[2]
            });
        }

        [TestMethod]
        public void WhenUsingItemAndThereAreMultipleOccurencesOfItem_RemoveTheLastOne()
        {
            //Arrange
            var item = Fixture.Create<Dummy>();
            var source = new List<Dummy>
            {
                Fixture.Create<Dummy>(),
                item,
                item,
                Fixture.Create<Dummy>(),
                item
            };
            var original = source.ToList();

            //Act
            source.RemoveLast(item);

            //Assert
            source.Should().BeEquivalentTo(new List<Dummy>
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
            var source = new List<Dummy>
            {
                Fixture.Create<Dummy>(),
                item,
                item,
                Fixture.Create<Dummy>(),
                item
            };
            var original = source.ToList();

            //Act
            source.RemoveLast(x => x.Name == item.Name);

            //Assert
            source.Should().BeEquivalentTo(new List<Dummy>
            {
                original[0],
                item,
                item,
                original[3]
            });
        }
    }
}