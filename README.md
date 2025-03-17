# Fibonacci Grid Game - Documentation

## Scope of this document
This document serves as a reference for the **Fibonacci Grid Game**, providing an overview of its functionality and details about its implementation.

---

## 🎮 How to Play

### Game Mechanics
- The game consists of a **50x50 grid**
- Clicking on a cell **increases all cells in the same row and column by +1**
- If a cell was empty, it will be set to **1**
- Cells are **highlighted in yellow** when updated

### Winning Condition
- If **five consecutive Fibonacci numbers** appear in a row or column, those cells **are cleared**
- The cleared cells briefly **highlight in green** before resetting
- You can play as long as you want, **there is no losing condition**

### Controls
- **Click** on any cell to **increase the numbers**
- **Press** the **Reset Grid** button to restart the game

---

## 🔮 Future Improvements

1. **Now the Fibonacci sequences are recognized** when they appear in a **row** (**from left to right**) and in a **column** (**from top to bottom**). A possible future improvement could be adding a check in the reverse direction as well, from right to left;
2. Another improvement could be **adding a score**, that is the summa of the detected Fibonacci's cells.

---

## ⚙️ Internal Functionality

This section explains the internal flow of the game, from user interaction to backend processing.

### **1. Game Initialization**
- When the user visits the `FibonacciGrid.razor` page, the `OnInitialized()` event is triggered;
- Inside `OnInitialized()`, a new grid instance is created, initializing all cells to **zero** by calling `GetGrid()` on `GameService`, which is injected via Dependency Injection.

---

### **2. User Clicks a Cell and cells are incremented**
When a user clicks on a cell, the following sequence of events is triggered:

1. The **Blazor UI** captures the click event in `FibonacciGrid.razor`, calling `ClickCellAsync(row, col)`;

2. This method calls **GameService**'s `ClickCell(row, col)`, passing the **row and column index** of the clicked cell;

3. The **GameService**:
   - Calls `IncrementCell(row, col)` on the **FibonacciGrid** instance;
   - This method updates all **cells in the same row and column**;
   - To avoid **double-incrementing** the clicked cell, it is incremented **separately**, outside the loop;
   - The execution then returns to `GameService`.

---

### **3. Checking for Fibonacci Sequences**
Now that the grid has been updated, **GameService** invokes `HasFibonacciSequence()` to check for any valid Fibonacci sequences:

1. `HasFibonacciSequence(grid, cellsToClear)` is called. It takes the **updated grid**, and an **empty list** `cellsToClear`, which will store the positions of any detected sequences;

2. It loops through:
   - **Each row**, calling `CheckSequence()`;
   - **Each column**, also calling `CheckSequence()`.

3. The **CheckSequence() method** processes the grid:
   - It checks if each cell contains a **Fibonacci number**, using `_FibonacciNumbers.Contains(cellValue)`;
   - If the number is valid, it is **stored in a temporary list** `sequence`, and its coordinates are saved in `currentSequencePosition`;
   - If the list has **at least 5 numbers**, it verifies if they form a **valid Fibonacci sequence** using `IsConsecutiveFibonacci(sequence)`;
     - If valid, the **last 5 numbers** are taken (to remove the initial extra number 1 detected as valid), and their positions are added to `cellsToClear`;
   - The loop continues scanning until all rows and columns are checked.

---

### **4. Returning the Result**
1. **CheckSequence()** finishes processing and returns to `HasFibonacciSequence()`;

2. If a valid sequence is found:
   - The method returns `true` along with `cellsToClear`, containing the positions to reset;
   - Otherwise, it returns `false` with an empty list.

3. **GameService handles the result:**
   - If no sequences were found, it **returns an empty list** to the UI;
   - If a sequence was detected, it **returns the `cellsToClear` list** to the UI.

---

### **5. Updating the UI and highlight the deleted cells**
Back in `FibonacciGrid.razor`, the frontend processes the result:

1. If `cellsToClear` contains elements:
   - The **corresponding grid cells are reset to 0**;
   - A **green animation effect** is briefly displayed before the reset.

2. The UI updates using **StateHasChanged()** to reflect the cleared numbers.
---

## 🖥️ UML Diagrams

### **Sequence Diagram**
The following sequence diagram illustrates the flow from user interaction to grid updates and Fibonacci validation:

![Sequence Diagram Fibonacci](https://github.com/user-attachments/assets/b8f8c897-9d35-4807-a462-688b8da71ad7)

---

### **Class Diagram**
This class diagram represents the **core architecture** of the game, including `GameService`, `FibonacciGrid`, and `FibonacciChecker`:

![Class Diagram Fibonacci](https://github.com/user-attachments/assets/ac0a9112-1b04-4095-98c1-b76ac44da784)

---

## 📝 Class Overview

### **GameService.cs**
- Acts as a bridge between the UI and the game logic;
- Calls `IncrementCell()` and `HasFibonacciSequence()`;
- Exposes `ClickCell()` and `ResetGame()` methods.

### **FibonacciGrid.cs**
- Manages the 50x50 grid;
- Updates row and column values on a cell click;
- Resets the grid when needed.

### **FibonacciChecker.cs**
- Detects Fibonacci sequences in the grid;
- Check the Fibonacci sequence, to see if it's valid and consecutive;
- Generate the Fibonacci numbers and add them in an HashSet for fast Fibonacci number checks.
