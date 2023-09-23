using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance = null;

    public float defaultVolume;

    public AudioSource audioSource;
    public List<AudioClip> musicList = new();
    public AudioClip currentSong;

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
        audioSource = GetComponent<AudioSource>();
        defaultVolume = audioSource.volume;
        currentSong = musicList[0];
        audioSource.clip = currentSong;
        audioSource.Play();
    }

    public void ChangeToMusic(string songName)
    {
        audioSource.Stop();
        currentSong = musicList.Find(x => x.name == songName);
        audioSource.clip = currentSong;
        audioSource.Play();
    }

    public void StopThenPlayOneShot(string songName)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(musicList.Find(x => x.name == songName));
    }
}
