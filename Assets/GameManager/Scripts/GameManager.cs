using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveManager
{
    public static GameManager Instance = null;

    public bool combatHappening = false;
    public string currentScene;
    public string previousMusic;

    public void LoadData(GameData data)
    {
        currentScene = data.currentScene;

        foreach (Equipment equipment in FindObjectsOfType<Equipment>())
        {
            if (data.equipmentAmounts.ContainsKey(equipment.getIdentifier()))
            {
                equipment.amount = data.equipmentAmounts[equipment.getIdentifier()];
            }
            if (data.equipmentEquipped.ContainsKey(equipment.getIdentifier())
                && data.equipmentEquipped[equipment.getIdentifier()]) 
            {
                equipment.equip(Player_Movement.Instance.GetComponent<Character>());
            }
        }
        Player_Movement.Instance.GetComponent<Character>().currentHp = data.currentHp;
        Player_Movement.Instance.GetComponent<Character>().currentMp = data.currentMp;
        foreach (Consumable consumable in FindObjectsOfType<Consumable>())
        {
            if (data.consumableAmounts.ContainsKey(consumable.getIdentifier()))
            {
                consumable.amount = data.consumableAmounts[consumable.getIdentifier()];
            }
        }
        FindObjectOfType<Gold>().amount = data.goldAmount;

        foreach (OverworldItem overworldItem in FindObjectsOfType<OverworldItem>())
        {
            if (data.overworldItems.ContainsKey(overworldItem.transform.parent.name))
            {
                overworldItem.IsPickedUp = data.overworldItems[overworldItem.transform.parent.name];
                overworldItem.transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled = !data.overworldItems[overworldItem.transform.parent.name];
                overworldItem.transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = !data.overworldItems[overworldItem.transform.parent.name];
                overworldItem.GetComponent<CircleCollider2D>().enabled = !data.overworldItems[overworldItem.transform.parent.name];
            }
        }
    }

    public void SaveData(GameData data)
    {
        data.currentScene = SceneManager.GetActiveScene().name;

        foreach (Equipment equipment in Player_Movement.Instance.GetComponent<Character>().equipment)
        {
            if (!data.equipmentAmounts.TryAdd(equipment.getIdentifier(), equipment.getAmount()))
            {
                data.equipmentAmounts[equipment.getIdentifier()] = equipment.getAmount();
            }
            if (!data.equipmentEquipped.TryAdd(equipment.getIdentifier(), equipment.equipped))
            {
                data.equipmentEquipped[equipment.getIdentifier()] = equipment.equipped;
            }
        }
        foreach (Consumable consumable in Player_Movement.Instance.GetComponent<Character>().consumables)
        {
            if (!data.consumableAmounts.TryAdd(consumable.getIdentifier(), consumable.getAmount()))
            {
                data.consumableAmounts[consumable.getIdentifier()] = consumable.getAmount();
            }
        }
        data.goldAmount = FindObjectOfType<Gold>().getAmount();

        foreach (OverworldItem overworldItem in FindObjectsOfType<OverworldItem>())
        {
            if (!data.overworldItems.TryAdd(overworldItem.transform.parent.name, overworldItem.IsPickedUp))
            {
                data.overworldItems[overworldItem.transform.parent.name] = overworldItem.IsPickedUp;
            }
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        combatHappening = false;
    }

    public void StartNewGame()
    {
        currentScene = "Village";
        StartCoroutine(LoadScene(currentScene, true));
    }

    public void LoadExistingGame()
    {
        currentScene = "Village";
        StartCoroutine(LoadScene(currentScene, false));
    }

    private IEnumerator LoadScene(string scene, bool isNewGame)
    {
        // Start loading the scene
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
        // Wait until the level finish loading
        while (!asyncLoadLevel.isDone)
            yield return null;
        // Wait a frame so every Awake and Start method is called
        yield return new WaitForEndOfFrame();

        if (!isNewGame) 
        {
            SaveManager.Instance.LoadGame();
            CharacterCreationMenu.Instance.closeCharacterCreationMenu();
        }
        else
        {
            Player_Movement.Instance.setIsInteracting(true);
        }
    }

    public void TeleportPlayer(Vector2 target)
    {
        StartCoroutine(TeleportWithDelay(1, target));
    }

    IEnumerator TeleportWithDelay(int seconds, Vector2 target)
    {
        Player_Movement.Instance.setIsInteracting(true);
        yield return new WaitForSeconds(seconds);
        Player_Movement.Instance.transform.position = target;
        Player_Movement.Instance.setIsInteracting(false);
    }

    public void ReturnToMainMenu()
    {
        MusicManager.Instance.ChangeToMusic("MainMenuMusic");
        currentScene = "MainMenu";
        SceneManager.LoadScene(currentScene);
    }

    public void PlayerHasDied()
    {
        MusicManager.Instance.StopThenPlayOneShot("GameOverMusic");
    }

    public void GameOver()
    {
        combatHappening = false;
        MusicManager.Instance.ChangeToMusic("MainMenuMusic");
        currentScene = "MainMenu";
        SceneManager.LoadScene(currentScene);
    }

    public void StartCombat(GameObject enemy)
    {
        combatHappening = true;
        BattleManager.Instance.beginCombat(enemy);
        previousMusic = MusicManager.Instance.currentSong.name;
        MusicManager.Instance.ChangeToMusic("BattleMusic");
    }

    public void EndCombat()
    {
        combatHappening = false;
        MusicManager.Instance.ChangeToMusic(previousMusic);
        if (LevelUpMenu.Instance.hasLeveled())
        {
            LevelUpMenu.Instance.levelUp();
        }
    }
}
