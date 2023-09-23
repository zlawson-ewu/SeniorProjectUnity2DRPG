The music manager controls all music, anything to do with starting, stopping, or modifying the playing of music is controlled here. It is a singleton like the other managers.

The area trigger prefab is attached to the area triggers which call the music manager's ChangeToMusic() method with the attached song.

The only hardcoded tracks are GameOverMusic and BattleMusic, these are called within Game Manager. The Boss fight music will also likely be hardcoded. This can be changed later if need be.

The music folder contains songs I found online for free at https://escalonamusic.itch.io/action-rpg-music-free