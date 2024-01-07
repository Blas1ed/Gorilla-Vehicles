# Gorilla-Vehicles
Spawn in (Custom) vehicles made from other players into your game and drive them around!

# Usage
First download the dll and put it in Gorilla Tag Folder/BepInEx/Plugins then you can start your game and successfully use the mod

How to download other .vehicle files first join the discord then go to # Upload Vehicles then download the vehicle file then go to your dll location and find the folder called "Gorilla Vehicles" and put it in there boom now you have a new vehicle to play with!

# For Creators
You can download the template project here:

How To:
First your gonna wanna open the project up then get your car model (Prefereably from blender) (also make sure in blender before export click on your wheels and click Object/Apply/Rotations on all wheels) then import the model into your game setup the model by first getting all the wheels and setting them to the correct names and DO NOT PUT WHEEL COLLIDERS ON THE WHEELS THEMSELF or it will break then make your wheel colliders which is easy and if you dont know just look it up its very simple just make sure to set the correct names or you will get errors after all of that you want to add a rigidbody to the models parent and set the mass to around 350 for good results then your gonna wanna add a "Descriptor" component to the object also and fill in the values (Note: Starter values are good also) then make your model a prefab and once done right click on the model then click "Export Asset Bundle" then wait for it to finish and DONE you can find you vehicle file in Assets/Asset Bundles!

Also forgot to add you also need to add a Cube in the model and call it SelectPoint make it the size of the object and a little bigger this is the select box for selecting the car ingame after its spawned!

Correct vehicle tire names
 Mesh Names:
Front left = FWheelLM
Front right = FWheelRM
Back left = BWheelLM
Back right = BWheelRM
 Wheel Collider names
Front left = FWheelL
Front right = FWheelR
Back left = BWheelL
Back right = BWheelR

# Base Cars
Fast Car (Red Muscle Car)
and some others

# Model Credits
Fast Car: https://assetstore.unity.com/packages/3d/vehicles/land/arcade-free-racing-car-161085

Other Cars : https://assetstore.unity.com/packages/3d/vehicles/land/simple-cars-pack-97669

# Info
![GitHub all releases](https://img.shields.io/github/downloads/Blas1ed/Gorilla-Vehicles/total?color=%2300FF00)

