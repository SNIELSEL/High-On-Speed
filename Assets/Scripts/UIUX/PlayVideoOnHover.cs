using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayVideoOnHover : MonoBehaviour
{
    public UnityEvent onEnter;
    public UnityEvent onExit;

    private void OnTriggerEnter(Collider other)
    {
        HoverExit.instance.currentCollider = GetComponent<PlayVideoOnHover>();
        onEnter.Invoke();
    }
}
