using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienAntiBounce : MonoBehaviour
{
    private Rigidbody otherRb;
    private new Rigidbody rigidbody;
    
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    /*private void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out Customer cust)) return;
        
        otherRb = cust.GetComponent<Rigidbody>();
            
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

        otherRb = cust.GetComponent<Rigidbody>();
        otherRb.velocity = Vector3.zero;
        otherRb.angularVelocity = Vector3.zero;
    }*/

    private void OnCollisionExit(Collision other)
    {
        otherRb = null;
        
        if (!other.gameObject.TryGetComponent(out Customer cust)) return;

        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
}
