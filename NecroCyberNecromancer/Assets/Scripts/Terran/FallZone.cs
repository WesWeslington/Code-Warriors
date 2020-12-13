using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallZone : MonoBehaviour
{
    public Transform recoveryCheckpoint = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            other.gameObject.GetComponent<PlayerStats>().PlayerTakeDamage(5);
            other.gameObject.transform.position = recoveryCheckpoint.position;
        }
    }
}
