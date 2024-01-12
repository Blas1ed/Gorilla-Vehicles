# Gorilla-Vehicles
Spawn in (Custom) vehicles made from other players into your game and drive them around!

# Usage
NOTE: YOU MUST BE IN A MODDED LOBBY FOR THE MOD TO BE ENABLED<br />
Note 2: To start driving spawned cars point your and at it and press Right Controller A and same to stop driving it (Also your turning disables when driving)<br />
Note 3: While in the vehicle you should be centered IRL for your player to be in the correct position in the car
First download the dll and put it in Gorilla Tag Folder/BepInEx/Plugins then you can start your game and successfully use the mod

How to download other .vehicle files first join the <a href = "https://discord.gg/YNkaEKfQBw">discord<a> then go to # Upload Vehicles then download the vehicle file then go to your dll location and find the folder called "Custom Vehicles" and put it in there boom now you have a new vehicle to play with!

# For Creators (Updated New GUI)
You can download the template project here: https://drive.google.com/drive/u/0/folders/1lYe67IK2ogx3TNicxq7vDeAnWxoJWpCg<br />

NOTE: Make sure you import XR Plugin Management into the project and set Plugin Provider to OpenXR then go to OpenXR Tab below it after the Restart and then set Render Mode to Multi-Pass this is to fix your car not rendering in both eyes<br />

How to Set Up Custom Vehicle:<br />

Begin by opening your project and obtaining your car model, preferably from Blender. Ensure that in Blender, you apply rotations to all the wheels before exporting them. To do this, select each wheel, go to Object > Apply > Rotations.<br />

Import the model into the scene and configure it by naming all the wheels correctly. Avoid placing wheel colliders directly on the wheels themselves, as it can lead to issues. Place them on empty gameobjects.<br />

Add a "Descriptor" component to the object, filling in the values (starter values work well).<br />

Create your wheel colliders, which is a straightforward process. If you're unfamiliar with it, you can find tutorials online. then set them correctly in the Descriptor<br />

Attach a Rigidbody component to the parent of the model and set its mass to around 350 for optimal results.<br />

Add a empty gameobject this is where your player will be seated (note: put it far below the car to make your player centered) then set the DrivePoint as this object in the Descriptor<br />

Add a mesh collider to the base of your car and check the "Convex" option but don't press the "Is Trigger" option.<br />

Add a cube to the model, this is the selection point for pointing at the car after making it set the SelectPoint to this in the Descriptor<br />

To export the object in the hierarchy, go to GorillaVehicles/Car Exporter click it then select the Export on the vehicle you want to export<br />

Notes:<br />
You can use the Template Models as a references for the setup.<br />
Parenting doesn't matter; you can organize objects in different parents. Just keep the entire car parented at the position (0,0,0).<br />
Properly align the wheel colliders and set their ranges to the correct size for optimal vehicle performance.<br />

# Base Cars
Fast Car (Red Muscle Car) <br />
Police Car
Motorcycle

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

Motorcycle: https://sketchfab.com/3d-models/motorcycle-38404e2077ca4b209cd2f1db30541b94

Other Cars : https://assetstore.unity.com/packages/3d/vehicles/land/simple-cars-pack-97669

Holo Shader : https://www.void1gaming.com/free-hologram-shader-for-unity-urp

# Info
Gorilla Vehicles Discord: https://discord.gg/YNkaEKfQBw<br />
![GitHub all releases](https://img.shields.io/github/downloads/Blas1ed/Gorilla-Vehicles/total?color=%2300FF00)<br />
300 Downloads thnks ↑
