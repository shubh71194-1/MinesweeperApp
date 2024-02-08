#Minesweeper App

# Summary
Minesweeper code implements the classic Minesweeper game logic with separate components for the game model, view, controller, and service layers. Here's a summary:

MinesweeperGame: Manages the game state, including initializing the minefield, revealing cells, checking for game over or victory, and generating the minefield display.

MinesweeperService: Acts as a bridge between the game model and the controller, providing methods to initialize the game, reveal cells, and check game status.

GameController: Orchestrates the game flow, interacting with the view and service layers. It prompts the user to input the grid size and number of mines, starts a new game, reveals cells, and displays the minefield.

ConsoleView: Implements the view interface, providing methods to display messages and receive user input via the console.

Constants: Contains constants used throughout the game, such as mine values, grid size limits, and cell input length limits.

Main method: The entry point of the application, which continuously runs the game loop, allowing the player to start new games indefinitely.

Overall, the code structure follows the MVC (Model-View-Controller) pattern, facilitating modularity and separation of concerns. The tests ensure the correctness of the game logic and user interaction.