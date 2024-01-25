using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarAceleration : MonoBehaviour
{
    [Header("Wheel Colliders")]

    [SerializeField] WheelCollider frontRightWheelCollider;
    [SerializeField] WheelCollider frontLeftWheelCollider;
    [SerializeField] WheelCollider rearRightWheelCollider;
    [SerializeField] WheelCollider rearLeftWheelCollider;

    [Header("Tire Transforms")]

    [SerializeField] Transform frontRightTireTransform;
    [SerializeField] Transform frontLeftTireTransform;
    [SerializeField] Transform rearRightTireTransform;
    [SerializeField] Transform rearLeftTireTransform;

    [Header("Accelaration and Decelaration")]

    [SerializeField] private float accelarationForce;
    [SerializeField] private float brakeForce;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float maximalAccelarationForce;
    [SerializeField] private float brakeValue;
    [SerializeField] private float accelarationValue;
    [SerializeField] private float uiSpeedDevider;
    [SerializeField] private float speedBoostDuration;
    [SerializeField] private float speedPenaltyDuration;

    [Header("Steering")]

    [SerializeField] private float maximalTurnAngle;
    [SerializeField] private float turnInput;

    [Header ("Gear Handeling")]

    [SerializeField] private float[] gearSpeedAmount;

    [SerializeField] private int gear;
    [SerializeField] private int maximalGear;
    [SerializeField] private int minimalGear;

    [SerializeField] private bool manualGearShifting;
    [SerializeField]  bool automatedGearShifting;


    [Header("Text Mesh Pro's")]

    [SerializeField] private TextMeshProUGUI showGear;
    [SerializeField] private TextMeshProUGUI showSpeed;

    [Header("Audio")]
    [SerializeField] private AudioSource engineSound;
    [SerializeField] private AudioSource brakeAudio;
    [SerializeField] private AudioSource gearSwitch;
    [SerializeField] private AudioSource gearSwitch2;

    private float currentAccelarationForce = 0f;
    private float currentBrakeForce = 0f;
    private float convertedAccelarationForce;

    private float currentTurnAngle = 0f;
    private float startTurnInput;
    public float steeringSpeedDivider;

    private bool FirstAudioCheck;

    private Vector3 wheelPosition;
    private Quaternion wheelRotation;

    void Start()
    {
        startTurnInput = maximalTurnAngle;

    }

    private void Update()
    {
        maximalTurnAngle = startTurnInput - (accelarationForce / steeringSpeedDivider);

        // makes it so thet when your at the maximal gear it shows you are in the maximal gear
        if(gear > maximalGear) 
        {
            gear = maximalGear;
        }

        // devide's the accelaration force so it looks better for the numbers of UI
        convertedAccelarationForce = accelarationForce / uiSpeedDevider;
        convertedAccelarationForce =(int)convertedAccelarationForce;

        // Coppeling gears's and speed to UI elements
        showGear.text = gear.ToString();
        showSpeed.text = (GetComponent<Rigidbody>().velocity.magnitude * 3.6).ToString("F1");

        // runs the code in void AutomatedGearbox
        AutomatedGearbox();
    }

    void FixedUpdate()
    {
        // increses the speed when you hold the gas paddel
        if (accelarationValue != 0 && accelarationForce <= maximalAccelarationForce)
        {
            if(GetComponent<Rigidbody>().velocity.magnitude * 3.6 <= 30)
            {
                accelarationForce += (speedMultiplier * accelarationValue) * 5000;
            }
            else
            {
                accelarationForce += speedMultiplier * accelarationValue;
            }
        }

        // makes the car lose speed when you dont accelarate
        else
        {
            if (accelarationForce >= 0)
            {
                accelarationForce -= speedMultiplier * uiSpeedDevider;
            }
        }

        // makes the cars acceleration go down after braking resulting in the speed going down
        if (brakeValue != 0)
        {
            accelarationForce -= speedMultiplier * accelarationValue * uiSpeedDevider;
        }

        // makes the car reverse
        if (brakeForce != 0 && accelarationForce != 0 && accelarationForce >= -160)
        {
            accelarationForce -= speedMultiplier * brakeValue;
        }

        // makes sure the accelaration force goes up after going into the negative
        else
        {
            if (brakeForce != 0)
            {
                accelarationForce += speedMultiplier * uiSpeedDevider;
            }
        }

        // sets the values of the forces and sets the amount of force they will have
        currentBrakeForce = brakeForce * brakeValue;
        currentAccelarationForce = accelarationForce * accelarationValue;

        // gives accelerationForce to the rear wheel's (creating rear wheel drive)
        rearLeftWheelCollider.motorTorque = currentAccelarationForce;
        rearRightWheelCollider.motorTorque = currentAccelarationForce; 

        // sets brakeForce to the wheel's so the car can brake
        frontRightWheelCollider.brakeTorque = currentBrakeForce;
        frontLeftWheelCollider.brakeTorque = currentBrakeForce;
        rearRightWheelCollider.brakeTorque = currentBrakeForce;
        rearLeftWheelCollider.brakeTorque = currentBrakeForce;

        /*sets the max amount of degreas the wheel's are allowed to turn
        and sets the steering to the front wheels whilest binding it to the value of your left trigger*/
        currentTurnAngle = maximalTurnAngle * turnInput;
        frontLeftWheelCollider.steerAngle = currentTurnAngle;
        frontRightWheelCollider.steerAngle = currentTurnAngle;

        // makes the wheel mashes rotate and turn along side the wheel colliders
        WheelTransform(frontRightWheelCollider, frontRightTireTransform);
        WheelTransform(frontLeftWheelCollider, frontLeftTireTransform);
        WheelTransform(rearRightWheelCollider, rearRightTireTransform);
        WheelTransform(rearLeftWheelCollider, rearLeftTireTransform);

    }

    //sets the wheels position and rotation tho that of the weheel collider
    private void WheelTransform(WheelCollider wheelCollider, Transform tireTransforms)
    {
        // getting the state of the collider
        wheelCollider.GetWorldPose(out wheelPosition, out wheelRotation);

        // gating the state of the transform
        tireTransforms.position = wheelPosition;
        tireTransforms.rotation = wheelRotation;
    }

    // automated Gear shifting
    private void AutomatedGearbox()
    {
        // makes it so the auto gearbox goes up ga gear every time you are at the maximal speed amount of said gear
        if (accelarationForce >= gearSpeedAmount[gear])
        {
            accelarationForce = gearSpeedAmount[gear];
        }

        if (gear != 0 && gear <= maximalGear)
        {
            if (accelarationForce >= 0 && accelarationForce < gearSpeedAmount[gear])
            {
                //als je tevroeg schakelen
                //accelarationForce -= speedMultiplier * uiSpeedDevider;
            }
        }

        // this section is for automated gear shifting going up
        if (automatedGearShifting == true && accelarationForce >= gearSpeedAmount[gear] && gear < maximalGear)
        {
            gear += 1;
        }

        // this section is for automated gear shifting going down
        if (automatedGearShifting == true && accelarationForce <=  gearSpeedAmount[gear] && gear > minimalGear)
        {
            gear -= 1;
        }
    }


    // a void created by the player input componed used to go up gear
    private void OnGearBoxUp()
    {
        if (manualGearShifting == true && gear <= maximalGear)
        {
            if (!gearSwitch.isPlaying)
            {
                gearSwitch.Play();
            }

            if (!gearSwitch2.isPlaying)
            {
                gearSwitch2.Play();
            }

            gear += 1;
        }
    }

    // a void created by the player input componed used to go down gear
    private void OnGearBoxDown()
    {
        // lets you shift down gear
        if (manualGearShifting == true && gear > minimalGear)
        {
            if (!gearSwitch.isPlaying)
            {
                gearSwitch.Play();
            }

            if (!gearSwitch2.isPlaying)
            {
                gearSwitch2.Play();
            }

            gear -= 1;
        }
    }

    // gives the value of how far the right trigger has been pressed
    private void OnGas(InputValue accelaration)
    {
        engineSound.volume = accelaration.Get<float>();

        if (engineSound.isPlaying && !FirstAudioCheck)
        {
            FirstAudioCheck = true;
            engineSound.Play();
        }
        else
        {
            engineSound.UnPause();
        }

        if(accelaration.Get<float>() != 0 && !FirstAudioCheck)
        {
            FirstAudioCheck = true;
            engineSound.Play();
        }
        else if (accelaration.Get<float>() != 0)
        {
            engineSound.Play();
        }

        if(accelaration.Get<float>() == 0)
        {
            engineSound.Stop();
        }

        accelarationValue = accelaration.Get<float>();
    }

    // gives the value of how far the left trigger has been pressed
    private void OnBrake(InputValue brake)
    {
        if (!brakeAudio.isPlaying)
        {
            brakeAudio.Play();
        }

        brakeValue = brake.Get<float>();
    }

    // gives the value of how far the left stick has been moved
    private void OnSteering(InputValue turning)
    {
        turnInput = turning.Get<Vector2>().x;
    }

}
