using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioClip music;

    private void Awake()
    {
        GameObject musicplayer = GameObject.FindGameObjectWithTag("Music");
        musicplayer.GetComponent<AudioSource>().clip = music;
        musicplayer.GetComponent<AudioSource>().Play();
        DontDestroyOnLoad(this.gameObject);

    }
}
