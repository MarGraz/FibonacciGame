using FibonacciGame.BusinessLogic;

namespace FibonacciGame.Tests.BusinessLogic
{
    public class FibonacciGridTests
    {
        [Fact]
        public void Constructor_ShouldInitializeGridWithZeros()
        {
            // Arrange
            int gridSize = 5;
            FibonacciGrid grid = new(gridSize);

            // Act
            int[,] result = grid.GetGrid();

            // Assert
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    Assert.Equal(0, result[i, j]);
                }
            }
        }

        [Fact]
        public void IncrementCell_ShouldIncreaseRowAndColumnValues()
        {
            // Arrange
            int gridSize = 5;
            FibonacciGrid grid = new(gridSize);
            int row = 2, col = 2;

            // Act
            grid.IncrementCell(row, col);
            int[,] result = grid.GetGrid();

            // Assert
            for (int i = 0; i < gridSize; i++)
            {
                if (i == row || i == col)
                {
                    Assert.True(result[row, i] >= 1); // Check row update
                    Assert.True(result[i, col] >= 1); // Check column update
                }
            }

            // Check the clicked cell is incremented once
            Assert.Equal(1, result[row, col]);
        }

        [Fact]
        public void IncrementCell_ShouldNotAffectOtherCells()
        {
            // Arrange
            int gridSize = 5;
            FibonacciGrid grid = new(gridSize);
            int row = 2, col = 2;

            // Act
            grid.IncrementCell(row, col);
            int[,] result = grid.GetGrid();

            // Assert
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (i != row && j != col)
                    {
                        Assert.Equal(0, result[i, j]); // Unaffected cells should remain 0
                    }
                }
            }
        }

        [Fact]
        public void ResetGrid_ShouldResetAllCellsToZero()
        {
            // Arrange
            int gridSize = 5;
            FibonacciGrid grid = new(gridSize);
            grid.IncrementCell(2, 2); // Make some modifications

            // Act
            grid.ResetGrid();
            int[,] result = grid.GetGrid();

            // Assert
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    Assert.Equal(0, result[i, j]); // Everything should be reset to 0
                }
            }
        }

        [Fact]
        public void IncrementCell_ShouldIgnoreOutOfBoundsIndices()
        {
            // Arrange
            int gridSize = 5;
            FibonacciGrid grid = new(gridSize);

            // Act
            grid.IncrementCell(-1, 2); // Negative row
            grid.IncrementCell(2, -1); // Negative col
            grid.IncrementCell(5, 2);  // Out of bounds row
            grid.IncrementCell(2, 5);  // Out of bounds col

            // Assert
            int[,] result = grid.GetGrid();
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    Assert.Equal(0, result[i, j]); // Should remain unchanged
                }
            }
        }
    }
}
