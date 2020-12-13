using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    Animator animator = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            this.gameObject.SetActive(false);
            if (animator != null) { animator.SetBool("Unlocked", true); }
            GetComponent<PlayerStats>().AddPlayerPoint();
        }
    }
}
