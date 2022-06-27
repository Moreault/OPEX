namespace OPEX.Tests;

[TestClass]
public class GetRandomIndex
{
    [TestClass]
    public class WithArray : Tester
    {
        [TestMethod]
        public void Always_ReturnRandomIndexWithinBoundaries()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>(15).ToArray();

            //Act
            var results = new List<int>();
            for (var i = 0; i < 100; i++)
                results.Add(collection.GetRandomIndex());

            //Assert
            results.Should().OnlyContain(x => x >= 0 && x <= 14);
        }
    }

    [TestClass]
    public class WithList : Tester
    {
        [TestMethod]
        public void Always_ReturnRandomIndexWithinBoundaries()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>(15).ToList();

            //Act
            var results = new List<int>();
            for (var i = 0; i < 100; i++)
                results.Add(collection.GetRandomIndex());

            //Assert
            results.Should().OnlyContain(x => x >= 0 && x <= 14);
        }
    }

    [TestClass]
    public class WithWriteOnlyList : Tester
    {
        [TestMethod]
        public void Always_ReturnRandomIndexWithinBoundaries()
        {
            //Arrange
            var collection = Fixture.CreateMany<Dummy>(15).ToWriteOnlyList();

            //Act
            var results = new List<int>();
            for (var i = 0; i < 100; i++)
                results.Add(collection.GetRandomIndex());

            //Assert
            results.Should().OnlyContain(x => x >= 0 && x <= 14);
        }
    }

    [TestClass]
    public class WithReadOnlyList : Tester
    {
        [TestMethod]
        public void Always_ReturnRandomIndexWithinBoundaries()
        {
            //Arrange
            IReadOnlyList<Dummy> collection = Fixture.CreateMany<Dummy>(15).ToList();

            //Act
            var results = new List<int>();
            for (var i = 0; i < 100; i++)
                results.Add(collection.GetRandomIndex());

            //Assert
            results.Should().OnlyContain(x => x >= 0 && x <= 14);
        }
    }
}