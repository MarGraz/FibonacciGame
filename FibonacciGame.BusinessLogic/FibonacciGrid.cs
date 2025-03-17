namespace FibonacciGame.BusinessLogic
{
    // The 50x50 grid. This class handle cell updates and grid state
    public class FibonacciGrid
    {
        private readonly int _gridSize;
        private readonly int[,] _grid;

        public FibonacciGrid(int gridSize)
        {
            _gridSize = gridSize;
            // New 50x50 empty grid
            _grid = new int[_gridSize, _gridSize];
        }


        // Retrieves the current grid state
        // And return a 2D array representing the grid
        public int[,] GetGrid() {
            return _grid;
        }

        // Increments the values in the entire row and column of the selected cell
        public void IncrementCell(int selectedRow, int selectedCol)
        {
            // Check to avoid to select a number out of the grid
            if (selectedRow < 0 || selectedRow >= _gridSize || selectedCol < 0 || selectedCol >= _gridSize) 
                return;

            // Increase values in the same row and column
            // If a cell is empty, it is set to 1, or it will be incremented
            for (int i = 0; i < _gridSize; i++)
            {
                if (i != selectedCol) // Exclude the clicked cell in the row loop to avoid to increment twice
                {
                    // Loop and modify all columns of that specific row
                    _grid[selectedRow, i] = _grid[selectedRow, i] == 0 ? 1 : _grid[selectedRow, i] + 1;
                }

                if (i != selectedRow) // Exclude the clicked cell in the column loop to avoid to increment twice
                {
                    // Loop and modify all rows of that specific column
                    _grid[i, selectedCol] = _grid[i, selectedCol] == 0 ? 1 : _grid[i, selectedCol] + 1;
                }
            }

             // Increment the clicked cell only once, doing this separately
            _grid[selectedRow, selectedCol] = _grid[selectedRow, selectedCol] == 0 ? 1 : _grid[selectedRow, selectedCol] + 1;        
        }

        // Resets all grid cells to zero        
        public void ResetGrid()
        {
            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    _grid[i, j] = 0;
                }
            }
        }

    }
}
