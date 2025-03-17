using FibonacciGame.BusinessLogic;

namespace FibonacciGame.Tests.BusinessLogic
{
    public class FibonacciCheckerTests
    {
        [Fact]
        public void HasFibonacciSequence_ShouldReturnFalse_WhenGridIsEmpty()
        {
            // Arrange
            int[,] grid = new int[5, 5]; // The entire grid is 0
            List<(int, int)> cellsToClear = new();

            // Act
            bool result = FibonacciChecker.HasFibonacciSequence(grid, cellsToClear);

            // Assert
            Assert.False(result);
            Assert.Empty(cellsToClear);
        }

        [Fact]
        public void HasFibonacciSequence_ShouldReturnTrue_WhenValidSequenceExists_Horizontally()
        {
            // Arrange
            int[,] grid = new int[5, 5]
            {
                { 1, 1, 2, 3, 5 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 }
            };
            List<(int, int)> cellsToClear = new();

            // Act
            bool result = FibonacciChecker.HasFibonacciSequence(grid, cellsToClear);

            // Assert
            Assert.True(result);
            Assert.Equal(5, cellsToClear.Count);
        }

        [Fact]
        public void HasFibonacciSequence_ShouldReturnTrue_WhenValidSequenceExists_Vertically()
        {
            // Arrange
            int[,] grid = new int[5, 5]
            {
                { 1, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 0 },
                { 2, 0, 0, 0, 0 },
                { 3, 0, 0, 0, 0 },
                { 5, 0, 0, 0, 0 }
            };
            List<(int, int)> cellsToClear = new();

            // Act
            bool result = FibonacciChecker.HasFibonacciSequence(grid, cellsToClear);

            // Assert
            Assert.True(result);
            Assert.Equal(5, cellsToClear.Count);
        }

        [Fact]
        public void HasFibonacciSequence_ShouldReturnFalse_WhenSequenceIsNotFibonacci()
        {
            // Arrange
            int[,] grid = new int[5, 5]
            {
                { 1, 1, 2, 4, 5 }, // 2+4 != 5, so is not Fibonacci
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 }
            };
            List<(int, int)> cellsToClear = new();

            // Act
            bool result = FibonacciChecker.HasFibonacciSequence(grid, cellsToClear);

            // Assert
            Assert.False(result);
            Assert.Empty(cellsToClear);
        }
    }
}
