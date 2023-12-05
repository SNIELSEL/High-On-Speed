using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarAceleration : MonoBehaviour
{
    [SerializeField] WheelCollider frontRight, frontLeft, rearRight, rearLeft;

    [SerializeField]
    private float accelarationForce, brakeForce, brakeValue, accelarationValue, speedMultiplier, reverceForce;

    [SerializeField]
    private float maximalTurnAngle, turnInput;

    private float currentAccelarationForce = 0f, currentBrakeForce = 0f, currentTurnAngle = 0f;
    private int gear, minimalGear, maximalGear;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        // increses the speed when you hold the gas paddel
        if (accelarationValue != 0)
        {
            accelarationForce += speedMultiplier * accelarationValue;
        }

        // makes the car lose speed when you dont accelarate
        else
        {
            if (accelarationForce >= 0)
            {
                accelarationForce -= speedMultiplier;
            }
        }

        //m makes the cars acceleration go down after braking resulting in the speed going down
        if (brakeValue != 0)
        {
            accelarationForce -= speedMultiplier * accelarationValue;
        }

        // makes the car reverse
        if (brakeForce != 0 && accelarationForce == 0 && reverceForce <= 160)
        {
            accelarationForce -= speedMultiplier * brakeValue;
        }

        // sets the values of the forces and sets the amount of force they will have
        currentBrakeForce = brakeForce * brakeValue;
        currentAccelarationForce = accelarationForce * accelarationValue;

        // gives accelerationForce to the rear wheel's (creating rear wheel drive)
        rearLeft.motorTorque = currentAccelarationForce;
        rearRight.motorTorque = currentAccelarationForce; 

        // sets brakeForce to the wheel's so the car can brake
        frontRight.brakeTorque = currentBrakeForce;
        frontLeft.brakeTorque = currentBrakeForce;
        rearRight.brakeTorque = currentBrakeForce;
        rearLeft.brakeTorque = currentBrakeForce;

        /*sets the max amount of degreas the wheel's are allowed to turn
        and sets the steering to the front wheels whilest binmding it to the value of your left trigger*/
        currentTurnAngle = maximalTurnAngle * turnInput;
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;
    }

    // handels every gear
    private void Gearbox()
    {

    }

    private void OnGas(InputValue accelaration)
    {
        accelarationValue = accelaration.Get<float>();
    }

    private void OnBrake(InputValue brake)
    {
        brakeValue = brake.Get<float>();
    }

    private void OnSteering(InputValue turning)
    {
        turnInput = turning.Get<Vector2>().x;
    }

}
