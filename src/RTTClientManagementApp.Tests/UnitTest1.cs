using Xunit;
using Xunit;

namespace RTTClientManagementApp.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestAddition()
        {
            // Arrange
            int a = 5;
            int b = 3;
            int expected = 8;

            // Act
            int result = Add(a, b);

            // Assert
            Assert.Equal(expected, result);
        }

        private int Add(int x, int y)
        {
            return x + y;
        }
    }
}