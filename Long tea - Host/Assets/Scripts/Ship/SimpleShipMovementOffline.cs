using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleShipMovementOffline : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotationSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void FixedUpdate()
    {

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            rb.AddTorque(transform.up * horizontal * rotationSpeed * Time.fixedDeltaTime);
            rb.AddForce(transform.forward * vertical * moveSpeed * Time.fixedDeltaTime);
    }
}
