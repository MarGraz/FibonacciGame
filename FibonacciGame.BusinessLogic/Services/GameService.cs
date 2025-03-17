using Microsoft.Extensions.Options;
using FibonacciGame.BusinessLogic.Interfaces;
using FibonacciGame.Common.Configurations;
using System;

namespace FibonacciGame.BusinessLogic.Services
{
    // It implements the game logic, including grid updates and Fibonacci sequence detection
    public class GameService : IGameService
    {

        private readonly FibonacciGrid _grid;

        public GameService(IOptions<GridConfig> gridConfig)
        {
            _grid = new FibonacciGrid(gridConfig.Value.GridSize);
        }

        
        // Retrieves the current grid state
        // And return a 2D array representing the grid
        public int[,] GetGrid()
        {
            return _grid.GetGrid();
        }
        
        // Based on the row and col user click, increments values in the row and column,
        // and checks if a Fibonacci sequence is present        
        public List<(int, int)> ClickCell(int row, int col)
        {
            _grid.IncrementCell(row, col);

            // Check if there is a Fibonacci sequence in the grid
            List<(int, int)> cellsToClear = new();

            if (FibonacciChecker.HasFibonacciSequence(_grid.GetGrid(), cellsToClear))
            {
                // Returns the cellsToClear finded in the HasFibonacciSequence
                return cellsToClear;
            }

            return new List<(int, int)>();
        }

        // Clear all the values
        public void ResetGame()
        {
            _grid.ResetGrid();
        }
    }
}
