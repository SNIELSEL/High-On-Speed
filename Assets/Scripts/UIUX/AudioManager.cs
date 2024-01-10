using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    public string[] clipNames;
    public void Awake()
    {
        clipNames = new string[audioClips.Length];

        for (int i = 0; i < audioClips.Length; i++)
        {
            clipNames[i] = audioClips[i].name;
        }
    }
}