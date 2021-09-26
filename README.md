# MarsRoverTechChallenge 

A squad of robotic rovers are to be landed by NASA on a plateau on Mars.

This plateau, which is curiously rectangular, must be navigated by the rovers so that their on board cameras can get a complete view of the surrounding terrain to send back to Earth.

A rover's position is represented by a combination of an x and y co-ordinates and a letter representing one of the four cardinal compass points. The plateau is divided up into a grid to simplify navigation. An example position might be 0, 0, N, which means the rover is in the bottom left corner and facing North.

In order to control a rover, NASA sends a simple string of letters. The possible letters are 'L', 'R' and 'M'. 'L' and 'R' makes the rover spin 90 degrees left or right respectively, without moving from its current spot.

'M' means move forward one grid point, and maintain the same heading.

Assume that the square directly North from (x, y) is (x, y+1).

Input:

The first line of input is the upper-right coordinates of the plateau, the lower-left coordinates are assumed to be 0,0.

The rest of the input is information pertaining to the rovers that have been deployed. Each rover has two lines of input. The first line gives the rover's position, and the second line is a series of instructions telling the rover how to explore the plateau.

The position is made up of two integers and a letter separated by spaces, corresponding to the x and y co-ordinates and the rover's orientation.

Each rover will be finished sequentially, which means that the second rover won't start to move until the first one has finished moving.

Rules
* Commands are sent from earth via satellites
* All rovers constantly report to the command center which is a satellite in orbit of Mars that tracks thier position and the state of the grid.
![Communication](https://user-images.githubusercontent.com/79660088/134823709-b18da344-b545-4968-958a-16582113e4ab.PNG)

* Rover can not move out of the plateau, if Rover reachs the edge of the plateau and the next command tells the rover to move out the rover will stop and ignore the rest of the commands.
* Two rovers can not be in the same grid at the same time, if a rover is commanded to go into a grid where another rover is currently, the rover will stop and ignore the rest of the commands
* The rover will take instrumental readings in each grid, and once it has completed all the given commands it will relay all that information back to the Command Center, example
360 degree Photo, Anemometer readings, Barometer readings, ...
* Once all rovers have completed the commands, the command center will send the information back to earth
![Sat](https://user-images.githubusercontent.com/79660088/134824075-5d98fe6a-1fbd-4817-824a-e43cc49be111.PNG)

Installation
Step 1: 
