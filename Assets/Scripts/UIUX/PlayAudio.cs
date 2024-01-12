using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource audioSource;

    private AudioManager audioManager;

    public bool playLock;
    public bool playOnAwake;

    [HideInInspector]
    public int arrayIdx = 0;
    [HideInInspector]
    public string[] audioClipToPlay = new string[] { "F1_Starting_light_sound", "GT Start Sound", "Flag Flapping Sound Effect", "16bit win sound", "Car Passing By", "badass-winsound", "Gaming Win Sound"};

    public void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void Start()
    {
        if (playOnAwake)
        {
            playAudio();
        }
    }

    public void Update()
    {
        if (playLock)
        {
            playLock = false;
            playAudio();
        }
    }
    public void playAudio()
    {
        audioSource.clip = audioManager.audioClips[arrayIdx];
        audioSource.Play();
    }
}
