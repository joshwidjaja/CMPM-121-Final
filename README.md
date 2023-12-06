# Devlog Entry F0 - 20 November 2023
## Introducing the Team
Tools Lead: Joshua Widjaja

Engine Lead: Chase Houske

Design Lead: Nicholas Puckdee

Pallavi Rajeev

Reese Garcia

## Tools and Materials
1.) For this final project, our team chose the Unity Game Engine because of its user-friendly design and the convenience of using the C# programming language. Unity's accessibility for developers of all levels is complemented by the powerful scripting language C#, which enhances the overall ease and efficiency of the development process. This combination allows us to efficiently bring our creative ideas to life and ensures a smooth development process.

2.) Our team chose C# for this project because the Unity Game Engine is built to work with the C# language. Using C# for our project with Unity makes things easier and ensures that the programming language and game engine work well together.

3.) We will use Visual Studio Code as our IDE for this project. We chose to use VS Code because all team members are familiar with this platform. We have also all pretty much used it in our other classes. Additionally, Visual Studio Code has useful features like intellisense, that can make our coding process and game development more efficient. 

## Outlook
This will be the first time using Unity and C# for some group members. Although we'll still use the engine for its above benefits, we'll still need to dedicate some extra time to teach them and get them acclimated to the environment. By the end of this project, we are hoping to earn further practice and familiarize ourselves with
the toolset provided within Unity. This will allow us to develop a set of skills in which can be applied to
future Unity based projects in the future.

## How we satisfied the software requirements
[F0.a] You control a character moving on a 2D grid.
Our game’s implementation allows you to control a character moving on a 2D grid using the keyboard inputs. The code detects which key is being pressed down by firing a keydown event and storing that input. Therefore, when the player presses down on the arrow keys, they are able to move correspondingly. The up arrow moves the character up, down arrow moves the character down, right arrow moves the character to the right, and left arrow moves the character to the left. 

[F0.b] You advance time in the turn-based simulation manually.
Each time the player moves around on the game space, they can do one of two actions: planting or harvesting crops, using the ‘z’ and ‘x’ keys respectively. When a player does one of these actions, an event will be triggered, advancing the player to their next turn. After this, the game will update the sun and water levels of the board, allowing for play to continue.

[F0.c] You can reap (gather) or sow (plant) plants on the grid when your character is near them.
When the player’s coordinates are close to a plant, they are able to reap or sow the plants. In order to reap or sow the plant, the player can use ‘x’ to reap or ‘z’ to sow. Once again our code triggers a keydown event when the player presses down on either of these keys. When either of these keys are pressed and the player decides to reap or sow the plant they are by, the plant is destroyed (if the player chooses to harvest it) or a new material is created using an array of coordinates to select a type of plant (if the player chooses to plant a plant). 

[F0.d] Grid cells have sun and water levels. The incoming sun and water for each cell is somehow randomly generated each turn. Sun energy cannot be stored in a cell (it is used immediately or lost) while water moisture can be slowly accumulated over several turns.
When the player triggers an event to advance their turn, the game will reset the sun level and spawn sun and water for the next turn. This is accomplished by calling a series of functions to reset a turn. At the end of a turn, the first function will reset the sun level to 0. Then two more functions are called to randomly generate numbers, determining the sun level and water level for the next turn.

[F0.e] 
Each plant on the grid has a type (e.g. one of 3 species) and a growth level (e.g. “level 1”, “level 2”, “level 3”).
Each crop cell has a growth level being an int 0-2 representing level 1-3. Each crop cell also has a species, a string, which is tomato, corn, or melon. The cube is accordingly red, yellow, or green based on the species.

[F0.f] 
Simple spatial rules govern plant growth based on sun, water, and nearby plants (growth is unlocked by satisfying conditions).
If the sun and water level exceeds a certain value, the crop's growth level will increase. It can increase multiple levels each turn if the water has scaled to a high value with previous turns having bad sun rolls, making the crop unable to grow even with high water values. The crop growing increases the crop's x, y, and z scales.

[F0.g] 
A play scenario is completed when some condition is satisfied (e.g. at least X plants at growth level Y or above).
Based on the crop level being harvested, that crop level number is added to a total points. Since crops you plant start at level 0, if you harvest an "unleveled" one you get 0 points. If the points exceeds 10, you get a message in the console saying "You Win!".

## Reflection
Looking back on how you achieved the F0 requirements, how has your team’s plan changed? Did you reconsider any of the choices you previously described for Tools and Materials or your Roles? It would be very suspicious if you didn’t need to change anything. There’s learning value in you documenting how your team’s thinking has changed over time.

Initially we were thinking of having copies of the crops in the scene view. However, since an announcement was made not wanting us to use the scene view at all, we realized we had to make some changes in our plan. So instead of using the scene view, we decided to make the crops colored cubes for now. This allows us to easily create the crops in code without having to apply some mesh to them. The setup of having a crop manager and a crop cell class and their roles have also changed over time. Originally there was no crop cell class.

# Devlog Entry F1 - 05 December 2023
[F0.a]
[F0.b] 
[F0.c]
[F0.d]
[F0.e]
[F0.f]
[F0.g]

[F1.a] The important state of each cell of your game’s grid must be backed by a single contiguous byte array in AoS or SoA format. Your team must statically allocate memory usage for the whole grid.

[F1.b] The player must be able to undo every major choice (all the way back to the start of play), even from a saved game. They should be able to redo (undo of undo operations) multiple times.

[F1.c] The player must be able to manually save their progress in the game in a way that allows them to load that save and continue play another day. The player must be able to manage multiple save files (allowing save scumming).

[F1.d] The game must implement an implicit auto-save system to support recovery from unexpected quits. (For example, when the game is launched, if an auto-save entry is present, the game might ask the player "do you want to continue where you left off?" The auto-save entry might or might not be visible among the list of manual save entries available for the player to load as part of F1.c.)

## Reflection
Looking back on how you achieved the new F1 requirements, how has your team’s plan changed? Did you reconsider any of the choices you previously described for Tools and Materials or your Roles? Has your game design evolved now that you've started to think about giving the player more feedback? It would be very suspicious if you didn’t need to change anything. There’s learning value in you documenting how your team’s thinking has changed over time.
