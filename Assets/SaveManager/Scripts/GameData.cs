using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]

public class GameData
{
    public string currentScene;
    public Vector3 currentPos;

    public SerializableDictionary<string, bool> questGivers;
    public SerializableDictionary<string, int> questGiversCount;
    public SerializableDictionary<string, bool> questGiversRewarded;

    public SerializableDictionary<string, bool> overworldItems;

    public SerializableDictionary<string, int> equipmentAmounts;
    public SerializableDictionary<string, bool> equipmentEquipped;
    public SerializableDictionary<string, int> consumableAmounts;
    public int goldAmount;

    public SerializableDictionary<string, bool> enemyIsDead;
    public SerializableDictionary<string, Vector3> enemyPos;

    public string characterName;
    public int level;
    public int experiencePoints;
    public int strength;
    public int dexterity;
    public int constitution;
    public int intelligence;
    public int currentHp;
    public int currentMp;

    //public SerializableDictionary<string, int> inventory; //item name, amount, this dictionary will let the inventory be saved in Json
    //can also use the serializeable dictionary to save things like quest flags
    public GameData() //default values on new game creation, filled in by player through character creation menu
    {
        currentScene = "";
        currentPos = Vector3.zero;

        questGivers = new();
        questGiversCount = new();
        questGiversRewarded = new();

        overworldItems = new();

        equipmentAmounts = new();
        equipmentEquipped = new();
        consumableAmounts = new();
        goldAmount = 0;

        enemyIsDead = new();
        enemyPos = new();

        characterName = "";
        level = 1;
        experiencePoints = 0;
        strength = 0;
        dexterity = 0;
        constitution = 0;
        intelligence = 0;
        currentHp = 0;
        currentMp = 0;
}
}
