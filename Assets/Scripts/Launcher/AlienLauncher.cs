using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

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
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Customer cust))
        {
            customersToLaunch.Remove(cust.GetComponent<Rigidbody>());
        }
    }


    IEnumerator BlastAliensIntoSpace()
    {
        foreach (var rb in customersToLaunch)
        {
            rb.GetComponent<NavMeshAgent>().enabled = false;
            rb.GetComponent<AlienAntiBounce>().enabled = false;
            yield return null;

            rb.useGravity = false;

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.angularDrag = 0f;

            yield return null;

            var transformPosition = shootDirection.transform.position;
            var forceVec = new Vector3( transformPosition.x * Random.Range(30f, 100f), transformPosition.y * 100f , transformPosition.z * Random.Range(30f, 100f) );
            rb.AddForce(forceVec, ForceMode.Impulse);
            
            var torqueVec = new Vector3(Random.Range(1000f, 50000f), 100000f ,Random.Range(1000f, 50000f));
            rb.AddTorque(torqueVec, ForceMode.Impulse);

            Destroy(rb.gameObject, 3f);
        }

        customersToLaunch = new HashSet<Rigidbody>();
    }
}
