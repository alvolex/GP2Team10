   
using System;
using UnityEngine;

public class SludgeHazard : Hazard
{
    //todo The player should have to delete this shtuff
    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement pmovement))
        {
            pmovement.MovementSpeed /= 2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement pmovement))
        {
            pmovement.MovementSpeed *= 2;
        }
    }
}
