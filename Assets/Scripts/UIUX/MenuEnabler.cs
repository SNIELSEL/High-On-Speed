using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuEnabler : MonoBehaviour
{
    public InputActionAsset inputActions;

    public GameObject Menu;
    public InputAction menuInput;
    private TimeAttack timeAttack;

    public void Start()
    {
        //menuInput = inputActions.FindActionMap("XRI LeftHand").FindAction("Menu");
        menuInput.Enable();
        menuInput.performed += ToggleMenu;
    }

    public void OnDestroy()
    {
        menuInput.performed -= ToggleMenu;
    }

    public void ToggleMenu(InputAction.CallbackContext context)
    {
        if(Menu.activeSelf == true)
        {
            Menu.SetActive(false);
        }
        else if (Menu.activeSelf == false)
        {
            Menu.SetActive(true);
        }
    }

    public void GetTimeAttackAndFinishRace()
    {
        timeAttack = GameObject.FindGameObjectWithTag("Car").GetComponent<TimeAttack>();

        timeAttack.FinishTimeTrial();
    }
}
