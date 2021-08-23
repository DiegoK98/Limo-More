using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Space]
    [Header("Wheel Colliders")]

    [SerializeField] private WheelCollider FrontLeftWheelCollider;
    [SerializeField] private WheelCollider FrontRightWheelCollider;
    [SerializeField] private WheelCollider RearLeftWheelCollider;
    [SerializeField] private WheelCollider RearRightWheelCollider;

    [Space]
    [Header("Wheel Transforms")]

    [SerializeField] private Transform FrontLeftWheelTransform;
    [SerializeField] private Transform FrontRightWheelTransform;
    [SerializeField] private Transform RearLeftWheelTransform;
    [SerializeField] private Transform RearRightWheelTransform;

    [Space]
    [Header("Forces and more")]

    [SerializeField] private Transform centerOfMass;
    [SerializeField] private float motorForce;
    [SerializeField] private float brakeForce;

    [Space]

    [SerializeField] private float accelerationRate;

    [Space]

    [SerializeField] private float maxSteeringAngle;

    private float currentBrakeForce;

    private float currentAcceleration;

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float inputHorizontal;
    private float inputVertical;

    private bool isBraking;

    private float currentSteeringAngle;

    private void Awake()
    {
        this.GetComponent<Rigidbody>().centerOfMass = centerOfMass.localPosition;
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleAcceleration();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        inputHorizontal = Input.GetAxis(HORIZONTAL);
        inputVertical = Input.GetAxis(VERTICAL);
        isBraking = Input.GetKey(KeyCode.Space);
    }

    private void HandleAcceleration()
    {
        if(Math.Abs(inputVertical) > 0)
        {
            currentAcceleration += accelerationRate * 0.03f;
        }
        else
        {
            currentAcceleration -= 0.03f;
        }

        if (currentAcceleration < 0)
            currentAcceleration = 0;

        if (currentAcceleration > 1)
            currentAcceleration = 1;
    }

    private void HandleMotor()
    {
        FrontLeftWheelCollider.motorTorque = inputVertical * motorForce * currentAcceleration;
        FrontRightWheelCollider.motorTorque = inputVertical * motorForce * currentAcceleration;
        currentBrakeForce = isBraking ? brakeForce : 0f;
        ApplyBraking();
    }

    private void ApplyBraking()
    {
        FrontLeftWheelCollider.brakeTorque = currentBrakeForce;
        FrontRightWheelCollider.brakeTorque = currentBrakeForce;
        RearLeftWheelCollider.brakeTorque = currentBrakeForce;
        RearRightWheelCollider.brakeTorque = currentBrakeForce;
    }

    private void HandleSteering()
    {
        currentSteeringAngle = maxSteeringAngle * inputHorizontal;
        FrontLeftWheelCollider.steerAngle = currentSteeringAngle;
        FrontRightWheelCollider.steerAngle = currentSteeringAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(FrontLeftWheelCollider, FrontLeftWheelTransform);
        UpdateSingleWheel(FrontRightWheelCollider, FrontRightWheelTransform);
        UpdateSingleWheel(RearLeftWheelCollider, RearLeftWheelTransform);
        UpdateSingleWheel(RearRightWheelCollider, RearRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);

        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }
}
