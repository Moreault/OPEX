namespace OPEX.Tests;

[TestClass]
public class Swap
{
    [TestClass]
    public class WithArray : TestBase
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            Garbage[] source = null!;
            var current = Dummy.Create<int>();
            var destination = Dummy.Create<int>();

            //Act
            var action = () => source.Swap(current, destination);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenCurrentIsNegative_Throw()
        {
            //Arrange
            var source = Dummy.CreateMany<int>().ToArray();
            var currentIndex = -Dummy.Create<int>();
            var destinationIndex = Dummy.Number.Between(0, source.Length).Create();

            //Act
            var action = () => source.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("currentIndex");
        }

        [TestMethod]
        public void WhenCurrentIsOutOfBounds_Throw()
        {
            //Arrange
            var source = Dummy.CreateMany<int>().ToArray();
            var currentIndex = Dummy.Number.GreaterThan(source.LastIndex()).Create();
            var destinationIndex = Dummy.Number.Between(0, source.LastIndex()).Create();

            //Act
            var action = () => source.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("currentIndex");
        }

        [TestMethod]
        public void WhenDestinationIsNegative_Throw()
        {
            //Arrange
            var source = Dummy.CreateMany<int>().ToArray();
            var currentIndex = Dummy.Number.Between(0, source.LastIndex()).Create();
            var destinationIndex = -Dummy.Create<int>();

            //Act
            var action = () => source.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("destinationIndex");
        }

        [TestMethod]
        public void WhenDestinationIsOutOfBounds_Throw()
        {
            //Arrange
            var source = Dummy.CreateMany<int>().ToArray();
            var currentIndex = Dummy.Number.Between(0, source.LastIndex()).Create();
            var destinationIndex = Dummy.Number.GreaterThan(source.LastIndex()).Create();

            //Act
            var action = () => source.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("destinationIndex");
        }

