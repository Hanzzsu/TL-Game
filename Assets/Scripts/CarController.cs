using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    public AudioClip startcar;
    public ParticleSystem dirtParticle;
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentBrakeForce;
    private bool isBraking;
    private bool isMoving;

    [SerializeField] private float motorForce;
    [SerializeField] private float maxBrakeForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();

        if (Mathf.Abs(verticalInput) > 0.1f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (isMoving)
        {
            ActivateDirtParticle();
        }
        else
        {
            DeactivateDirtParticle();
        }

        if (isBraking)
        {
            ApplyBraking();
        }
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBraking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        if (isBraking)
        {
            // Pengereman perlahan ketika tombol spasi ditekan
            currentBrakeForce = Mathf.Lerp(0f, maxBrakeForce, Mathf.Abs(verticalInput));
            frontLeftWheelCollider.brakeTorque = currentBrakeForce;
            frontRightWheelCollider.brakeTorque = currentBrakeForce;
            rearLeftWheelCollider.brakeTorque = currentBrakeForce;
            rearRightWheelCollider.brakeTorque = currentBrakeForce;

            // Menghentikan tenaga motor saat melakukan pengereman perlahan
            frontLeftWheelCollider.motorTorque = 0f;
            frontRightWheelCollider.motorTorque = 0f;
        }
        else
        {
            // Menggerakkan mobil saat tidak melakukan pengereman
            frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
            frontRightWheelCollider.motorTorque = verticalInput * motorForce;

            // Menghentikan pengereman saat tidak menekan tombol spasi
            currentBrakeForce = 0f;
            frontLeftWheelCollider.brakeTorque = currentBrakeForce;
            frontRightWheelCollider.brakeTorque = currentBrakeForce;
            rearLeftWheelCollider.brakeTorque = currentBrakeForce;
            rearRightWheelCollider.brakeTorque = currentBrakeForce;
        }
    }

    private void ApplyBraking()
    {
        // Menghentikan tenaga motor saat melakukan pengereman tiba-tiba
        frontLeftWheelCollider.motorTorque = 0f;
        frontRightWheelCollider.motorTorque = 0f;

        // Pengereman tiba-tiba saat tombol spasi ditekan
        frontLeftWheelCollider.brakeTorque = maxBrakeForce;
        frontRightWheelCollider.brakeTorque = maxBrakeForce;
        rearLeftWheelCollider.brakeTorque = maxBrakeForce;
        rearRightWheelCollider.brakeTorque = maxBrakeForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void ActivateDirtParticle()
    {
        if (!dirtParticle.isPlaying)
        {
            dirtParticle.Play();
        }
    }

    private void DeactivateDirtParticle()
    {
        if (dirtParticle.isPlaying)
        {
            dirtParticle.Stop();
        }
    }

    public void PlayGame (){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame (){
        Debug.Log("QUIT!");
        Application.Quit();
        SceneManager.LoadScene(0);
    }
}
