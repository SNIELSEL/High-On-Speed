using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        currentCollider.onExit.Invoke();
    }
}
