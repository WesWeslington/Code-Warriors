using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    FallZone fallZoneRef = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            fallZoneRef.recoveryCheckpoint = this.transform;
        }
    }
}
