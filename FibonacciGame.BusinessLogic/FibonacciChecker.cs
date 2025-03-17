namespace FibonacciGame.BusinessLogic
{
    // Static because it contains only a class with utility methods, to do some calculations
    public static class FibonacciChecker
    {
        // There's no limit to the user click, so potentially we need an infinite Fibonacci, but I set it to 5000 that is a reasonable limit
        private static readonly HashSet<int> _fibonacciNumbers = GenerateFibonacciNumbers(5000);

        /// <summary>
        /// Checks if there is a sequence of five consecutive Fibonacci numbers, both horizontally or vertically in the grid
        /// </summary>
        /// <param name="grid">The game grid</param>
        /// <param name="cellsToClear">List of cells that need to be cleared</param>
        /// <returns>True if a Fibonacci sequence is found, otherwise false</returns>
        public static bool HasFibonacciSequence(int[,] grid, List<(int, int)> cellsToClear)
        {
            int gridSize = grid.GetLength(0);

            // Check every single row for Fibonacci sequences
            for (int r = 0; r < gridSize; r++)
                CheckSequence(grid, r, 0, 0, 1, cellsToClear);

            // Check every single column for Fibonacci sequences
            for (int c = 0; c < gridSize; c++)
                CheckSequence(grid, 0, c, 1, 0, cellsToClear);

            return cellsToClear.Count > 0;
        }

        #region Private methods
      
        // Checks a row OR column for a sequence of Fibonacci numbers        
        private static void CheckSequence(int[,] grid, int row, int col, int rowNextStep, int colNextStep, List<(int, int)> cellsToClear)
        {
            // Temporary storage for potential Fibonacci sequences
            List<(int, int)> currentSequencePosition = new();
            List<int> sequence = new();

            // Cycle all the rows or all the columns (it depends from the parameters passed)
            while (row < grid.GetLength(0) && col < grid.GetLength(1))
            {
                // Get the value stored in the current cell
                int cellValue = grid[row, col];

                // If it's a Fibonacci number, we add id to our sequence
                if (_fibonacciNumbers.Contains(cellValue))
                {
                    sequence.Add(cellValue);
                    currentSequencePosition.Add((row, col)); // Sotre also the position

                    // If we have at least 5 numbers, check if they form a valid consecutive Fibonacci sequence
                    if (sequence.Count >= 5 && IsConsecutiveFibonacci(sequence))
                    {
                        // It takes only the last five numbers of the valid sequence, ignoring the numbers before, like: 1,1,1,>1,1,2,3,5<
                        var validStartIndex = sequence.Count - 5;
                        var validSequence = sequence.Skip(validStartIndex).ToList();
                        var validPositions = currentSequencePosition.Skip(validStartIndex).ToList(); // Update also the positions

                        // Add to the cells to clear only the valid positions
                        cellsToClear.AddRange(validPositions);

                        // Reset the temporary lists to go ahead with the while and look for more sequences composed by 5 numbers
                        currentSequencePosition.Clear();
                        sequence.Clear();
                    }
                }
                else
                {
                    // If it's not a Fibonacci number, clear the sequence and positions
                    currentSequencePosition.Clear();
                    sequence.Clear();
                }

                 row += rowNextStep;  // Moves down for a column check, otherwise remains the same for a row check, behaviour set in CheckSequence()
                 col += colNextStep;  // Moves right for a row check, otherwise remains the same for a column check, behaviour set in CheckSequence()
            }
        }

        // Checks if a sequence of numbers follows the Fibonacci pattern        
        private static bool IsConsecutiveFibonacci(List<int> sequence)
        {
            // Do the check only if the sequence contains at least 5 numbers
            if (sequence.Count < 5)
            {
                return false;
            }

            // Cycle the sequence list, and check all possible subsequences of 5 consecutive numbers(group by 5)
            for (int start = 0; start <= sequence.Count - 5; start++)
            {
                bool isValid = true;

                // Loop through the first three pairs of the subsequence and verify if it follows Fibonacci logic: 1+1=2, 1+2=3, 2+3=5
                for (int i = 0; i < 3; i++)
                {
                    // Fibonacci logic: the sum of two numbers must give as result the third one
                    if (sequence[start + i] + sequence[start + i + 1] != sequence[start + i + 2])
                    {
                        isValid = false;
                        break;
                    }
                }

                if (isValid)
                {
                    return true; // If we have at least a sequence of 5 valid, return true
                }
            }

            return false;
        }

        //Generates a list of Fibonacci numbers up to a given maximum value
        private static HashSet<int> GenerateFibonacciNumbers(int maxValue)
        {
            // I use the hashset in the CheckSequence() because it's faster compared to a List. Ít doen't store duplicated 1, but is not a problem, because the HashSet contains a number 1, this is sufficient for the check, we add it to the variable "sequence"
            HashSet<int> fibonacciList = new() { 1, 1 };
            int previousNumber = 1;
            int currentNumber = 1;

            while (currentNumber <= maxValue)
            {
                int fibonacciNumber = previousNumber + currentNumber;

                if (fibonacciNumber > maxValue)
                {
                    break; // Stop the while when max is reached
                }
                else
                {
                    // Store the Fibonacci number in the list
                    fibonacciList.Add(fibonacciNumber);

                    // The previous number, is now the Current number
                    previousNumber = currentNumber;
                    // And the current number is now the fibonacciNumber
                    currentNumber = fibonacciNumber;
                }
            }

            // Convert to HashSet to keep lookup speed fast
            return fibonacciList;
        }

        #endregion
    }
}
