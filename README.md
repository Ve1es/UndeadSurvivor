# UndeadSurvivor

A lobby is formed with 2 players, each player selects a character, and the game begins. At the start of the game, a random weapon is assigned (not repeated among players) and the first wave of enemy spawns begins. During the wave, enemies and supplies spawn. Enemies must be present throughout the wave. After the wave ends, all enemies are destroyed. Supplies, on the other hand, appear at regular intervals. After all waves are completed or after all players die, a scoreboard displays the players with the amount of damage dealt and the number of enemies killed. If a player dies, they no longer continue the session but become a spectator.

## Implementation Features
- The project code is structured with SOLID principles in mind, especially the Single Responsibility Principle (SRP).

- A system is implemented to switch controls depending on the platform being used.

- The character movement system is implemented using the Host Client model.

- The project is structured to avoid the use of large controller classes.

- The Singleton anti-pattern is not used in the project.

- Create a convenient toolkit for configuring the main aspects of the game:

## A toolkit for configuring:

- Waves
- Weapon characteristics
- Enemy characteristics

## Project Requirements

- Use Photon Fusion as the networking solution (Host client): Photon Fusion is used in the project for network synchronization and interaction between clients.

- The location is created using Tile Map, providing flexibility and simplicity in level design.

- Create a mechanism for connecting multiple players to one game session.

- Create a character selection mechanic from four possible characters.

- Display a timer that shows the time until the end of the wave.

- Control via virtual joystick.
- Create 3 types of enemies.

- Create 3 types of weapons.

- Create 3 types of pickups.


## Use design patterns:
- Factory Method
- State
- Strategy
