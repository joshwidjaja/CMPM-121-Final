# Devlog Entry - 20 November 2023
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
[F0.f] 
[F0.g] 

## Reflection
Looking back on how you achieved the F0 requirements, how has your team’s plan changed? Did you reconsider any of the choices you previously described for Tools and Materials or your Roles? It would be very suspicious if you didn’t need to change anything. There’s learning value in you documenting how your team’s thinking has changed over time.
