using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaMusicTrigger : MonoBehaviour
{
    public AudioClip areaMusic;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player_Movement player = collision.gameObject.GetComponent<Player_Movement>();
        if (player != null) 
        {
            Debug.Log($"Entering { gameObject.name }");
            MusicManager.Instance.ChangeToMusic(areaMusic.name);
        }
    }
}
