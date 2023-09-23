using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;
    [SerializeField] private GameData gameData;
    private List<ISaveManager> saveManagerObjects;
    private FileDataHandler dataHandler;

    public static SaveManager Instance { get; private set; }

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

    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        saveManagerObjects = FindAllSaveManagerObjects();
    }
    
    public void NewGame()
    {
        gameData = new GameData();
        GameManager.Instance.StartNewGame();
    }

    public bool CheckIfGameDataExists() => dataHandler.Load() != null;

    public void LoadGame()
    {
        gameData = dataHandler.Load();
        if (gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

        saveManagerObjects = FindAllSaveManagerObjects();
        foreach (ISaveManager saveManagerObj in saveManagerObjects)
        {
            Debug.Log($"{saveManagerObj.GetType().Name} called its LoadData()");
            saveManagerObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        saveManagerObjects = FindAllSaveManagerObjects();
        foreach (ISaveManager saveManagerObj in saveManagerObjects)
        {
            Debug.Log($"{saveManagerObj.GetType().Name} called its SaveData()");
            saveManagerObj.SaveData(gameData);
        }
        dataHandler.Save(gameData);
    }

    private List<ISaveManager> FindAllSaveManagerObjects()
    {
        //scripts that implement ISaveManager must be monobehaviour to be found
        IEnumerable<ISaveManager> saveManagerObjects = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();

        return new List<ISaveManager>(saveManagerObjects);
    }
}
