using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class VideoDisabler : MonoBehaviour
{
    public VideoPlayer[] vids;
    public GameObject[] videoPlayerObjects;

    public GameObject rayCaster;

    [NonSerialized]
    public PlayVideoOnHover playVideoOnHover;

    [NonSerialized]
    public bool uiClicked;

    public void ToggleMenu(InputAction.CallbackContext context)
    {
        if (playVideoOnHover.onTrigger != null && !uiClicked)
        {
            uiClicked = true;

            playVideoOnHover.onTrigger.Invoke();

            StartCoroutine(ClickDelay());
        }
    }
    public void Start()
    {
        ScanObjects();
    }

    public void ScanObjects()
    {
        videoPlayerObjects = GameObject.FindGameObjectsWithTag("VideoPlayer");

        vids = new VideoPlayer[videoPlayerObjects.Length];

        for (int i = 0; i < videoPlayerObjects.Length; i++)
        {
            vids[i] = videoPlayerObjects[i].GetComponent<VideoPlayer>();
        }
    }

    public void DisableAllVids()
    {
        for (int i = 0; i < vids.Length; i++)
        {
            vids[i].Pause();
        }
    }

    public IEnumerator ClickDelay()
    {
        yield return new WaitForSeconds(0.3f);
        uiClicked = false;
    }
}
