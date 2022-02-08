using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienAntiBounce : MonoBehaviour
{
    private new Rigidbody rigidbody;
    
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionExit(Collision other)
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

    }
}
