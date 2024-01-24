using Meta.Voice.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalAICarController : MonoBehaviour
{
    [Header("Wheel Colliders")]

    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;


    [Header("Tire Transforms")]

    [SerializeField] private Transform frontRightTireTransform;
    [SerializeField] private Transform frontLeftTireTransform;
    [SerializeField] private Transform rearRightTireTransform;
    [SerializeField] private Transform rearLeftTireTransform;


    [Header("Steering")]

    [SerializeField] private float steeringSpeed;

    [SerializeField] private Transform frontRightWheelColliderTransform;
    [SerializeField] private Transform frontLeftWheelColliderTransform;

    [SerializeField] private Transform frontRightTireMeshTransform;
    [SerializeField] private Transform frontLeftTireMeshTransform;

    private float targetSteeringAngle;
    private Quaternion toRotation;


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


    [Header("Waypoints")]

    [SerializeField] private GameObject[] wayPoints;
    [SerializeField] private int currentWayPoint;

            
    private Vector3 wheelPosition;
    private Quaternion wheelRotation;

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
        if (raceStart == true && inBrakeZone == false)
        {
            Accelaration();
            Steering();
        }
        
        if (inBrakeZone == true)
        {
            Deceleration();
        }

        // makes the wheel mashes rotate and turn along side the wheel colliders
        WheelTransform(frontRightWheelCollider, frontRightTireTransform);
        WheelTransform(frontLeftWheelCollider, frontLeftTireTransform);
        WheelTransform(rearRightWheelCollider, rearRightTireTransform);
        WheelTransform(rearLeftWheelCollider, rearLeftTireTransform);
    }

    private void OnTriggerStay(Collider brakeZone)
    {
        Debug.Log("OnTriggerStay called");
        if (brakeZone.tag == "BrakingZone")
        {
            inBrakeZone = true;
        }
    }

    private void OnTriggerExit(Collider brakeZone)
    {
        Debug.Log("OnTriggerExit called");
        if (brakeZone.tag == "BrakingZone")
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
        rearLeftWheelCollider.motorTorque = currentAccelarationForce;
        rearRightWheelCollider.motorTorque = currentAccelarationForce;

        // makes it so that when the car needs to accelarate it dous so in a range of 0.5 to 1
        if (accelarationForce >= 0 && accelarationForce <= maximalAccelarationForce && inBrakeZone == false)
        {
            accelarationForce += UnityEngine.Random.Range(0.5f, 1);
        }

        brakeForce = 0f;
    }

    private void Deceleration()
    {
        currentBrakeForce = brakeForce;

        // sets brakeForce to the wheel's so the car can brake
        frontRightWheelCollider.brakeTorque = currentBrakeForce;
        frontLeftWheelCollider.brakeTorque = currentBrakeForce;
        rearRightWheelCollider.brakeTorque = currentBrakeForce;
        rearLeftWheelCollider.brakeTorque = currentBrakeForce;


        accelarationForce = 100f;
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
        // makes it so that when the car passes a waypoint it moves tothe next one
        if (Vector3.Distance(transform.position, wayPoints[currentWayPoint].transform.position) < 5)
        {
            currentWayPoint ++;
        }

        // calculates the direction the next waypoint is facing
        Vector3 directionToWaypoint = wayPoints[currentWayPoint].transform.position - transform.position;

        // makes it so that the visual tire mesahes get rotated to the waypoint
        toRotation = Quaternion.LookRotation(wayPoints[currentWayPoint].transform.position - transform.position, Vector3.up);

        // rotates the meshes of the wheels smoothly towards the waypoint
        frontRightTireMeshTransform.rotation = Quaternion.Lerp(frontRightTireMeshTransform.rotation, toRotation, steeringSpeed);
        frontLeftTireMeshTransform.rotation = Quaternion.Lerp(frontLeftTireMeshTransform.rotation, toRotation, steeringSpeed);

        // calculates the difference for the Wheel Colliders rotation angle
        targetSteeringAngle = toRotation.eulerAngles.y - transform.eulerAngles.y;

        // sets the steerAngle of the wheel colliders to the same value as the targetSteerAngle
        frontRightWheelCollider.steerAngle = targetSteeringAngle;
        frontLeftWheelCollider.steerAngle = targetSteeringAngle;
    }
}
