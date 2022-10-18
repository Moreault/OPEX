namespace OPEX.Tests;

[TestClass]
public class TryGetLast
{
    [TestClass]
    public class WithoutPredicate : Tester
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            IEnumerable<Dummy> source = null!;

            //Act
            var action = () => source.TryGetLast();

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenSourceIsEmpty_ReturnFailure()
        {
            //Arrange
            var source = Array.Empty<Dummy>();

            //Act
            var result = source.TryGetLast();

            //Assert
            result.Should().Be(TryGetResult<Dummy>.Failure);
        }

        [TestMethod]
        public void WhenSourceContainsExactlyOneItem_ReturnThatItem()
        {
            //Arrange
            var source = new List<Dummy> { Fixture.Create<Dummy>() };

            //Act
            var result = source.TryGetLast();

            //Assert
            result.Should().BeEquivalentTo(new TryGetResult<Dummy>(true, source.Single()));
        }

        [TestMethod]
        public void WhenSourceContainsExactlyOneItemButIsWeird_ReturnThatItem()
        {
            //Arrange
            var source = new WeirdCollection<Dummy> { Fixture.Create<Dummy>() };

            //Act
            var result = source.TryGetLast();

            //Assert
            result.Should().BeEquivalentTo(new TryGetResult<Dummy>(true, source.Single()));
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItems_ReturnTheLast()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToList();
            var last = Fixture.Create<Dummy>();
            source.Add(last);

            //Act
            var result = source.TryGetLast();

            //Assert
            result.Should().Be(new TryGetResult<Dummy>(true, last));
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItemsButDoesNotImplementIList_ReturnTheLast()
        {
            //Arrange
            var source = new WeirdCollection<Dummy>(Fixture.CreateMany<Dummy>());
            var last = Fixture.Create<Dummy>();
            source.Add(last);

            //Act
            var result = source.TryGetLast();

            //Assert
            result.Should().Be(new TryGetResult<Dummy>(true, last));
        }
    }

    [TestClass]
    public class WithPredicate : Tester
    {
        [TestMethod]
        public void WhenSourceIsNull_Throw()
        {
            //Arrange
            IEnumerable<Dummy> source = null!;

            //Act
            var action = () => source.TryGetLast(x => x.Name == "Something");

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(source));
        }

        [TestMethod]
        public void WhenPredicateIsNull_Throw()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToArray();
            Func<Dummy, bool> predicate = null!;

            //Act
            var action = () => source.TryGetLast(predicate);

            //Assert
            action.Should().Throw<ArgumentNullException>().WithParameterName(nameof(predicate));
        }

        [TestMethod]
        public void WhenSourceIsEmpty_ReturnFailure()
        {
            //Arrange
            var source = Array.Empty<Dummy>();

            //Act
            var result = source.TryGetLast(x => x.Name == "Something");

            //Assert
            result.Should().Be(TryGetResult<Dummy>.Failure);
        }

        [TestMethod]
        public void WhenSourceIsEmptyButIsWeird_ReturnFailure()
        {
            //Arrange
            var source = new WeirdCollection<Dummy>();

            //Act
            var result = source.TryGetLast(x => x.Name == "Something");

            //Assert
            result.Should().Be(TryGetResult<Dummy>.Failure);
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItemsButNoneCorrespond_ReturnFailure()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToList();

            //Act
            var result = source.TryGetLast(x => x.Name == "Something");

            //Assert
            result.Should().Be(TryGetResult<Dummy>.Failure);
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItemsButNoneCorrespondButIsWeird_ReturnFailure()
        {
            //Arrange
            var source = new WeirdCollection<Dummy>(Fixture.CreateMany<Dummy>());

            //Act
            var result = source.TryGetLast(x => x.Name == "Something");

            //Assert
            result.Should().Be(TryGetResult<Dummy>.Failure);
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItemsButOnlyOneCorresponds_ReturnThatOneItem()
        {
            //Arrange
            var source = Fixture.CreateMany<Dummy>().ToList();

            var first = Fixture.Build<Dummy>().With(x => x.Name, "Something").Create();
            source.Insert(1, first);

            //Act
            var result = source.TryGetLast(x => x.Name == "Something");

            //Assert
            result.Should().Be(new TryGetResult<Dummy>(true, first));
        }

        [TestMethod]
        public void WhenSourceContainsMultipleItemsButOnlyOneCorrespondsButIsWeird_ReturnThatOneItem()
        {
            //Arrange
            var source = new WeirdCollection<Dummy>(Fixture.CreateMany<Dummy>());

            var first = Fixture.Build<Dummy>().With(x => x.Name, "Something").Create();
            source.Insert(1, first);

            //Act
            var result = source.TryGetLast(x => x.Name == "Something");

            //Assert
            result.Should().Be(new TryGetResult<Dummy>(true, first));
        }

        [TestMethod]
        public void WhenAllItemsCorrespond_ReturnOnlyTheLast()
        {
            //Arrange
            var source = Fixture.Build<Dummy>().With(x => x.Name, "Something").CreateMany(3).ToArray();

            //Act
            var result = source.TryGetLast(x => x.Name == "Something");

            //Assert
            result.Should().Be(new TryGetResult<Dummy>(true, source[2]));
        }

        [TestMethod]
        public void WhenAllItemsCorrespondButIsWeird_ReturnOnlyTheLast()
        {
            //Arrange
            var source = new WeirdCollection<Dummy>(Fixture.Build<Dummy>().With(x => x.Name, "Something").CreateMany(3));

            //Act
            var result = source.TryGetLast(x => x.Name == "Something");

            //Assert
            result.Should().Be(new TryGetResult<Dummy>(true, source[2]));
        }
    }
}