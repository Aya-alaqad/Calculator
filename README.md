# Calculator

A desktop calculator built with **C# and Windows Forms (.NET Framework)**. The project separates the calculator's core logic from its user interface to make the code easier to maintain and extend.

## Features

- Perform arithmetic operations: addition (`+`), subtraction (`-`), multiplication (`*`), and division (`/`).
- Support percentage calculations (`%`).
- Evaluate mathematical expressions using operator precedence and parentheses.
- Support negative numbers.
- Support implicit multiplication, such as `5(9)` and `(2+3)(4+1)`.
- Accept input through the calculator buttons and keyboard.
- Delete the last character using Backspace.
- Evaluate expressions using Enter.

## Technologies Used

- **Language:** C#
- **Framework:** .NET Framework
- **User Interface:** Windows Forms
- **IDE:** Visual Studio
- **Version Control:** Git and GitHub

## Getting Started

### Prerequisites

- Windows
- Visual Studio with the .NET desktop development workload
- The .NET Framework version targeted by the project

### Installation

1. Clone the repository:

   ```bash
   git clone <repository-url>
   ```

2. Open the solution (`.sln`) in Visual Studio. If the repository does not contain a solution file, open the `.csproj` file instead.

3. Restore any required dependencies, if prompted.

4. Build the project using **Build → Build Solution**.

5. Run the application using **F5** or **Start Without Debugging** (`Ctrl+F5`).

## Usage

1. Enter an expression using the on-screen buttons or your keyboard.
2. Use parentheses to group operations.
3. Press Enter or the equals button to calculate the result.
4. Use Backspace or the delete button to remove the last character.

Example expressions:

| Expression | Result |
|---|---:|
| `2+3*4` | `14` |
| `(2+3)*4` | `20` |
| `5(9)` | `45` |
| `-5+8` | `3` |

## Project Structure

The project separates the core calculation logic from the Windows Forms interface.

- **Core logic:** Handles expression processing and calculation.
- **Windows Forms UI:** Handles user interaction, keyboard input, and result display.
- **Project configuration (`.csproj`):** Defines the project settings, target framework, and source files.
  
