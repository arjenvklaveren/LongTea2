using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipGyroControls : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] private float accelerationSensitify = 3f;
    [SerializeField] private float steerSensitivity = 2;
    [Range(0.01f, 0.2f), SerializeField] private float steerDeadzone;
    [Range(1,10), SerializeField] float minimumSpeed;
    [Range(10,20), SerializeField] float maximumSpeed;

    private float moveAcceleration;

    void Start()
    {
        moveAcceleration = minimumSpeed;
        rb = GetComponent<Rigidbody>();
        Input.gyro.enabled = true;
    }

    void FixedUpdate()
    {
        RotateShip();
        MoveShip();
    }

    void RotateShip()
    {
        float steerAcceleration = Mathf.Abs(Input.acceleration.x) * steerSensitivity;
        if (Input.acceleration.x > steerDeadzone)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler((new Vector3(0, 10, 0) / 100) * steerAcceleration));
        }
        else if (Input.acceleration.x < -steerDeadzone)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler((new Vector3(0, -10, 0) / 100) * steerAcceleration));
        }    
    }

    void MoveShip()
    {
        if (Input.acceleration.z > -1 && Input.acceleration.z < 0)
        {
            if (moveAcceleration < maximumSpeed)
            {
                moveAcceleration += (accelerationSensitify / 100);
            }
        }
        else
        {
            if(moveAcceleration > minimumSpeed)
            {
                moveAcceleration -= (accelerationSensitify / 50);
            }
        }
        rb.AddForce(transform.forward * (moveAcceleration * 50) * Time.fixedDeltaTime);
    }
}
