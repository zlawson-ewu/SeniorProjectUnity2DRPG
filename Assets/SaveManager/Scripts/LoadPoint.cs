using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPoint : MonoBehaviour
{
    public SaveManager SaveManager = SaveManager.Instance;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Loading Game");
            SaveManager.LoadGame();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Game Loaded");
        }
    }
}
