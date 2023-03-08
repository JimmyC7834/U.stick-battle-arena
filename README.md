# U.stick-battle-arena
### Game Description
Stick Battle Arena is a multiplayer combat party game with multiple gamemodes. Players will fight and battle each other to victory. The game will include several themed stages, and even more unique weapons such as bows, guns, spears, swords, grenades, machine guns, rocket launchers, flame throwers and more. Learning good movement and how to line up shots will be a core element of the gameplay.
### Our goal
Our goal is to ship a finished game with refined details and fun gameplay by the end of the quarter. Also, to gain group project and teamwork experience along the development process.
### Install and running the software
1. Simple download the compressed file from the game release page:
  - [itch.io](https://jimmyc.itch.io/cse403-stick-battle-arena-alpha-release)
2. Extract the content of the compressed file into the desired location. You will need `7zip` to uncompress the file.
3. To run the software, simply double click the `stick-battle-arena.exe` executable 
* Note that on a mac machine the executable could be marked as malicious and cannot be started. Please change your machine settings to override the malicious warning
### User manual
- Simply click on buttons to navigate between menus (press `f11` to switch between fullscreen and windowed mode)
- To control your character, use the key specified (as we are supporting maximum of 4 player, the key mapping could be clamped):
  - Player1: `WASD` for movement, `1` for shooting/use item, `2` for switching between inventory items
  - Player2: `Arrow Keys` for movement, `Right shift` for shooting/use item, `Right ctrl` for switching between inventory items
  - Player3: `YGHJ` for movement, `V` for shooting/use item, `B` for switching between inventory items
  - Player4: `PL:"` for movement, `,` fo shooting, `.` for switching between inventory items
- Game mechanism:
- There are two game mode for gameplay:
  - Battle royal mode: each player gets 5 lifes and the last one standing on the stage wins.
  - Target score mode: The first player to get 120 scores wins.
- Player Outfit
  - player can customize their character with differnt color tint and accessories provided.
- Combat mechanisms:
  - player can perform wall jumps by jumping against a wall
  - Items/Weapons will spawn randomly on stage for players to pick up.
  - Each player has two inventory slots such that item can be switched for more combat option.
  - Each weapon comes with a unique behaviour (Knockback, recoil, shooting rate, explosion, etc).
  - Each weapon has its own durability and can only be used for certain number of times before it breaks.
  - Player can choose between 8 different maps each with a unique theme and set up for the gameplay.
  - There are stage elements such as moving platforms, jump pads, warp zone, and kill zone set up for more dynamic gameplay.
### Contributor Guide
Please read the [developer guide](https://github.com/JimmyC7834/U.stick-battle-arena/blob/main/Docs/Developer%20Guide.md) for details and complete guide.
### Bug Reporting
Bugs are reported to `JimmyC7834/U.stick-battle-arena` repo under the issues tab. Please include the following details in your issue submission:
- The action performed/trigger of the bug
- the behaviour of the bug
- the version number of the build
DO NOT submit a PR directly. Bug reports should be submitted as issues.
### Known Bugs
- Player could get stuck on item objects when inventory is full as the player object cannot pick them up.
- Players can possiblely pickup the same item object if they collided with the object at the same time.
### Team Members
- JimmyC7834: Jimmy
- Teru5244: Phoenix
- Fettuchoony: Todd
- dylannalcock: Dylan
- rhyswe: Ryse
- irontigeroutreach,tommydgithub: Tommy
