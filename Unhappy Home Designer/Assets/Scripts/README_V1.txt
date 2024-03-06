Instructions for Basic Building and Money Management scripts.


GameObjects you will need:

- Grid
- 2 Tilemaps (child of Grid)
* Name one tilemap "Temp Tilemap" and the other "Main Tilemap".

- Canvas
- Button (child of Canvas)
- TextMeshPro Prefab (child of Canvas)
*The button will instantiate a piece of furniture; make one per piece of furniture.
*The TextMeshPro prefab displays the player's current amount of money.

- Empty Prefab
- 2D Sprite (child of Canvas)
*The 2D Sprite will hold the furniture sprite itself.


**To create a prefab, drag a GameObject in the scene into the Project tab.


Script 1: GridBuildingSystem.cs

Attach GridBuildingSystem to your Grid. Attach the following GameObjects in the inspector:

 Grid Layout - Grid
 Main Tilemap - Main Tilemap
 Temp Tilemap - Temp Tilemap

Explanation: This script is the backbone behind the isometric grid system. The Main Tilemap contains the background elements (Namely the room itself; white tiles), while the Temp Tilemap is where the green and red tiles temporarily appear while placing a piece of furniture.


Script 2: MoneyManager.cs

Attach this script to your TextMeshPro prefab. Attach the following components in the inspector:

 Text - TextMeshPro - Text (UI) (This is a component of the same TextMeshPro prefab)

Explanation: This script calculates the player's money and displays it on-screen.

WARNING: This script may not fully work in its current state. It is able to calculate money, but may not be able to properly display these changes on-screen.


Script 3: Building.cs


Part I: The Script Itself

Attach this script to each of your Empty Prefabs parenting a piece of furniture. Modify the following in the inspector:

 Area
  (Optional) Position - Decides where the object will be placed when instantiated.
  Size - The length and width (in tiles) of the piece of furniture. The Z axis value should ALWAYS be 1.

 Cost - How much the piece of furniture costs (in whole dollars).
 Money Manager Object - Holds the GameObject that has the MoneyManager script; likely your TextMeshPro prefab.

Explanation: Each piece of furniture will have one of these scripts attached to them, as it is what allows them to be dragged and placed. It also gives them individualized prices.


Part II: The Button Which Instantiates

Take the following steps:

1. Go to your Button and add an On Click() to it in the inspector (this is done by clicking + in the GameObject's button component).

2. Drag your Grid to the new On Click().

3. Click on the top-left bar in the On Click() and find GridBuildingSystem, then find InitializeWithBuilding(GameObject).

4. Take the furniture prefab that you want this button to instantiate; drag it to the remaining slot in On Click().

When clicked, this button will now instantiate the piece of furniture from step 4.


Part III: Tiles and Tilemaps

You'll need to have the following sprites available:

 White Tiles
 Green Tiles
 Red Tiles

Take the following steps: 

1. Create a folder named Resources (I created it within the Assets folder; don't know what happens if you create it any deeper).

2. Create another folder within Resources named Tiles.

3. Import your tile sprites to Unity; moving them to Tiles is optional (I think).

4. Open up the Tile Palette and create a new palette if you haven't already.

5. Switch your Active Tilemap to Temp Tilemap if it's not already.

6. Drag your sprites to the Tile Palette; save these new instances to the Tiles folder as "white", "green", and "red" (without the quotes). Remember, these names are case sensitive.

7. You can add "white" to your Room Tilemap in the Tile Palette.

Explanation: white is your room tile; I'll modify it later on if the room's color or design changes. It serves as a guide for the other two tiles, so place them (within Room Tilemap, NOT Temp) where you want furniture to be placeable. green will tell you where your furniture is placeable, while red will tell you where it is not.

Note: If Part IV isn't working as intended, try moving your tile sprites from step 3 to the Tiles folder if you haven't done so yet.


Part IV: How To Use Everything

Take the following steps while in play mode:

1. Click on the button from Part II. This should instantiate a piece of furniture.

2. Click and drag the furniture to move it.

3. Press Space to place the furniture. The furniture's cost will be subtracted from the player's money.

Note: To place furniture, you must make sure you are no longer clicking on it.

WARNING: The button has no limit on the amount of times it can instantiate. Clicking it again before placing your current furniture removes your control over that piece of furniture in favor of a new copy.

WARNING: At the moment, once you place furniture, you cannot unplace nor move it.

WARNING: It is currently possible to place multiple pieces of furniture in the same spot.