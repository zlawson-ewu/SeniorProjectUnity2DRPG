using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public List<GameObject> NPCPrefabs;
    public List<GameObject> enemyPrefabs;

    public static SpawnManager Instance = null;
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

    public void SpawnPlayer(Vector2 spawnLoc)
    {
        Debug.Log("Spawning Player");
        Instantiate(playerPrefab);
        playerPrefab.transform.position = spawnLoc;
        playerPrefab.transform.rotation = Quaternion.identity;
    }

    public void SpawnNPCs()
    {
        foreach (GameObject npc in NPCPrefabs)
        {
            Debug.Log($"Spawning {npc.transform.parent.name}");
            Instantiate(npc);
        }
    }

    public void SpawnEnemies()
    {
        foreach (GameObject enemy in enemyPrefabs)
        {
            Debug.Log($"Spawning {enemy.transform.parent.name}");
            Instantiate(enemy);
        }
    }
}
