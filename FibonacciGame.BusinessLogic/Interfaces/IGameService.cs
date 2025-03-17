namespace FibonacciGame.BusinessLogic.Interfaces
{
    /// <summary>
    /// This service is used to manage the game grid and applies game rules
    /// </summary>
    public interface IGameService
    {
        /// <summary>
        /// Retrieves the current game grid
        /// </summary>
        /// <returns>A 2D array representing the grid state</returns>
        int[,] GetGrid();

        /// <summary>
        /// Handles the logic when a player clicks on a cell,
        /// It increments values and checks for Fibonacci sequences
        /// </summary>
        /// <param name="row">The row index of the clicked cell</param>
        /// <param name="col">The column index of the clicked cell</param>
        /// <returns>A list of cells to be resetted to zero</returns>
        List<(int, int)> ClickCell(int row, int col);

        /// <summary>
        /// Resets the game grid to the initial empty state
        /// </summary>
        void ResetGame();
    }
}
