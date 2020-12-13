using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviroHazard : MonoBehaviour
{
    [SerializeField]
    int damage = 2;

    [SerializeField]
    float cooldown = 2;

    [SerializeField]
    Transform damageEffect = null;
    [SerializeField]
    Transform spawner = null;

    bool hazardActive = false;
    bool onCooldown = false;
    float cooldownTimer = -1;


    // Update is called once per frame
    void Update()
    {
        if(onCooldown)
        {
            if(cooldownTimer < cooldown)
            {
                cooldownTimer += Time.deltaTime;
            }else
            {
                onCooldown = false;
                cooldownTimer = 0;
            }
        }else if (hazardActive && !onCooldown)
        {
            if (damageEffect != null)
            {
                Instantiate(damageEffect, spawner.position, Quaternion.identity);
            }
            else
            {
                FindObjectOfType<PlayerStats>().PlayerTakeDamage(damage);
            }

            onCooldown = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            hazardActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            hazardActive = false;
        }
    }
}
