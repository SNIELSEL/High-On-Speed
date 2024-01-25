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
    [SerializeField] private float maximalBrakeForce;
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
    [SerializeField] private int nextWaypoint;

    private Vector3 wheelPosition;
    private Quaternion wheelRotation;

    public IEnumerator Delayer()
    {
        yield return new WaitForSeconds(10);
        raceStart = true;
    }

    private void Start()
    {
        StartCoroutine(Delayer());
        raceStart = false;
        inBrakeZone = false;

        StartCoroutine(TimerCheck());
    }

    private void Update()
    {
        GearBox();
    }

    private void FixedUpdate()
    {
        //runs the code in the voids if the race starts
        if (raceStart == true)
        {
            SafeCurrentWayPoint();

            AiTeleporter();
        }

        //makes it so the car wont bottem out in the brakeing zone
        if (inBrakeZone == true)
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
        }

        //makes it zo that if The AI leave's the breaking zone the cars rotation isnt locked anymore
        else
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }

        if (raceStart == true && inBrakeZone == false)
        {
            Accelaration();
            Steering();
        }
        
        if (inBrakeZone == true)
        {
            Deceleration();
            Debug.Log(brakeForce);
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
        // sets the current acceleration force to the same value as the acceleration force
        currentAccelarationForce = accelarationForce;

        // gives acceleration force to the rear wheels (creating rear-wheel drive)
        rearLeftWheelCollider.motorTorque = currentAccelarationForce;
        rearRightWheelCollider.motorTorque = currentAccelarationForce;

        // makes it so that when the car needs to accelerate it does so in a range of 0.5 to 1
        if (accelarationForce >= 0 && accelarationForce <= maximalAccelarationForce && inBrakeZone == false)
        {
            accelarationForce += UnityEngine.Random.Range(0.5f, 1);
        }

        if (inBrakeZone == false)
        {
            // Reset brake torque after braking
            currentBrakeForce = 0f;
            frontRightWheelCollider.brakeTorque = 0f;
            frontLeftWheelCollider.brakeTorque = 0f;
            rearRightWheelCollider.brakeTorque = 0f;
            rearLeftWheelCollider.brakeTorque = 0f;
        }
    }

    private void Deceleration()
    {
        currentBrakeForce = brakeForce;

        // sets brake force to the wheel's so the car can brake
        frontRightWheelCollider.brakeTorque = currentBrakeForce;
        frontLeftWheelCollider.brakeTorque = currentBrakeForce;
        rearRightWheelCollider.brakeTorque = currentBrakeForce;
        rearLeftWheelCollider.brakeTorque = currentBrakeForce;

        // Reset motor torque after braking
        rearLeftWheelCollider.motorTorque = 0f;
        rearRightWheelCollider.motorTorque = 0f;

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

    private void SafeCurrentWayPoint()
    {
        PlayerPrefs.SetInt("wayPoint", currentWayPoint);
    }

    private void AiTeleporter()
    {
        nextWaypoint = currentWayPoint + 1;
    }

    private IEnumerator TimerCheck()
    {
        yield return new WaitForSeconds(240);

        if ( PlayerPrefs.GetInt("wayPoint") == currentWayPoint)
        {
            transform.position = wayPoints[nextWaypoint].transform.position;

            currentWayPoint = nextWaypoint;
        }

        StartCoroutine(TimerCheck());
    }

}
