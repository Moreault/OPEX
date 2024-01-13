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
            Dummy[] source = null!;
            var current = Fixture.Create<int>();
            var destination = Fixture.Create<int>();

            //Act
            var action = () => source.Swap(current, destination);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenCurrentIsNegative_Throw()
        {
            //Arrange
            var source = Fixture.CreateMany<int>().ToArray();
            var currentIndex = -Fixture.Create<int>();
            var destinationIndex = Fixture.CreateBetween(0, source.Length);

            //Act
            var action = () => source.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("currentIndex");
        }

        [TestMethod]
        public void WhenCurrentIsOutOfBounds_Throw()
        {
            //Arrange
            var source = Fixture.CreateMany<int>().ToArray();
            var currentIndex = Fixture.CreateGreaterThan(source.LastIndex());
            var destinationIndex = Fixture.CreateBetween(0, source.LastIndex());
                
            //Act
            var action = () => source.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("currentIndex");
        }

        [TestMethod]
        public void WhenDestinationIsNegative_Throw()
        {
            //Arrange
            var source = Fixture.CreateMany<int>().ToArray();
            var currentIndex = Fixture.CreateBetween(0, source.LastIndex());
            var destinationIndex = -Fixture.Create<int>();

            //Act
            var action = () => source.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("destinationIndex");
        }

        [TestMethod]
        public void WhenDestinationIsOutOfBounds_Throw()
        {
            //Arrange
            var source = Fixture.CreateMany<int>().ToArray();
            var currentIndex = Fixture.CreateBetween(0, source.LastIndex());
            var destinationIndex = Fixture.CreateGreaterThan(source.LastIndex());

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
                Fixture.Build<Dummy>().With(x => x.Name, "First").Create(),
                Fixture.Build<Dummy>().With(x => x.Name, "Second").Create(),
                Fixture.Build<Dummy>().With(x => x.Name, "Third").Create(),
                Fixture.Build<Dummy>().With(x => x.Name, "Fourth").Create(),
            };
            var original = source.ToArray();

            var current = 1;
            var destination = 2;

            //Act
            source.Swap(current, destination);

            //Assert
            source.Should().ContainInOrder(new List<Dummy>
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
            List<Dummy> source = null!;
            var current = Fixture.Create<int>();
            var destination = Fixture.Create<int>();

            //Act
            var action = () => source.Swap(current, destination);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenCurrentIsNegative_Throw()
        {
            //Arrange
            var source = Fixture.CreateMany<int>().ToList();
            var currentIndex = -Fixture.Create<int>();
            var destinationIndex = Fixture.CreateBetween(0, source.LastIndex());

            //Act
            var action = () => source.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("currentIndex");
        }

        [TestMethod]
        public void WhenCurrentIsOutOfBounds_Throw()
        {
            //Arrange
            var source = Fixture.CreateMany<int>().ToList();
            var currentIndex = Fixture.CreateGreaterThan(source.LastIndex());
            var destinationIndex = Fixture.CreateBetween(0, source.LastIndex());

            //Act
            var action = () => source.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("currentIndex");
        }

        [TestMethod]
        public void WhenDestinationIsNegative_Throw()
        {
            //Arrange
            var source = Fixture.CreateMany<int>().ToList();
            var currentIndex = Fixture.CreateBetween(0, source.LastIndex());
            var destinationIndex = -Fixture.Create<int>();

            //Act
            var action = () => source.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("destinationIndex");
        }

        [TestMethod]
        public void WhenDestinationIsOutOfBounds_Throw()
        {
            //Arrange
            var source = Fixture.CreateMany<int>().ToList();
            var currentIndex = Fixture.CreateBetween(0, source.LastIndex());
            var destinationIndex = Fixture.CreateGreaterThan(source.LastIndex());

            //Act
            var action = () => source.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("destinationIndex");
        }

        [TestMethod]
        public void WhenCurrentAndDestinationAreWithinBounds_SwapTogether()
        {
            //Arrange
            var source = new List<Dummy>
            {
                Fixture.Build<Dummy>().With(x => x.Name, "First").Create(),
                Fixture.Build<Dummy>().With(x => x.Name, "Second").Create(),
                Fixture.Build<Dummy>().With(x => x.Name, "Third").Create(),
                Fixture.Build<Dummy>().With(x => x.Name, "Fourth").Create(),
            };
            var original = source.ToArray();

            var current = 1;
            var destination = 2;

            //Act
            source.Swap(current, destination);

            //Assert
            source.Should().ContainInOrder(new List<Dummy>
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
            WriteOnlyList<Dummy> source = null!;
            var current = Fixture.Create<int>();
            var destination = Fixture.Create<int>();

            //Act
            var action = () => source.Swap(current, destination);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenCurrentIsNegative_Throw()
        {
            //Arrange
            var source = Fixture.CreateMany<int>().ToWriteOnlyList();
            var currentIndex = -Fixture.Create<int>();
            var destinationIndex = Fixture.CreateBetween(0, source.LastIndex());

            //Act
            var action = () => source.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("currentIndex");
        }

        [TestMethod]
        public void WhenCurrentIsOutOfBounds_Throw()
        {
            //Arrange
            var source = Fixture.CreateMany<int>().ToWriteOnlyList();
            var currentIndex = Fixture.CreateGreaterThan(source.LastIndex());
            var destinationIndex = Fixture.CreateBetween(0, source.LastIndex());

            //Act
            var action = () => source.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("currentIndex");
        }

        [TestMethod]
        public void WhenDestinationIsNegative_Throw()
        {
            //Arrange
            var source = Fixture.CreateMany<int>().ToWriteOnlyList();
            var currentIndex = Fixture.CreateBetween(0, source.LastIndex());
            var destinationIndex = -Fixture.Create<int>();

            //Act
            var action = () => source.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("destinationIndex");
        }

        [TestMethod]
        public void WhenDestinationIsOutOfBounds_Throw()
        {
            //Arrange
            var source = Fixture.CreateMany<int>().ToWriteOnlyList();
            var currentIndex = Fixture.CreateBetween(0, source.LastIndex());
            var destinationIndex = Fixture.CreateGreaterThan(source.LastIndex());

            //Act
            var action = () => source.Swap(currentIndex, destinationIndex);

            //Assert
            action.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("destinationIndex");
        }

        [TestMethod]
        public void WhenCurrentAndDestinationAreWithinBounds_SwapTogether()
        {
            //Arrange
            var source = new WriteOnlyList<Dummy>
            {
                Fixture.Build<Dummy>().With(x => x.Name, "First").Create(),
                Fixture.Build<Dummy>().With(x => x.Name, "Second").Create(),
                Fixture.Build<Dummy>().With(x => x.Name, "Third").Create(),
                Fixture.Build<Dummy>().With(x => x.Name, "Fourth").Create(),
            };
            var original = source.ToArray();

            var current = 1;
            var destination = 2;

            //Act
            source.Swap(current, destination);

            //Assert
            source.Should().ContainInOrder(new List<Dummy>
            {
                original[0],
                original[2],
                original[1],
                original[3],
            });
        }
    }
}