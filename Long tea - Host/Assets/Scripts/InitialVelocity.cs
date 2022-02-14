using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialVelocity : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 initialVelocity;
    // Start is called before the first frame update
    void Start()
    {
        if(rb == null) rb = GetComponent<Rigidbody>();
        if (rb != null) rb.AddForce(transform.TransformDirection(initialVelocity), ForceMode.VelocityChange);
    }
}
