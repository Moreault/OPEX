using ToolBX.OPEX.Utilities;

namespace OPEX.Tests.Utilities;

[TestClass]
public class EnumUtilsTester
{
    public enum DummyEnum
    {
        One,
        Two,
        Three
    }

    [TestClass]
    public class ToList : TestBase
    {
        [TestMethod]
        public void Always_ReturnListWithAllValuesOfTheEnum()
        {
            //Arrange
            var expected = new List<DummyEnum>
            {
                DummyEnum.One,
                DummyEnum.Two,
                DummyEnum.Three
            };

            //Act
            var result = EnumUtils.ToList<DummyEnum>();

            //Assert
            result.Should().BeEquivalentTo(expected);
        }
    }

}