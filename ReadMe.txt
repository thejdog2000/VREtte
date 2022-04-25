Before Opening Unity:
	1. Download the gazepointer software or similar webcam eye tracking software: https://gazerecorder.com/gazepointer/
	2. Open gazepointer and calibrate it. Follow the instructions in the software as the appear.
	3. Once calibration is complete move onto your Unity project or executable you wish to test.


For Scene 1:
	Scene one exemplifies eye tracking uses in game through using your eye tracking system to manuever an obstacle course
	and interact with a few NPCs.
	The controls are WASD for movement, PL;' for camera controls, and a free floating mouse that is manipulated by gazepointer
	to handle all look interactions. E is used for a door in the beginning, and Shift is used in conjuction with the eye tracking
	to activate obstacle blocks. NPCs involve staring at them for a certain period of time.


For Scene 2:
	Scene 2 is simply a test case to be compared to FoveatedA1 for the sake of comparison. It has simple WASD movement and mouse
	controls or gamepad abilities. It also has a free floating mouse controlled by gazepointer that can open a door, but other than that it is simply used for 
	a statistical comparison.


For FoveatedA1:
	This scene has the same controls as the previous, making a comparison apt. Both also contains a built in asset that handles 
	calculating and displaying processing usage and framerate to make comparison for the scene, allowing testing of efficiency.
	In this scene there are two cameras, both displaying to render textures, one of which is overlayed over another. The top one will
	move and slide around over the top, being a higher quality, and focused on where you are looking, causing no easily noticable 
	lost in visual quality. The scripting also makes sure to keep the overlay in bounds, making sure to limit its updates for the 
	sake of performance, and morphs the top to display the correct portion of the camera to sync with the bottom image.

For StareTest.exe:
	This software is used to test the accuracy of your eye tracking software. Once it is calibrated, open this software. When there
	is nothing displayed spacebar can be pressed to spawn in an icon. Staring at that icon with eye tracking software on will allow
	the executable to know how many pixels off your eye tracking is. Each one will wait 1 second, then track for 5 seconds and create
	an average pixel count off. After running in the 5 locations it will no longer work. Swap over to its console window using Alt-tab
	and take note of the pixel distance calculations displayed. Convert these values to inches, and measure the distance from your
	screen to you in the position you have calibrated for. Use the degree variant of arctan(pixel_to_inches/distance_to_face) to find
	the estimation in degrees of how far off your eye tracking is. Multiple tests are recommended.
