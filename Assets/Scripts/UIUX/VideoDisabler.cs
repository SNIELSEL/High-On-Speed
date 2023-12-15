using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;
using static UnityEngine.Rendering.DebugUI;

public class VideoDisabler : MonoBehaviour
{
    public VideoPlayer[] vids;
    public GameObject[] videoPlayerObjects;

    public GameObject rayCaster;

    public PlayVideoOnHover playVideoOnHover;

    [NonSerialized]
    public bool uiClicked;

    [NonSerialized]
    public float triggerFloat;

    private void OnClick(InputValue value)
    {
        triggerFloat = value.Get<float>();
    }

    public void ToggleMenu(InputAction.CallbackContext context)
    {
        if (playVideoOnHover.onTrigger != null && triggerFloat <= 0.8f)
        {
            playVideoOnHover.onTrigger.Invoke();
            playVideoOnHover.triggerInput.Disable();
        }
    }
    public void Start()
    {
        ScanObjects();
    }

    public void Update()
    {
        if(triggerFloat == 0)
        {
            if(playVideoOnHover != null)
            {
                playVideoOnHover.triggerInput.Enable();
            }
        }
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
}
