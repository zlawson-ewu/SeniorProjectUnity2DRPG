using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    public float defaultVolume;

    public AudioSource audioSource;
    public List<AudioClip> soundList = new();

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

    public void PlaySFX(string sfxName)
    {
        audioSource.PlayOneShot(soundList.Find(x => x.name == sfxName));
    }

}
