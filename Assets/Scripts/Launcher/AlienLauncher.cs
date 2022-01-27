using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienLauncher : MonoBehaviour
{
    [SerializeField]private Transform shootDirection;

    private HashSet<Rigidbody> customersToLaunch = new HashSet<Rigidbody>();

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(BlastAliensIntoSpace());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Customer cust))
        {
            customersToLaunch.Add(cust.GetComponent<Rigidbody>());
        }
    }


    IEnumerator BlastAliensIntoSpace()
    {
        foreach (var rb in customersToLaunch)
        {
            rb.GetComponent<NavMeshAgent>().enabled = false;
            yield return null;

            rb.useGravity = false;

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.angularDrag = 0f;

            yield return null;
            
            rb.AddForce(shootDirection.forward * 700f, ForceMode.Impulse);
            rb.AddTorque(Vector3.forward * 100000f, ForceMode.Impulse);

            Destroy(rb.gameObject, 3f);
        }

        customersToLaunch = new HashSet<Rigidbody>();
    }
}
