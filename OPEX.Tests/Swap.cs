using ToolBX.Eloquentest.Extensions;

namespace OPEX.Tests;

[TestClass]
public class Swap
{
    [TestClass]
    public class WithArray : Tester
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            Dummy[] collection = null!;
            var current = Fixture.Create<int>();
            var destination = Fixture.Create<int>();

            //Act
            var action = () => collection.Swap(current, destination);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenCurrentIsNegative_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<int>().ToArray();
            var currentIndex = -Fixture.Create<int>();
            var destinationIndex = Fixture.CreateBetween(0, collection.Length);

            //Act
            var action = () => collection.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("currentIndex");
        }

        [TestMethod]
        public void WhenCurrentIsOutOfBounds_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<int>().ToArray();
            var currentIndex = Fixture.CreateGreaterThan(collection.LastIndex());
            var destinationIndex = Fixture.CreateBetween(0, collection.LastIndex());
                
            //Act
            var action = () => collection.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("currentIndex");
        }

        [TestMethod]
        public void WhenDestinationIsNegative_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<int>().ToArray();
            var currentIndex = Fixture.CreateBetween(0, collection.LastIndex());
            var destinationIndex = -Fixture.Create<int>();

            //Act
            var action = () => collection.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("destinationIndex");
        }

        [TestMethod]
        public void WhenDestinationIsOutOfBounds_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<int>().ToArray();
            var currentIndex = Fixture.CreateBetween(0, collection.LastIndex());
            var destinationIndex = Fixture.CreateGreaterThan(collection.LastIndex());

            //Act
            var action = () => collection.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("destinationIndex");
        }

        [TestMethod]
        public void WhenCurrentAndDestinationAreWithinBounds_SwapTogether()
        {
            //Arrange
            var collection = new[]
            {
                Fixture.Build<Dummy>().With(x => x.Name, "First").Create(),
                Fixture.Build<Dummy>().With(x => x.Name, "Second").Create(),
                Fixture.Build<Dummy>().With(x => x.Name, "Third").Create(),
                Fixture.Build<Dummy>().With(x => x.Name, "Fourth").Create(),
            };
            var original = collection.ToArray();

            var current = 1;
            var destination = 2;

            //Act
            collection.Swap(current, destination);

            //Assert
            collection.Should().ContainInOrder(new List<Dummy>
            {
                original[0],
                original[2],
                original[1],
                original[3],
            });
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
            var current = Fixture.Create<int>();
            var destination = Fixture.Create<int>();

            //Act
            var action = () => collection.Swap(current, destination);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenCurrentIsNegative_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<int>().ToList();
            var currentIndex = -Fixture.Create<int>();
            var destinationIndex = Fixture.CreateBetween(0, collection.LastIndex());

            //Act
            var action = () => collection.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("currentIndex");
        }

        [TestMethod]
        public void WhenCurrentIsOutOfBounds_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<int>().ToList();
            var currentIndex = Fixture.CreateGreaterThan(collection.LastIndex());
            var destinationIndex = Fixture.CreateBetween(0, collection.LastIndex());

            //Act
            var action = () => collection.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("currentIndex");
        }

        [TestMethod]
        public void WhenDestinationIsNegative_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<int>().ToList();
            var currentIndex = Fixture.CreateBetween(0, collection.LastIndex());
            var destinationIndex = -Fixture.Create<int>();

            //Act
            var action = () => collection.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("destinationIndex");
        }

        [TestMethod]
        public void WhenDestinationIsOutOfBounds_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<int>().ToList();
            var currentIndex = Fixture.CreateBetween(0, collection.LastIndex());
            var destinationIndex = Fixture.CreateGreaterThan(collection.LastIndex());

            //Act
            var action = () => collection.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("destinationIndex");
        }

        [TestMethod]
        public void WhenCurrentAndDestinationAreWithinBounds_SwapTogether()
        {
            //Arrange
            var collection = new List<Dummy>
            {
                Fixture.Build<Dummy>().With(x => x.Name, "First").Create(),
                Fixture.Build<Dummy>().With(x => x.Name, "Second").Create(),
                Fixture.Build<Dummy>().With(x => x.Name, "Third").Create(),
                Fixture.Build<Dummy>().With(x => x.Name, "Fourth").Create(),
            };
            var original = collection.ToArray();

            var current = 1;
            var destination = 2;

            //Act
            collection.Swap(current, destination);

            //Assert
            collection.Should().ContainInOrder(new List<Dummy>
            {
                original[0],
                original[2],
                original[1],
                original[3],
            });
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
            var current = Fixture.Create<int>();
            var destination = Fixture.Create<int>();

            //Act
            var action = () => collection.Swap(current, destination);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName("collection");
        }

        [TestMethod]
        public void WhenCurrentIsNegative_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<int>().ToWriteOnlyList();
            var currentIndex = -Fixture.Create<int>();
            var destinationIndex = Fixture.CreateBetween(0, collection.LastIndex());

            //Act
            var action = () => collection.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("currentIndex");
        }

        [TestMethod]
        public void WhenCurrentIsOutOfBounds_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<int>().ToWriteOnlyList();
            var currentIndex = Fixture.CreateGreaterThan(collection.LastIndex());
            var destinationIndex = Fixture.CreateBetween(0, collection.LastIndex());

            //Act
            var action = () => collection.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("currentIndex");
        }

        [TestMethod]
        public void WhenDestinationIsNegative_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<int>().ToWriteOnlyList();
            var currentIndex = Fixture.CreateBetween(0, collection.LastIndex());
            var destinationIndex = -Fixture.Create<int>();

            //Act
            var action = () => collection.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("destinationIndex");
        }

        [TestMethod]
        public void WhenDestinationIsOutOfBounds_Throw()
        {
            //Arrange
            var collection = Fixture.CreateMany<int>().ToWriteOnlyList();
            var currentIndex = Fixture.CreateBetween(0, collection.LastIndex());
            var destinationIndex = Fixture.CreateGreaterThan(collection.LastIndex());

            //Act
            var action = () => collection.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("destinationIndex");
        }

        [TestMethod]
        public void WhenCurrentAndDestinationAreWithinBounds_SwapTogether()
        {
            //Arrange
            var collection = new WriteOnlyList<Dummy>
            {
                Fixture.Build<Dummy>().With(x => x.Name, "First").Create(),
                Fixture.Build<Dummy>().With(x => x.Name, "Second").Create(),
                Fixture.Build<Dummy>().With(x => x.Name, "Third").Create(),
                Fixture.Build<Dummy>().With(x => x.Name, "Fourth").Create(),
            };
            var original = collection.ToArray();

            var current = 1;
            var destination = 2;

            //Act
            collection.Swap(current, destination);

            //Assert
            collection.Should().ContainInOrder(new List<Dummy>
            {
                original[0],
                original[2],
                original[1],
                original[3],
            });
        }
    }
}