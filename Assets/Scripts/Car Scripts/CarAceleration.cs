using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarAceleration : MonoBehaviour
{
    private VrController controlls;
    private Vector3 movementInput;
    private InputAction rightTrigger, leftTrigger;


    [SerializeField] WheelCollider frontRight, frontLeft, rearRight, rearLeft;

    [SerializeField]
    private float accelaration, brakeForce;

    private float currentAccelaration = 0f, currentBrakeForce = 0f ;


    public void Awake()
    {
        controlls = new VrController();
    }

    public void OnEnable()
    {
 /*       controlls.Enable();

        rightTrigger.Enable();
        rightTrigger = controlls.Controller.Gas;

        leftTrigger.Enable();
        leftTrigger = controlls.Controller.Brake;
*/
/*        leftTrigger.started += OnBrake;
        leftTrigger.performed += OnBrake;
        leftTrigger.canceled += OnBrake;*/
    }

    public void OnDisable()
    {
       /* controlls.Disable();
        rightTrigger.Disable();
        leftTrigger.Disable();*/

/*        leftTrigger.started -= OnBrake;
        leftTrigger.performed -= OnBrake;
        leftTrigger.canceled -= OnBrake;*/
    }

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
    }


    void Update()
    {
        
    }

    private void OnBrake(InputValue brake)
    {
        Debug.Log(brake.Get<float>());
       
        /*if (brake.started)
        {
            currentBrakeForce = brakeForce;
        }

        else if (brake.canceled)
        {
            currentBrakeForce = 0f;
        }*/

    }
}
