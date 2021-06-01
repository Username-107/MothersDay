# Game of Life

A Unity based version of Conway's Game of Life.  
Includes both the classic 2D mode, and an experimental 3D mode.  

## Rules

Click on any cell to toggle it's state between dead and alive.  
Run the game by pressing the play button, or manually step through the game with the step button.  
The slider bar controls how quickly the game automatically proceeds.  
Play around with this sandbox, and let your imagination run wild!  

# 2D
Standard rules apply for the 2D game:
1. Any live cell with fewer than two live neighbours dies, as if caused by underpopulation.
2. Any live cell with two or three live neighbours lives on to the next generation.
3. Any live cell with more than three live neighbours dies, as if by overpopulation.
4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.

# 3D
To try to closely relate the 3D game to it's 2D sibling, the rules for the 2D game were broken down into fractions
based off the total number of neighbours that are possible for a cell.

The 3D game rules are as follows:
1. Any live cell with fewer than seven live neighbours dies, as if caused by underpopulation.
2. Any live cell with seven to twelve live neighbours lives on to the next generation.
3. Any live cell with more than twelve live neighbours dies, as if by overpopulation.
4. Any dead cell with exactly ten, eleven, or twelve live neighbours becomes a live cell, as if by reproduction.

## Controls

# Menu Bar
Slider Bar: Controls how quickly the game progresses  
Play/Pause Button: Plays and Pauses the automatic progress of the game  
Step Button: Manually progresses the game one generation  
Reset Button: Resets the current game  

# Mouse
Left Click: Toggle cell at pointer location  
Right Click and Drag: Pan Camera  
Alt + Right Click and Drag: Orbit around 3D game  
Mouse Wheel: Zoom In/Out  
