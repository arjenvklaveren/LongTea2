using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipGyroControls : MonoBehaviour
{
    Gyroscope gyro;
    Vector3 rotation;
    Rigidbody rb;

    [SerializeField] private float moveSensitivity = 3f;
    [SerializeField] private float steerSensitivity = 2;
    [SerializeField] private float steerDeadzone = 20;

    void Start()
    {
        gyro = Input.gyro;
        gyro.enabled = true;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        ControlShip();
    }

    void ControlShip()
    {
        rotation = gyro.attitude.eulerAngles;
        float zRot = rotation.z;
        float yRot = rotation.y;
        //Debug.Log(System.Math.Round(rotation.x, 2) + " " + System.Math.Round(rotation.y, 2) + " " + System.Math.Round(rotation.z, 2));
        Debug.Log(System.Math.Round(rotation.y, 2));
        if (zRot < 90) zRot = 360;
        transform.eulerAngles += (new Vector3(0, -(zRot - 260), 0) / 100) * steerSensitivity;
        rb.AddForce(transform.forward * (moveSensitivity * 50) * Time.fixedDeltaTime);
    }
}