        [TestMethod]
        public void WhenCurrentAndDestinationAreWithinBounds_SwapTogether()
        {
            //Arrange
            var source = new[]
            {
                Dummy.Build<Garbage>().With(x => x.Name, "First").Create(),
                Dummy.Build<Garbage>().With(x => x.Name, "Second").Create(),
                Dummy.Build<Garbage>().With(x => x.Name, "Third").Create(),
                Dummy.Build<Garbage>().With(x => x.Name, "Fourth").Create(),
            };
            var original = source.ToArray();

            var current = 1;
            var destination = 2;

            //Act
            source.Swap(current, destination);

            //Assert
            source.Should().ContainInOrder(new List<Garbage>
            {
                original[0],
                original[2],
                original[1],
                original[3],
            });
        }
    }

    [TestClass]
    public class WithList : TestBase
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            List<Garbage> source = null!;
            var current = Dummy.Create<int>();
            var destination = Dummy.Create<int>();

            //Act
            var action = () => source.Swap(current, destination);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenCurrentIsNegative_Throw()
        {
            //Arrange
            var source = Dummy.CreateMany<int>().ToList();
            var currentIndex = -Dummy.Create<int>();
            var destinationIndex = Dummy.Number.Between(0, source.LastIndex()).Create();

            //Act
            var action = () => source.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("currentIndex");
        }

        [TestMethod]
        public void WhenCurrentIsOutOfBounds_Throw()
        {
            //Arrange
            var source = Dummy.CreateMany<int>().ToList();
            var currentIndex = Dummy.Number.GreaterThan(source.LastIndex()).Create();
            var destinationIndex = Dummy.Number.Between(0, source.LastIndex()).Create();

            //Act
            var action = () => source.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("currentIndex");
        }

        [TestMethod]
        public void WhenDestinationIsNegative_Throw()
        {
            //Arrange
            var source = Dummy.CreateMany<int>().ToList();
            var currentIndex = Dummy.Number.Between(0, source.LastIndex()).Create();
            var destinationIndex = -Dummy.Create<int>();

            //Act
            var action = () => source.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("destinationIndex");
        }

        [TestMethod]
        public void WhenDestinationIsOutOfBounds_Throw()
        {
            //Arrange
            var source = Dummy.CreateMany<int>().ToList();
            var currentIndex = Dummy.Number.Between(0, source.LastIndex()).Create();
            var destinationIndex = Dummy.Number.GreaterThan(source.LastIndex()).Create();

            //Act
            var action = () => source.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("destinationIndex");
        }

        [TestMethod]
        public void WhenCurrentAndDestinationAreWithinBounds_SwapTogether()
        {
            //Arrange
            var source = new List<Garbage>
            {
                Dummy.Build<Garbage>().With(x => x.Name, "First").Create(),
                Dummy.Build<Garbage>().With(x => x.Name, "Second").Create(),
                Dummy.Build<Garbage>().With(x => x.Name, "Third").Create(),
                Dummy.Build<Garbage>().With(x => x.Name, "Fourth").Create(),
            };
            var original = source.ToArray();

            var current = 1;
            var destination = 2;

            //Act
            source.Swap(current, destination);

            //Assert
            source.Should().ContainInOrder(new List<Garbage>
            {
                original[0],
                original[2],
                original[1],
                original[3],
            });
        }
    }

    [TestClass]
    public class WithWriteOnlyList : TestBase
    {
        [TestMethod]
        public void WhenCollectionIsNull_Throw()
        {
            //Arrange
            WriteOnlyList<Garbage> source = null!;
            var current = Dummy.Create<int>();
            var destination = Dummy.Create<int>();

            //Act
            var action = () => source.Swap(current, destination);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenCurrentIsNegative_Throw()
        {
            //Arrange
            var source = Dummy.CreateMany<int>().ToWriteOnlyList();
            var currentIndex = -Dummy.Create<int>();
            var destinationIndex = Dummy.Number.Between(0, source.LastIndex()).Create();

            //Act
            var action = () => source.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("currentIndex");
        }

        [TestMethod]
        public void WhenCurrentIsOutOfBounds_Throw()
        {
            //Arrange
            var source = Dummy.CreateMany<int>().ToWriteOnlyList();
            var currentIndex = Dummy.Number.GreaterThan(source.LastIndex()).Create();
            var destinationIndex = Dummy.Number.Between(0, source.LastIndex()).Create();

            //Act
            var action = () => source.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("currentIndex");
        }

        [TestMethod]
        public void WhenDestinationIsNegative_Throw()
        {
            //Arrange
            var source = Dummy.CreateMany<int>().ToWriteOnlyList();
            var currentIndex = Dummy.Number.Between(0, source.LastIndex()).Create();
            var destinationIndex = -Dummy.Create<int>();

            //Act
            var action = () => source.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("destinationIndex");
        }

        [TestMethod]
        public void WhenDestinationIsOutOfBounds_Throw()
        {
            //Arrange
            var source = Dummy.CreateMany<int>().ToWriteOnlyList();
            var currentIndex = Dummy.Number.Between(0, source.LastIndex()).Create();
            var destinationIndex = Dummy.Number.GreaterThan(source.LastIndex()).Create();

            //Act
            var action = () => source.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("destinationIndex");
        }

        [TestMethod]
        public void WhenCurrentAndDestinationAreWithinBounds_SwapTogether()
        {
            //Arrange
            var source = new WriteOnlyList<Garbage>
            {
                Dummy.Build<Garbage>().With(x => x.Name, "First").Create(),
                Dummy.Build<Garbage>().With(x => x.Name, "Second").Create(),
                Dummy.Build<Garbage>().With(x => x.Name, "Third").Create(),
                Dummy.Build<Garbage>().With(x => x.Name, "Fourth").Create(),
            };
            var original = source.ToArray();

            var current = 1;
            var destination = 2;

            //Act
            source.Swap(current, destination);

            //Assert
            source.Should().ContainInOrder(new List<Garbage>
            {
                original[0],
                original[2],
                original[1],
                original[3],
            });
        }
    }
}