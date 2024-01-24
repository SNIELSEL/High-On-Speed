using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class PlayVideoOnHover : MonoBehaviour
{
    VRRaceGame game;

    public UnityEvent onEnter;
    public UnityEvent onExit;
    public UnityEvent onTrigger;

    private VideoDisabler videoDisabler;

    private InputActionAsset inputActions;

    public InputAction triggerInput;

    private void Awake()
    {
        game = new VRRaceGame();
    }
    public void Start()
    {
        triggerInput = game.UI.Click;

        videoDisabler = GameObject.Find("Menu Manager").GetComponent<VideoDisabler>();

        triggerInput.Enable();
        triggerInput.performed += videoDisabler.ToggleMenu;
    }

    private void OnTriggerEnter(Collider other)
    {
        videoDisabler.rayCaster.SetActive(false);
        videoDisabler.rayCaster.SetActive(true);

        videoDisabler.playVideoOnHover = this;

        HoverExit.instance.currentCollider = GetComponent<PlayVideoOnHover>();
        videoDisabler.DisableAllVids();

        if(onEnter != null)
        {
            onEnter.Invoke();
        }
    }

    public void ToggleMenu()
    {
        triggerInput.Enable();
        triggerInput.performed += videoDisabler.ToggleMenu;
    }

    public void OnDisable()
    {
        triggerInput.performed += videoDisabler.ToggleMenu;
    }
}
