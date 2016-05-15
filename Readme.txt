I once did a little game for a job interview so let it be example of my code here on Github.

The task was to develop a vertical scrolling shooter game with minimum required game elements:
- player's ship
- opponet's ship(s)
- bullet(s)

- The HUD should display the score and the remaining lifes
- The player's ship can be controlled via keyboard controls or via mouse
- It should be possible to pause the game
- Implement a game over dialog with possibility to play the game again
- Extra (not mandatory): Save and show scores
- Extra (not mandatory): Implement a scrolling star background

==========================

Here you have a Unity project(made with version 4.6.4f1 just in case) and a compiled version of the game for Windows with locked resolution to 486x864 (9:16), if it would be necessary in a real project to move to different resolution it would be easily possible through changing the size of GameFieldBoundary collider and a little adjustment to the camera, everything else would work from there, but it is simple test so I assumed locking it would work fine.
Pause can be made by pressing P on keyboard.
Player's ship can be controlled via keyboard wasd/arrows, mouse movement or gamepad, all of which work simultaneously.
After death player should press Space(or tap on mobile devices) to restart the game.

Things that presented in game:
1 player ship type
2 weapons that player can use, although second one(LigtningGun) is acquired for a short period of time through picking a power up
2 bullet types for 2 different guns, but they are not connected to the guns in general and either gun can use different bullets if needed
2 types of power ups: health and weapon
2 different enemies
3 layers of background images moving with different speed
all of the above is easily expandable to higher(or lower) numbers if needed.