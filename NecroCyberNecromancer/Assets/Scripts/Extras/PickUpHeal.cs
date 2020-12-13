using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHeal : MonoBehaviour
{
    private PlayerStats pStats;
    [Header("Health Bonus")]
    private bool healed = false;
    [SerializeField] private int maxHealthDividedBy =5;//means the health bonus max health will be divided by this number
    // Start is called before the first frame update
    void Start()
    {
        pStats = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !healed)
        {
            pStats.PlayerHeal(pStats.maxHealth / maxHealthDividedBy);
            pStats.UpdateUIText();
            Destroy(gameObject);
            print("player healed by " + (pStats.maxHealth / maxHealthDividedBy) + "HP");
            healed = true;//This is so it only happens once

        }
    }
}
