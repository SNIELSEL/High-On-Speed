using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;

public class ZRotationButton : MonoBehaviour
{
    VrController vrController;
    
    private InputAction input;

    public Rigidbody rb;

    private bool boolCheck;

    private void Awake()
    {
        vrController = new VrController();
    }

    private void OnEnable()
    {
        input = vrController.Controller.A;

        input.Enable();
        input.started += Click;
    }

    public void Click(InputAction.CallbackContext context)
    {

        if (boolCheck)
        {
            boolCheck = false;
            rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            boolCheck = true;
            rb.constraints = RigidbodyConstraints.None;
        }
    }
}
