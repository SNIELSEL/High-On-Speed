using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoverExit : MonoBehaviour
{
    public static HoverExit instance;

    [HideInInspector]
    public PlayVideoOnHover currentCollider;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void OnDisable()
    {
        if(currentCollider != null)
        {
            currentCollider.ToggleMenu();

            if (currentCollider.onExit != null)
            {
                currentCollider.onExit.Invoke();
            }
        }
    }
}
