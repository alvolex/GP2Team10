   
using System;
using UnityEngine;

public class SludgeHazard : Hazard
{
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
