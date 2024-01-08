# Gorilla-Vehicles
Spawn in (Custom) vehicles made from other players into your game and drive them around!

# Usage
NOTE: YOU MUST BE IN A MODDED LOBBY FOR THE MOD TO BE ENABLED<br />
Note 2: To start driving spawned cars point your and at it and press Right Controller A and same to stop driving it (Also your turning disables when driving)<br />
Note 3: While in the vehicle you should be centered IRL for your player to be in the correct position in the car
First download the dll and put it in Gorilla Tag Folder/BepInEx/Plugins then you can start your game and successfully use the mod

How to download other .vehicle files first join the <a href = "https://discord.gg/YNkaEKfQBw">discord<a> then go to # Upload Vehicles then download the vehicle file then go to your dll location and find the folder called "Custom Vehicles" and put it in there boom now you have a new vehicle to play with!

# For Creators
You can download the template project here: https://drive.google.com/drive/u/0/folders/1lYe67IK2ogx3TNicxq7vDeAnWxoJWpCg<br />

How to Set Up Custom Vehicle:<br />

Begin by opening your project and obtaining your car model, preferably from Blender. Ensure that in Blender, you apply rotations to all the wheels before exporting them. To do this, select each wheel, go to Object > Apply > Rotations.<br />

Import the model into the scene and configure it by naming all the wheels correctly. Avoid placing wheel colliders directly on the wheels themselves, as it can lead to issues. Place them on empty gameobjects.<br />

Create your wheel colliders, which is a straightforward process. If you're unfamiliar with it, you can find tutorials online. Be sure to use the correct names to prevent errors.<br />

Attach a Rigidbody component to the parent of the model and set its mass to around 350 for optimal results.<br />

Add a "Descriptor" component to the object, filling in the values (starter values work well).<br />

Add a empty gameobject called "DrivePoint" then put it down below the car like a good amount if you want it to be in the middle you have to put it below the car around the size of the car<br />

Add a mesh collider to the base of your car and check the "Convex" option but don't press the "Is Trigger" option.<br />

Add a cube to the model, naming it "SelectPoint" and adjusting its size to match the cars size. This cube serves as the selection box for choosing the car in-game once it's spawned.<br />

To export the object in the hierarchy, select "Assets/Build Vehicle" and wait for the process to complete. Your vehicle file can be found in Assets/Asset Bundles.<br />

Notes:<br />
You can use the Template Model as a reference for the setup.<br />
Names are crucial; ensure you change them to the correct ones.<br />
Parenting doesn't matter; you can organize objects in different parents. Just keep the entire car parented at the position (0,0,0). The system will find any object in any parent as long as the names are correct.<br />
Properly align the wheel colliders and set their ranges to the correct size for optimal vehicle performance.<br />

Correct vehicle tire names <br />
 Mesh Names: <br />
Front left = FWheelLM<br />
Front right = FWheelRM<br />
Back left = BWheelLM<br />
Back right = BWheelRM<br />
 Wheel Collider names<br />
Front left = FWheelL<br />
Front right = FWheelR<br />
Back left = BWheelL<br />
Back right = BWheelR

# Base Cars
Fast Car (Red Muscle Car) <br />
Police Car

# Mod Requirements
Computer++ 1.0.1
Utilla 1.5.0

# Controls
Open Pad = Left Controller X<br />
Select Vehicle = Right Controller A<br />
Delete Selected Vehicle = Right Controller B + Right Controller Index<br />
Drive = Right Controller Idex<br />
Reverse = Left Controller Index<br />
Break = Right Controller B<br />
Steer = Right Stick<br />
Get Out Of Car = Right Stick CLICK

 MENU CONTROLS<br />
Spawn Vehicles = Right Controller Index<br />
Rotate Left/Right = Right Controller A<br />
Rotate Up/Down = Right Controller B<br />
more controls will be added so make sure to keep checking in on the repo

# Soon To Be Added
Engine Sounds

# Credits
Fast Car: https://assetstore.unity.com/packages/3d/vehicles/land/arcade-free-racing-car-161085

Other Cars : https://assetstore.unity.com/packages/3d/vehicles/land/simple-cars-pack-97669

Holo Shader : https://www.void1gaming.com/free-hologram-shader-for-unity-urp

# Info
Gorilla Vehicles Discord: https://discord.gg/YNkaEKfQBw<br />
![GitHub all releases](https://img.shields.io/github/downloads/Blas1ed/Gorilla-Vehicles/total?color=%2300FF00)



