using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //AudioManager.Instance.PlayMusic();
            }
        }
    }
}
