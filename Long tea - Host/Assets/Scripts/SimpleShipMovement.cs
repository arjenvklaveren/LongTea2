using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody))]
public class SimpleShipMovement : NetworkBehaviour
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
        if (isLocalPlayer || NetworkManager.singleton == null)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            rb.AddTorque(horizontal * rotationSpeed * transform.up);
            rb.AddForce(moveSpeed * vertical * transform.forward);
        }
    }
}
