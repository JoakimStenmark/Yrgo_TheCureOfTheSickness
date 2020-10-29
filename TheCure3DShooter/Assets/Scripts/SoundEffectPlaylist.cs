using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlaylist : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource soundPlayer;
    void Start()
    {
        soundPlayer = GetComponent<AudioSource>();

        int clipNumber = Random.Range(0, audioClips.Length);
        soundPlayer.clip = audioClips[clipNumber];
        soundPlayer.pitch = Random.Range(0.8f, 1.1f);
        soundPlayer.Play();
        Debug.Log("Play?");

    }
        

}
