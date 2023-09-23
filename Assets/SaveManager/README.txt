Save Manager v1

SaveManager is a singleton class that handles saving. A game object must be created and assigned the SaveManager script to function.

The save manager talks to any script that implements ISaveManager when it saves and loads. Any script that needs something from a save or load must implement ISaveManager. 

The GameData script is what defines all of the stored variables. A script implementing ISaveManager will use the LoadData and StoreData functions to assign or retrieve values to/from these variables (likely just the character class, to keep things simple). Since Json is fairly simple, the SerializableDictionary script is for building associative lists (for things like what flags have been set or what's in the player's inventory) so that they can be saved/read from the Json file.

I've included a version of "character.cs" modified to work with the save manager as it currently is.

When SaveGame or LoadGame are called from SaveManager, a file will be created/read from the default unity save location (C:\Users\[user]\AppData\LocalLow\DefaultCompany\[project]), named whatever is assigned in the script UI, and possibly encrypted (via XOR) depending on the "use encryption" setting.

The save/load point prefabs have a simple script for testing, and must be pointed at a save manager object to function. Colliding with the object will cause a save or load to happen. To test, move to the up triangle to save the data, check the file location to see that it is saved, change one of the player's stats during runtime, then move to the down triangle and observe the stat change back as data is loaded. Data will also (obviously) persist between runs.

Once the "New Game" scene is created, NewGame() from the save manager must be called to instantiate the values so the character can be saved.