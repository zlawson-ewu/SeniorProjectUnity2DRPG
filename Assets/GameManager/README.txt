Game Manager v1

The Game Manager prefab has a GameManager script attached to it, the GM is a singleton. It contains methods that switch the scene based on what is called. In order to test this, you will need to make sure that CombatScene and MainMenu are active in your build settings. 

If correct, then when simulating, running into the red enemy will trigger combat by calling StartCombat() in the GM, when combat ends in success, EndCombat() is called to load the previous scene (marked origin in GM). 

I have also tested the Game Over button in the BattleUI at the end of combat failure with the GameOver() method in GM and it is functional. 

I have not yet figured out how to preserve the state of the old scene, but it does switch reliably. I have also implemented the ISaveManager interface to save the current scene to the save file. Other limitations are that scene names for combat and the main menu are hard coded, though this may not ultimately be an issue.