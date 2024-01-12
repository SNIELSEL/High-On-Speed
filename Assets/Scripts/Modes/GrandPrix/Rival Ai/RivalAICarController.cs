using Meta.Voice.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalAICarController : MonoBehaviour
{
    [Header("Wheel Colliders")]

    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider rearRight;
    [SerializeField] WheelCollider rearLeft;


    [Header("Tire Transforms")]

    [SerializeField] Transform frontRightTireTransform;
    [SerializeField] Transform frontLeftTireTransform;
    [SerializeField] Transform rearRightTireTransform;
    [SerializeField] Transform rearLeftTireTransform;


    [Header("Accelaration And Deceleration Forces")]

    [SerializeField] private float accelarationForce;
    [SerializeField] private float brakeForce;
    [SerializeField] private float maximalAccelarationForce;

    private float currentAccelarationForce = 0f;
    private float currentBrakeForce = 0f;


    [Header("Gear Handeling")]

    [SerializeField] private float[] gearSpeedAmount;

    [SerializeField] private int gear;
    [SerializeField] private int maximalGear;
    [SerializeField] private int minimalGear;


    [Header("Bools")]

    [SerializeField] public bool raceStart;
    [SerializeField] private bool inBrakeZone;


    [Header("Particles and sounds")]

    [SerializeField] private AudioPlayer accelarationSound;
    [SerializeField] private AudioPlayer gearSwitchSound;
    [SerializeField] private AudioPlayer brakingSound;

    [SerializeField] private GameObject gearSwitch;

    private Vector3 wheelPosition;
    private Quaternion wheelRotation;


    [Header("Waypoints")]

    [SerializeField] private GameObject[] wayPoints;
    [SerializeField] private int currentWayPoint;


    [Header("Steering")]

    [SerializeField] private float steeringSpeed;

    private void Start()
    {
        raceStart = false;
        inBrakeZone = false;
    }

    private void Update()
    {
        GearBox();
    }

    private void FixedUpdate()
    {

        if (raceStart == true)
        {
            Accelaration();
            Steering();
        }
        
        if (inBrakeZone == true)
        {
            Decelaration(); 
        }

        // makes the wheel mashes rotate and turn along side the wheel colliders
        WheelTransform(frontRight, frontRightTireTransform);
        WheelTransform(frontLeft, frontLeftTireTransform);
        WheelTransform(rearRight, rearRightTireTransform);
        WheelTransform(rearLeft, rearLeftTireTransform);

    }

    private void OnTriggerStay(Collider brakeZone)
    {
        if (gameObject.tag == ("Brakezone"))
        {
            inBrakeZone = true;
        }
    }

    private void OnTriggerEnter(Collider brakeZone)
    {
        if (gameObject.tag == ("Brakezone"))
        {
            inBrakeZone = false;
        }
    }

    private void WheelTransform(WheelCollider wheelCollider, Transform tireTransforms)
    {
        // getting the state of the collider
        wheelCollider.GetWorldPose(out wheelPosition, out wheelRotation);

        // gating the state of the transform
        tireTransforms.position = wheelPosition;
        tireTransforms.rotation = wheelRotation;
    }

    private void Accelaration()
    {
        // sets the curent accalaration force to the same value as the accelaration force
        currentAccelarationForce = accelarationForce;

        // gives accelerationForce to the rear wheel's (creating rear wheel drive)
        rearLeft.motorTorque = currentAccelarationForce;
        rearRight.motorTorque = currentAccelarationForce;

        if (accelarationForce != 0 && accelarationForce <= maximalAccelarationForce)
        {
            accelarationForce += UnityEngine.Random.Range(0.5f, 1);
        }
    }

    private void Decelaration()
    {
        // sets the curent brake force to the same value as the brake force
        currentBrakeForce = brakeForce;

        // sets brakeForce to the wheel's so the car can brake
        frontRight.brakeTorque = currentBrakeForce;
        frontLeft.brakeTorque = currentBrakeForce;
        rearRight.brakeTorque = currentBrakeForce;
        rearLeft.brakeTorque = currentBrakeForce;

        // makes the cars accelaration force go down whilest braking
        if (accelarationForce > 0)
        {
            accelarationForce -= 1;
        }
    }

    private void GearBox()
    {
        // this section is for automated gear shifting going up
        if (accelarationForce >= gearSpeedAmount[gear] && gear < maximalGear)
        {
            gear += 1;
        }

        // this section is for automated gear shifting going down
        if (accelarationForce <= gearSpeedAmount[gear] && gear > minimalGear)
        {
            gear -= 1;
        }
    }

    private void Steering()
    {
        if (Vector3.Distance(transform.position, wayPoints[currentWayPoint].transform.position) < 5)
        {
            currentWayPoint ++;
        }

        frontRightTireTransform.position = Vector3.Lerp(frontRightTireTransform.position, wayPoints[currentWayPoint].transform.position, steeringSpeed);
        //frontRightTireTransform.LookAt(wayPoints[currentWayPoint].transform.position);
    }
}
