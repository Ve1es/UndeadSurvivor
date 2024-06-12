# UndeadSurvivor

A lobby is formed with 2 players, each player selects a character, and the game begins. At the start of the game, a random weapon is assigned (not repeated among players) and the first wave of enemy spawns begins. During the wave, enemies and supplies spawn. Enemies must be present throughout the wave. After the wave ends, all enemies are destroyed. Supplies, on the other hand, appear at regular intervals. After all waves are completed or after all players die, a scoreboard displays the players with the amount of damage dealt and the number of enemies killed. If a player dies, they no longer continue the session but become a spectator.

## Implementation Features
Use SOLID principles, particularly the SRP principle:
The project code is structured with SOLID principles in mind, especially the Single Responsibility Principle (SRP).

Create a system for changing controls based on the platform:
A system is implemented to switch controls depending on the platform being used.

Movement implemented through Host Client:
The character movement system is implemented using the Host Client model.

Avoid using large controller classes:
The project is structured to avoid the use of large controller classes.

Avoid using the Singleton anti-pattern:
The Singleton anti-pattern is not used in the project.

Create a convenient toolkit for configuring the main aspects of the game:
A toolkit for configuring:
Waves
Weapon characteristics
Enemy characteristics

## Project Requirements
Load the asset and implement the project mechanics:
All necessary assets are loaded and implemented in the project.

Use Photon Fusion as the networking solution (Host client):
Photon Fusion is used in the project for network synchronization and interaction between clients.

Create a location where enemies and bonuses spawn in waves:
A location is developed using Tile Map where enemies and bonuses spawn in waves.

Use Tile Map for creating the location:
The location is created using Tile Map, providing flexibility and simplicity in level design.

Create 3 types of enemies:
Regular Zombie: slow with weak attack.
Enhanced Zombie: slow with increased health and damage, but low attack frequency.
Skeleton: attacks from a distance and is fast.

Create 3 types of weapons:
Pistol: short attack range, high damage.
Shotgun: medium range, low damage, spreads shot.
Automatic Rifle: medium damage, long attack range.

Create 3 types of pickups:
Medkit: restores health.
Bomb: destroys enemies within a certain radius.
Ammo crates.

Create 3 waves:
First Wave:
Break time: 10 seconds
Duration: 1 minute
Enemies: regular zombies
Bonuses: ammo

Second Wave:
Break time: 30 seconds
Duration: 3 minutes
Enemies: regular zombies, skeletons
Bonuses: ammo, medkits

Third Wave:
Break time: 30 seconds
Duration: 5 minutes
Enemies: regular zombies, skeletons, enhanced zombies
Bonuses: ammo, medkits, bombs

Create a mechanism for connecting multiple players to one game session:
A mechanism for connecting two players to one game session is implemented using Photon Fusion.

Create a character selection mechanic from four possible characters:
An interface for selecting one of four characters is implemented in the project.

Display a timer that shows the time until the end of the wave:
A timer is displayed on the screen showing the time until the end of the current wave.

Control via virtual joystick:
Character control is implemented through a virtual joystick, ensuring convenience for playing on mobile devices.


Use design patterns:
Factory Method
State
Strategy
